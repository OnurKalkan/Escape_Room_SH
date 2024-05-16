using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    public GameObject inventory, tempItem, itemSlotsParent, craftItemParents;
    bool activeItem;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(inventory.activeInHierarchy)
            { 
                inventory.SetActive(false); 
                Cursor.visible = false;
            }
            else 
            { 
                inventory.SetActive(true);
                Cursor.visible = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && activeItem)
        {
            bool sameItem = false;
            for (int i = 1; i < itemSlotsParent.transform.childCount; i++)
            {
                if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().isCaptured == true && 
                    tempItem.GetComponent<Item>().items == itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType)
                {
                    itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount++;
                    itemSlotsParent.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount.ToString();
                    tempItem.SetActive(false);
                    tempItem = null;
                    activeItem = false;
                    i = itemSlotsParent.transform.childCount;
                    sameItem = true;
                    CraftingItemsCheck();
                }
            }
            if (!sameItem)
            {
                for (int i = 1; i < itemSlotsParent.transform.childCount; i++)
                {
                    if (itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().isCaptured == false)
                    {
                        itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount++;
                        itemSlotsParent.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemCount.ToString();
                        itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().isCaptured = true;
                        itemSlotsParent.transform.GetChild(i).GetComponent<ItemSlot>().itemType = tempItem.GetComponent<Item>().items;
                        itemSlotsParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = tempItem.GetComponent<Item>().icon;
                        itemSlotsParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = true;
                        tempItem.SetActive(false);
                        tempItem = null;
                        activeItem = false;
                        i = itemSlotsParent.transform.childCount;
                        CraftingItemsCheck();
                    }
                }
            }                      
        }
    }

    void CraftingItemsCheck()
    {
        for (int i = 1; i < craftItemParents.transform.childCount; i++)
        {
            craftItemParents.transform.GetChild(i).GetComponent<CraftItem>().CraftAvailabilityCheck();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            activeItem = true;
            tempItem = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            activeItem = false;
            tempItem = null;
        }
    }
}
