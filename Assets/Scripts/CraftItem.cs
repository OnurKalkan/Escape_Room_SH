using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class CraftItem : MonoBehaviour
{
    public CraftingItem craftItem;
    public GameObject itemSlotsParent;
    public int req1, req2, req3;
    public Items item1, item2, item3;
    bool req1check, req2check, req3check;

    private void Start()
    {
        CraftAvailabilityCheck();
    }

    public void CraftAvailabilityCheck()
    {
        for (int i = 1; i < itemSlotsParent.transform.childCount; i++)
        {
            if(itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType == item1 && 
                req1 <= itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount)
            {
                req1check = true;
            }
            if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType == item2 &&
                req2 <= itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount)
            {
                req2check = true;
            }
            if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType == item3 &&
                req3 <= itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount)
            {
                req3check = true;
            }
        }
        if(req1check && req2check && req3check)
        {
            GetComponent<Button>().interactable = true;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
        }
        else
        {
            GetComponent<Button>().interactable = false;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    public enum CraftingItem
    {
        YellowGreenGun
    }

    public void Craft()
    {
        for (int i = 1; i < itemSlotsParent.transform.childCount; i++)
        {
            if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType == item1 &&
                req1 <= itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount)
            {
                itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount -= req1;
                itemSlotsParent.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount.ToString();
                if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount == 0)
                {
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().isCaptured = false;
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType = Items.None;
                    itemSlotsParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                }
            }
            if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType == item2 &&
                req2 <= itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount)
            {
                itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount -= req2;
                itemSlotsParent.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount.ToString();
                if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount == 0)
                {
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().isCaptured = false;
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType = Items.None;
                    itemSlotsParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                }
            }
            if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType == item3 &&
                req3 <= itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount)
            {
                itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount -= req3;
                itemSlotsParent.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount.ToString();
                if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount == 0)
                {
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().isCaptured = false;
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType = Items.None;
                    itemSlotsParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                }
            }
        }
        req1check = false; req2check = false; req3check = false;
        CraftAvailabilityCheck();
    }
}
