using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = transform.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach (Item item in inventory.GetItemList())
        {
            int x = 0;
            int y = 0;
            float itemSlotCellSize = 30;

            RectTransform itemSlotTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotTransform.gameObject.SetActive(true);


            itemSlotTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI UiText = itemSlotTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                UiText.SetText(item.amount.ToString());
            } else
            {
                UiText.SetText("");
            }
            ++x;
            if(x > 4) // Change this value to change the length of the cell grid
            {
                x = 0;
                ++y;
            }
        }
    }

}
