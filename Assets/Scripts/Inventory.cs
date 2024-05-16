using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //crafting items
    public GameObject greenYellowGun;

    void Start()
    {        
        //InventoryCheck();
    }
    void InventoryCheck()
    {
        int yellowCubeNo = PlayerPrefs.GetInt("YellowCube", 3);
    }
}

    
    




