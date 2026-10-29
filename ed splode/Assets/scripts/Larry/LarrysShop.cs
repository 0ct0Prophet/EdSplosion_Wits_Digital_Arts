using System;
using UnityEngine;
using UnityEngine.UI;

public class LarrysShop : MonoBehaviour
{

    public bool playerInRange, talkingWithPLayer;

    public GameObject shopDialogue;
    public Button closeButton;
    public Button buyButton;
    public GameObject healthBar;

    public GameObject buyPanelUI;


    private void Start()
    {
        shopDialogue.SetActive(false);

        closeButton.onClick.AddListener(StopInteracting);
        buyButton.onClick.AddListener(BuyMode);
    }



    public void Interact()
    {
        talkingWithPLayer = true;

        // Open shop UI
        DisplayDialogueUi();

        MovementManager.Instance.EnableMovement(false);
        MovementManager.Instance.EnableLookOnly(false);

        //MovementManager.Instance.canLook = false;
        //MovementManager.Instance.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StopInteracting()
    {
        talkingWithPLayer = false;

        // Close shop UI
        HideDialogueUi();
        healthBar.SetActive(true);

        MovementManager.Instance.EnableMovement(true);
        MovementManager.Instance.EnableLookOnly(true);

        //MovementManager.Instance.canLook = true;
        //MovementManager.Instance.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    public void BuyMode()
    {
        HideDialogueUi();
        buyPanelUI.SetActive(true);

    }

    public void DialogueMode()
    {
        DisplayDialogueUi();
    }

    private void HideDialogueUi()
    {
        shopDialogue.SetActive(false);
    }

    private void DisplayDialogueUi()
    {
        healthBar.SetActive(false);
        buyPanelUI.SetActive(false);
        shopDialogue.SetActive(true);
    }

    #region || ---- Trigger Methods --- ||
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
    #endregion || ---- Trigger Methods -- ||

}
