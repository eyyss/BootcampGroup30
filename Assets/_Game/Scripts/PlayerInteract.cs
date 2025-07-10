using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public static float InteractDistance = 3f;
    public LayerMask interactMask;

    private IInteractable interactable;
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * InteractDistance);
    }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, InteractDistance, interactMask))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent(out IInteractable _interactable))
            {
                if (_interactable != interactable)
                {
                    if (interactable != null)
                    {
                        interactable.Exit();
                    }

                    interactable = _interactable;
                    interactable.Enter();
                }

                interactable.Tick();
            }
            else
            {
                if (interactable != null)
                {
                    interactable.Exit();
                    interactable = null;
                }
            }
        }
        else
        {
            if (interactable != null)
            {
                interactable.Exit();
                interactable = null;
            }
        }

    }

}