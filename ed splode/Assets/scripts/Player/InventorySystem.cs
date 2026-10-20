using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    

    public GameObject inventoryScreenUI;
    public GameObject canvasInfo;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    public bool isOpen;

    public bool isInventoryFull;

    public TextMeshProUGUI coinText;
    internal int currentCoins = 50;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
    }


    void Update()
    {

        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame && !isOpen)
        {

            Debug.Log("Tab is pressed");
            inventoryScreenUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canvasInfo.SetActive(false);

            isOpen = true;

        }
        else if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame && isOpen)
        {
            inventoryScreenUI.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canvasInfo.SetActive(true);

            isOpen = false;
        }

        coinText.text = $"{currentCoins} Coins";
    }

}