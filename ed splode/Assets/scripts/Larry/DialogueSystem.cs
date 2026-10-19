using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    public TextMeshProUGUI dialogueText;

    public Button button1;
    public Button button2;

    public Canvas dialogueUi;

    public bool dialogueUiActive;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void OpenDialogueUi()
    {
        dialogueUi.gameObject.SetActive(true);
        dialogueUiActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseDialogueUi()
    {
        dialogueUi.gameObject.SetActive(false);
        dialogueUiActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}