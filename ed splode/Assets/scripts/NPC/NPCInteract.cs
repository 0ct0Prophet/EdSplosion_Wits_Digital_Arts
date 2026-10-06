using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public void Interact()
    {
        NPC npcInteraction = GetComponent<NPC>();
        if (npcInteraction != null && npcInteraction.canInteract())
            npcInteraction.Interact();
    }
}
