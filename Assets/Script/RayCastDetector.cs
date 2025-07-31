using UnityEngine;
using UnityEngine.InputSystem;

public class RayCastDetector : MonoBehaviour
{
    public LayerMask detectionMask;

    public RaycastHit CheckHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, detectionMask))
        {
            return hit;
        }
        return new RaycastHit();
    }

    public void CheckHitRef()
    {
        // Se não usar mais o refSegurandoObjeto, pode remover
    }
}
