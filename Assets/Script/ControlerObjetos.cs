using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlerObjetos : MonoBehaviour
{
    public RayCastDetector rayCastDetector;
    public Vector3 currentScale;
    private GameObject itemAtual = null;
    public Vector3 escalaBase = Vector3.one;
    public float distanciaAntiga;
    public Transform refSegurandoObjeto;



    public Vector3 positionOriginal;
    public Vector3 currentPosition;
    public float distanciaentrePontos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rayCastDetector = GetComponent<RayCastDetector>();
        refSegurandoObjeto.transform.localPosition = Camera.main.transform.forward * 2;
    }

    void Update()
    {
        rayCastDetector.CheckHitRef();
        MoveItem();
    }

    void MoveItem()
    {
        // item que vai ser movimentado
        RaycastHit  hit = rayCastDetector.CheckHit();
        if (hit.collider == null) return ;

        // Obter RigidyBody do item que vai ser movimentado
        GameObject item = hit.collider.gameObject;
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            
            itemAtual = item;
            // Sempre que pegar, atualiza a escala base com a escala atual
            escalaBase = itemAtual.transform.localScale;    
            //seguir minha referencia de item
            itemAtual.transform.SetParent(refSegurandoObjeto);

            positionOriginal = itemAtual.transform.position;

            // desativar gravidade 
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.freezeRotation = true;
        }

        if (Input.GetMouseButton(0) && item == itemAtual)
        { 
            //Obter distancia entre player e item;
            float distancia = Vector3.Distance(transform.position, itemAtual.transform.position);
            float distanciaComp =Vector3.Distance(transform.position,  positionOriginal);
            
            currentPosition = itemAtual.transform.position;
            distanciaentrePontos = Vector3.Distance(currentPosition, positionOriginal);

            if (distancia < distanciaComp )
            {
                distanciaentrePontos *= -1;
            }
 
            //Obter distancia entre player e item;
            itemAtual.transform.localScale = escalaBase *  distancia * 0.1f;
            // Mover item 
            itemAtual.transform.position = refSegurandoObjeto.transform.position;
        }

        if(Input.GetMouseButtonUp(0) && item == itemAtual)
        {
            
            distanciaAntiga = Vector3.Distance(transform.position, itemAtual.transform.position);
            // ativar gravidade
            item.transform.SetParent(null);
            rb.useGravity = true;
            rb.freezeRotation = false;

            //Ignorar itematual
            itemAtual = null;
        }
    }
}
