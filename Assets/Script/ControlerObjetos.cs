using UnityEngine;
using UnityEngine.InputSystem;

public class ControlerObjetos : MonoBehaviour
{
    public RayCastDetector rayCastDetector;
    public float tamanho;


    public Transform refSegurandoObjeto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rayCastDetector = GetComponent<RayCastDetector>();
    }


 // Update is called once per frame
 void FixedUpdate()
    {
        MoveItem();
    }

    void MoveItem()
    {
        // item que vai ser movimentado
        GameObject item = rayCastDetector.CheckHit().collider.gameObject;

        // Obter RigidyBody do item que vai ser movimentado
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (item != null && Input.GetMouseButton(0))
        {
            // desativar gravidade 
            rb.useGravity = false;

            //seguir minha referencia de item
            rb.transform.position = Vector3.Lerp(rb.transform.position, refSegurandoObjeto.position, Time.deltaTime * 200f);
        }
        else
        {
            // ativar gravidade
            rb.useGravity = true;
         }
    }
}
