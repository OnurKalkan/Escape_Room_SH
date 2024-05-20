using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class CraftItem : MonoBehaviour
{
    public CraftingItem craftItem;
    public GameObject itemSlotsParent, equipItemsPanel;
    Transform rightHandSpot, leftHandSpot;
    public int req1, req2, req3;
    public Items item1, item2, item3;
    bool req1check, req2check, req3check;
    WeaponList weaponList;

    private void Awake()
    {
        weaponList = GameObject.Find("GameManager").GetComponent<WeaponList>();
        rightHandSpot = GameObject.FindGameObjectWithTag("RightHandSpot").transform;
        leftHandSpot = GameObject.FindGameObjectWithTag("LeftHandSpot").transform;
    }

    private void Start()
    {
        CraftAvailabilityCheck();
        transform.Find("Item Req Text").GetComponent<TextMeshProUGUI>().text = item1.ToString() + ": " + req1.ToString() + "\n" +
            item2.ToString() + ": " + req2.ToString() + "\n" + item3.ToString() + ": " + req3.ToString() + "\n";
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
        YellowGreenGun,
        BlueShield
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
        GameObject instObj;
        if (craftItem == CraftingItem.YellowGreenGun)
        {
            instObj = weaponList.yellowGreenGun;
        }
        else if (craftItem == CraftingItem.BlueShield)
        {
            instObj = weaponList.blueShield;
        }
        else
        {
            instObj = weaponList.yellowGreenGun;
        }
        GameObject newItem = Instantiate(instObj, transform.position, Quaternion.identity);        
        
        if (newItem.GetComponent<Weapon>().weaponType == Weapon.WeaponType.Ranged)
        {            
            for (int i = 0; i < equipItemsPanel.transform.Find("Range Weapons").childCount - 1; i++)
            {
                if(equipItemsPanel.transform.Find("Range Weapons").GetChild(i).GetComponent<ItemSlot>().isCaptured == false)
                {
                    equipItemsPanel.transform.Find("Range Weapons").GetChild(i).GetComponent<ItemSlot>().isCaptured = true;
                    equipItemsPanel.transform.Find("Range Weapons").GetChild(i).GetComponent<ItemSlot>().itemType = Items.Weapon;
                    equipItemsPanel.transform.Find("Range Weapons").GetChild(i).GetComponent<ItemSlot>().itemCount = 1;
                    equipItemsPanel.transform.Find("Range Weapons").GetChild(i).GetComponent<Image>().sprite = newItem.GetComponent<Weapon>().icon;
                    newItem.transform.parent = rightHandSpot;
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localEulerAngles = Vector3.zero;
                    i = equipItemsPanel.transform.Find("Range Weapons").childCount - 1;
                }
            }            
        }
        if (newItem.GetComponent<Weapon>().weaponType == Weapon.WeaponType.Melee)
        {
            for (int i = 0; i < equipItemsPanel.transform.Find("Melee Weapons").childCount - 1; i++)
            {
                if (equipItemsPanel.transform.Find("Melee Weapons").GetChild(i).GetComponent<ItemSlot>().isCaptured == false)
                {
                    equipItemsPanel.transform.Find("Melee Weapons").GetChild(i).GetComponent<ItemSlot>().isCaptured = true;
                    equipItemsPanel.transform.Find("Melee Weapons").GetChild(i).GetComponent<ItemSlot>().itemType = Items.Weapon;
                    equipItemsPanel.transform.Find("Melee Weapons").GetChild(i).GetComponent<ItemSlot>().itemCount = 1;
                    equipItemsPanel.transform.Find("Melee Weapons").GetChild(i).GetComponent<Image>().sprite = newItem.GetComponent<Weapon>().icon;
                    newItem.transform.parent = leftHandSpot;
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localEulerAngles = Vector3.zero;
                    i = equipItemsPanel.transform.Find("Melee Weapons").childCount - 1;
                }
            }
        }
    }
}
