using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour, Interactable 
{
    public NpcDialogue dialogueData;
    public GameObject dialogueUI;
    public TMP_Text dialogueText, nameText;
    public Image potraitImage;

    private int _dialogueIndex;
    private bool _isTyping, _isDialogueActive;

    public bool canInteract()
    {
        return !_isDialogueActive;
    }

    public void Interact()
    {

        // Check if dialogue data is null or if the game is paused and dialogue is not active
        if (dialogueData == null || (PauseController.isGamePaused && !_isDialogueActive))
        
            return;
        

        if (!_isDialogueActive)
        {
            NextLine();
        }
        else 
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        _isDialogueActive = true;
        _dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        potraitImage.sprite = dialogueData.npcPotrait;

        PauseController.SetGamePaused(true);


        StartCoroutine(TypeDialogue());

    }

    void NextLine()
    {
        if (_isTyping)
        {
            //Skips line and displays the full line immediately
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[_dialogueIndex]);
            _isTyping = false;
        }
        else if (++_dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // Display Next Line
            StartCoroutine(TypeDialogue());
        }
        else
        {
            EndDialogue();
        }

    }

    IEnumerator TypeDialogue()
    {
        _isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[_dialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        _isTyping = false;

        if (dialogueData.dialogueLines.Length > _dialogueIndex && dialogueData.autoProgressLine[_dialogueIndex])
        {
            
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        _isDialogueActive = false;
        dialogueText.SetText("");   
        dialogueUI.SetActive(false);
        PauseController.SetPaused(false);   
    }

}
