using UnityEngine;

public class RayCastDetector : MonoBehaviour
{

    // === RAYCAST ===
    [Header("Configuração do Detector")]
    [Tooltip("Alcance máximo do raycast")]
    public float maxDistance = 1f;

    [Tooltip("Layers a detectar")]
    public LayerMask layerMask;

    // Guarda a última informação de colisão
    private RaycastHit _lastHitInfo;
    public RaycastHit LastHitInfo => _lastHitInfo;
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
}
