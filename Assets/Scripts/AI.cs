using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Raycast>().isActive)
            transform.DOLookAt(player.transform.position + new Vector3(0,1,0), 1);
    }
}
