using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : MonoBehaviour
{

   // === MOVIMENTAÇÃO ===
   [Header("Movimentation")]
    public float speedMoviment = 10f;     // volocidade do movimento do player
    public float forceJump= 10; //Forca do pulo do Player
    public float PlayerDampingMax = 5f;
    public float PlayerDampingMin = 1.2f;
    public Rigidbody rb; // RigidyBody Player

    [HideInInspector]public float moveH; 
    [HideInInspector]public float moveV;

    // === ROTAÇÃO ===
    [Header("Rotation")]
    [SerializeField]private GameObject head; // cabeça do player
    [SerializeField]private float    sensitivityX =  100f; // sensibilidade da rotação
    [SerializeField]private float    sensitivityY =  100f; // sensibilidade da rotação
    [SerializeField]private float    minRotationX =  -45f; // angulo minimo ao olhar para baixo
    [SerializeField]private float    maxRotationX =  45f; // angulo maximo para olhara para cima
    private float   currentRotationX = 0f; // rotação atual;
    
    // === State Machine ===
    [HideInInspector]public StateMachine    stateMachine;
    [HideInInspector]public IdleState       idleState;
    [HideInInspector]public JumpState       jumpState;
    [HideInInspector]public WalkingState       walkingState;
    [HideInInspector]public RunState       runState;

    // === Ground ===
    [Header("Ground")]
    public bool isground; 
    public LayerMask LayerMaskGrounded;
    
     
    void Start()
    {
        // Pegar RigidyBody do objeto em que o script esta
        rb = GetComponent<Rigidbody>();

        // StateMachine
        stateMachine = new StateMachine();
        // Estados
        idleState = new IdleState(this);
        jumpState = new JumpState(this);
        walkingState = new WalkingState(this);
        runState = new RunState(this);

        // Define estado incial
        stateMachine.ChangeState(idleState);

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Rotation();
        Moviment();
        stateMachine.Update();
    }

    void Moviment()
    {
        // Imputs
         moveH = Input.GetAxis("Horizontal");
         moveV = Input.GetAxis("Vertical");

        if(moveV != 0 || moveH != 0){
            if(Input.GetKey(KeyCode.LeftShift)){
            stateMachine.ChangeState(runState);
            }else{
            stateMachine.ChangeState(walkingState);
            }
        }
         
        // Congela Rotação do Rigidbody(rb)
        rb.freezeRotation = true;
        
        // Define o amortecimento linear do rigidbody (rb):
        rb.linearDamping = isground? PlayerDampingMax : PlayerDampingMin;
    }

    void Rotation(){
        // Configurações do cursor
        Cursor.visible = false; // deixar invisivel
        Screen.lockCursor = true; // travar cursos na tela
        
        // Pegar posição do mouse na tela.
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        // Manter a rotação do eixo Y da cabeça  entre o angulo minimo e o maximo;
        currentRotationX -= mouseY;
        currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);

        // Rotaciona localmente a cabeça do player apenas no eixo Y (horizontal)
        head.transform.localRotation = Quaternion.Euler(currentRotationX,0f,0f);
        transform.Rotate(0, mouseX, 0);
    }

    /* ---------------------------------------------------------JUMP--------------------------------------------------
     Sistema de pulo fica aqui.
    ------------------------------------------------------------------------------------------------------------------*/
    void  Jump(){

        // Tocando no chão
        isground = Physics.CheckSphere(transform.position, 1.2f, LayerMaskGrounded);

        // Get Entrada Teclado Jump
        bool inputJump = Input.GetKeyDown(KeyCode.Space);

        // Verificar entrada se 1 executar pulo
        if(inputJump && isground){
            stateMachine.ChangeState(jumpState);
        }
    }
}
