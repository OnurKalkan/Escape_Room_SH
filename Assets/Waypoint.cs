using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject miniMap, pin;

    public void PinPoint()
    {
        pin.SetActive(true);
        pin.transform.position = miniMap.transform.Find(this.gameObject.name).transform.position;        
    }
}
