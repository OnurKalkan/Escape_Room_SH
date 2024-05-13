using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public bool electricGun, fireGun, rangeGun;
    public bool isActive = true;
    public GameObject rangeProjectile, fire, electric;
    GameObject player;
    public int aim = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        if (isActive)
            StartCoroutine(SpawnProjectile());
    }

    IEnumerator SpawnProjectile()
    {
        float xPos = 0;
        float yPos = 1;
        float zPos = 0;

        if (aim < 3)
        {
            xPos = Random.Range(-3.0f, 3.0f);
            yPos = Random.Range(-3.0f, 3.0f);
            zPos = Random.Range(-3.0f, 3.0f);
        }
        else if(aim < 6)
        {
            xPos = Random.Range(-1.0f, 1.0f);
            yPos = Random.Range(-1.0f, 1.0f);
            zPos = Random.Range(-1.0f, 1.0f);
        }
        yield return new WaitForSeconds(3);
        if(rangeGun)
        {
            GameObject projectile = Instantiate(rangeProjectile, transform.position, Quaternion.identity);
            projectile.transform.parent = null;
            projectile.transform.DOMove(player.transform.position + new Vector3(xPos, yPos, zPos), 2);
        }
        else if (fireGun)
        {
            GameObject projectile = Instantiate(fire, transform.position, Quaternion.identity);
            projectile.transform.parent = null;
            projectile.transform.DOMove(player.transform.position + new Vector3(xPos, yPos, zPos), 2);
        }
        else if (electricGun)
        {
            GameObject projectile = Instantiate(electric, transform.position, Quaternion.identity);
            projectile.transform.parent = null;
            projectile.transform.DOMove(player.transform.position + new Vector3(xPos, yPos, zPos), 1);
        }
        if(isActive)
            StartCoroutine(SpawnProjectile());
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //print(hit.distance);
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.CompareTag("Player"))
            {
                if (hit.distance > 10 && hit.distance < 20)
                {
                    electricGun = false;
                    fireGun = false;
                    rangeGun = true;
                }
                else if (hit.transform.GetComponent<Player>().ironShield && hit.distance < 10)
                {
                    electricGun = true;
                    fireGun = false;
                    rangeGun = false;
                }
                else if (hit.transform.GetComponent<Player>().magneticShield && hit.distance < 10)
                {
                    electricGun = false;
                    fireGun = true;
                    rangeGun = false;
                }
                else
                {
                    electricGun = false;
                    fireGun = false;
                    rangeGun = false;
                }
            }
        }
    }
}
