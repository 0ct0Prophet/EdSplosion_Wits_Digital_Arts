using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BuySystem;

public class ShopItemSlot : MonoBehaviour
{
    [Header("UI")]
    public Image itemImageUI;
    public TextMeshProUGUI itemNameUI;
    public Button buyButtonUI;

    [Header("Data")]
    public ShopItemData shopItemData;

    private void Start()
    {
        buyButtonUI.onClick.AddListener(SelectItem);
    }

    public void Setup(BuySystem.ShopItemData itemData)
    {
        shopItemData = itemData;

        if (itemNameUI != null)
        {
            itemNameUI.text = shopItemData.itemName;
        }

        if (itemImageUI != null)
        {
            itemImageUI.sprite = shopItemData.itemIcon;
            itemImageUI.enabled = shopItemData.itemIcon != null;
        }

        if (buyButtonUI != null)
        {
            buyButtonUI.onClick.RemoveListener(SelectItem);
            buyButtonUI.onClick.AddListener(SelectItem);
            buyButtonUI.interactable = true;
        }

    }

    public void SelectItem()
    {
        
        if (BuySystem.Instance != null)
        {
            BuySystem.Instance.SelectItem(shopItemData);
        }
    }
    private void OnDestroy()
    {
        if (buyButtonUI != null)
        {
            buyButtonUI.onClick.RemoveListener(SelectItem);
        }
    }
}
