using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Sprite icon;
    public WeaponType weaponType;
    public int weaponHealth = 100;

    public enum WeaponType
    {
        Ranged,
        Melee,
        Cloth
    }
}
