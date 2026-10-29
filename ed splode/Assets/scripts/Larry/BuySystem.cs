using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySystem : MonoBehaviour
{
    #region || -- Singelton -- ||
    public static BuySystem Instance { get; set; }
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
    #endregion

    [Header("ShopKeeper")]
    public LarrysShop ShopKeeper;

    [Header("UI")]
    public Transform contentTransform;
    public GameObject shopItemPrefab;
    public Button backButton;

    [Header("Current List")]
    public List<ShopItemData> currentShopList = new List<ShopItemData>();


    public enum ItemType
    {
        AttackBoost,
        Healing,
        SpeedBoost
    }


    [System.Serializable]
    public class ShopItemData
    {
        public string itemName;
        public Sprite itemIcon;
        public ItemType itemType;
    }

    private void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(ExitBuyMode);
        }

        InitializeBuyList(currentShopList);
    }

    private void InitializeBuyList(List<ShopItemData> shopList)
    {
      

        // Clear existing slots to prevent duplicates.
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItemData item in shopList)
        {
            if (item == null)
                continue;

            GameObject slotObject = Instantiate(
                shopItemPrefab,
                contentTransform
            );

            ShopItemSlot slot = slotObject.GetComponent<ShopItemSlot>();

            if (slot == null)
            {
                Debug.LogError("Shop item prefab is missing ShopItemSlot!");
                Destroy(slotObject);
                continue;
            }

            slot.Setup(item);
        }
    }

    public void SelectItem(ShopItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("No item was selected.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Could not find a GameObject tagged Player.");
            return;
        }

        bool effectApplied = false;

        switch (item.itemType)
        {
            case ItemType.Healing:
                {
                    PlayerStats stats = player.GetComponent<PlayerStats>();

                    if (stats != null)
                    {
                        stats.HealPercentage(0.3f);
                        effectApplied = true;
                    }
                    else
                    {
                        Debug.LogWarning("Player is missing PlayerStats.");
                    }

                    break;
                }

            case ItemType.SpeedBoost:
                {
                    Movement movement = player.GetComponent<Movement>();

                    if (movement != null)
                    {
                        movement.ActivateSpeedBoost();
                        effectApplied = true;
                    }
                    else
                    {
                        Debug.LogWarning("Player is missing Movement.");
                    }

                    break;
                }

            case ItemType.AttackBoost:
                {
                    Debug.LogWarning(
                        "Attack Boost needs to be connected to your damage system."
                    );

                    break;
                }
        }

        if (effectApplied)
        {
            Debug.Log("Selected item: " + item.itemName);
            ExitBuyMode();
        }
    }

    public void ExitBuyMode()
    {
        ShopKeeper.DialogueMode();
    }

}
