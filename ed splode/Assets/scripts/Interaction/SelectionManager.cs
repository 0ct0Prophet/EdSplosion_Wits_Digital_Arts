
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public GameObject interaction_Info_UI;
    private TextMeshProUGUI interaction_text;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            #region || --- Shop Interaction --- ||
            LarrysShop shop = selectionTransform.GetComponent<LarrysShop>();

            if (shop != null && shop.playerInRange)
            {
                if (shop.talkingWithPLayer == false)
                {
                    interaction_text.text = "Talk";
                    interaction_Info_UI.SetActive(true);

                    if (Mouse.current.leftButton.wasPressedThisFrame && shop.talkingWithPLayer == false)
                    {
                        shop.Interact();
                    }
                }
                else
                {
                    interaction_text.text = "";
                    interaction_Info_UI.SetActive(false);
                }
            }

            

            #endregion || --- Shop Interaction --- ||

            #region || --- NPC Interaction --- ||
            NPC npc = selectionTransform.GetComponent<NPC>();

            if (npc && npc.playerInRange)
            {
                interaction_text.text = "Talk";
                interaction_Info_UI.SetActive(true);

                if (Mouse.current.rightButton.wasPressedThisFrame && npc.isTalkingWithPlayer == false)
                {
                    npc.StartConversation();
                }

                if (DialogueSystem.Instance.dialogueUiActive)
                {
                    interaction_Info_UI.SetActive(false);
                }
            }
            else
            {
                interaction_text.text = "";
                interaction_Info_UI.SetActive(false);
            }
            #endregion || --- NPC Interaction --- ||

            if (!shop && !npc)
            {
                interaction_text.text = "";
                interaction_Info_UI.SetActive(false);
            }

        }
    }
}