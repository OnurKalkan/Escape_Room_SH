using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;
    public Items items;

    public enum Items
    {
        KeyCard,
        Coin
    }
}