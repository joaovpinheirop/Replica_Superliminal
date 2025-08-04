using UnityEngine;

public class RayCastDetector : MonoBehaviour
{

    // === RAYCAST ===
    [Header("Configuração do Detector")]
    [Tooltip("Alcance máximo do raycast")]
    public float maxDistance = 1f;
    public GameObject refSegurandoObjeto;
    public float recuo = 0.5f;

    [Tooltip("Layers a detectar")]
    public LayerMask layerMask;
    public LayerMask layerMaskScale;

    // Guarda a última informação de colisão
    private RaycastHit _lastHitInfo;
    public RaycastHit LastHitInfo => _lastHitInfo;
    public Transform head;
    public Vector3 rectOffset;
    // public Vector3 OffsetRay;



    public RaycastHit CheckHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _lastHitInfo, maxDistance, layerMask))
        {
            // Se for um carrinho ver se tem algum item dentro
            if (_lastHitInfo.collider.CompareTag("ItemMove"))
            {
                var obj = _lastHitInfo.collider.gameObject;
                if (obj.transform.childCount == 0)
                {
                    Debug.DrawLine(ray.origin, _lastHitInfo.point);
                    return _lastHitInfo;
                }
            }
        }

        Debug.Log(LastHitInfo.point.magnitude);
        return LastHitInfo;
    }

    public void CheckHitRef()
    {
        Ray ray = new Ray(head.position, head.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMaskScale))
        {
            Debug.DrawRay(head.position, head.forward * hit.distance, Color.red);
            refSegurandoObjeto.transform.rotation = Quaternion.identity;
            Vector3 rectOffset = hit.point + hit.normal * recuo;
            refSegurandoObjeto.transform.position = rectOffset;
        }
        else
        {
            Debug.DrawRay(head.position, head.forward * maxDistance, Color.green);
        }
    }
}
