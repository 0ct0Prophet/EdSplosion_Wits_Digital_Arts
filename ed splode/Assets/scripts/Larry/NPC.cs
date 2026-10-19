using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool playerInRange;
    public bool isTalkingWithPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void StartConversation()
    {
        isTalkingWithPlayer = true;

        print("Starting conversation with NPC: " + gameObject.name);

        DialogueSystem.Instance.OpenDialogueUi();
        DialogueSystem.Instance.dialogueText.text = "Fancy meeting you here. Looks like it was quite a mess back there" +
            ", let me offer you some recupperation.";
        DialogueSystem.Instance.button1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Thanks";
        DialogueSystem.Instance.button1.onClick.AddListener(() =>
        {
            DialogueSystem.Instance.CloseDialogueUi();
            isTalkingWithPlayer = false;
        });
    }

}