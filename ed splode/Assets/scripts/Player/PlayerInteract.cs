using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
 
    // Update is called once per frame

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

            foreach (Collider collider in colliderArray)
            {
                var interactable = collider.GetComponent<IInteractable>();
                if (interactable != null && interactable.canInteract())
                {
                    interactable.Interact();
                }
            }
        }
    }

}
