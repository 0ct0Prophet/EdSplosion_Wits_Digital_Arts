using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; //Tracks closets interactable
    public GameObject interactionPrompt; //UI prompt to show when player can interact
    void Start()
    {
        interactionPrompt.SetActive(false); 
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            interactableInRange?.Interact(); 
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.canInteract()) //Check if the object has the tag "Interactable"
        {
            interactableInRange = interactable; //Set the interactable in range
            interactionPrompt.SetActive(true); //Show prompt
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange) //Check if the object has the tag "Interactable"
        {
            interactableInRange = null; //Clear the interactable in range
            interactionPrompt.SetActive(false); //Hide prompt
        }
    }
}

