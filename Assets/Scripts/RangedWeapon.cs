using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.transform.parent = null;
            proj.GetComponent<Rigidbody>().isKinematic = false;            
            proj.GetComponent<Rigidbody>().AddRelativeForce(transform.right * 1000);            
            proj.transform.localScale = Vector3.one * 0.25f;
        }
    }
}
