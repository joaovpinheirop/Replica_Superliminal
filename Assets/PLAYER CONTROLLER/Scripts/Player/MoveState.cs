using UnityEngine;

public class MoveState : State
{
    // Controlador do player
    public ControllerPlayer controller;
    // Velocidade de movimentação, definida na criação do estado
    public float speedMoviment;
    
    // Construtor
    public MoveState(ControllerPlayer controllerPlayer, float speedMoviment, string stateName) : base(stateName)
    {
        this.controller = controllerPlayer;
        this.speedMoviment = speedMoviment;
    }

 public override void Enter()=>base.Enter();

    public override void Exit(){
        base.Exit();
    }

    public override void Update()
    {
        // Se não estiver movimentando mudar para o estado de parado
        if(controller.moveV == 0 && controller.moveH == 0)
        controller.stateMachine.ChangeState(controller.idleState);
       
        // moviment
        Vector3 moveDirection = new Vector3(controller.moveH, 0f, controller.moveV);

        
        if(controller.isground){
            //Normalizar movimento para ficar sempre entre 0 e 1
            moveDirection = moveDirection.normalized* speedMoviment;
            
            // Se o personagem estiver no chão, ele pode se mover na direção indicada.
            moveDirection = controller.transform.right * moveDirection.x + controller.transform.forward * moveDirection.z;
            
            // Verificar a normal da superfície para aplicar a movimentação adequada em rampas
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, Vector3.down, out hit, 1.2f))
            {
                // Ajusta a direção de movimento para respeitar a superfície inclinada
                 moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal);
            }

            // mudar Daping para parada imediata quando parar de andar;

            //Determinar força de movimento
            Vector3 force = moveDirection * 10f;
            controller.rb.AddForce(force, ForceMode.Force);

            Vector3 flatVel =  new Vector3(
                controller.rb.linearVelocity.x,
                0f,
                controller.rb.linearVelocity.z
                );

                if(flatVel.magnitude > speedMoviment){
                    Vector3 limitMoviment = flatVel.normalized * speedMoviment;
                    controller.rb.linearVelocity =  new Vector3(
                        limitMoviment.x,
                        controller.rb.linearVelocity.y,
                        limitMoviment.z
                    );
                }
        }
        base.Update();
    }
}