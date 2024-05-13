using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool holdingKeyCard, coin, keypadActive;
    public bool itemTrigger;
    public GameObject itemObj = null;
    public Image itemSlot1, itemSlot2, crossHair;
    float crosshairScaleSpeed = 0.25f;
    public bool ironShield, magneticShield;
    public GameObject ai, aiAgent;
    public int health = 0, shieldHealth = 0;

    int fearless = 8;
    int runSpeed = 0, aim = 0;

    public SaveData saveData;

    private void Awake()
    {
        saveData = GameObject.Find("GameManager").GetComponent<SaveData>();
        health = PlayerPrefs.GetInt("PlayerHealth", 100);
        saveData = SaveData.Instantiate(saveData);
    }

    private void Start()
    {
        if(ironShield)
            transform.Find("IronShield").gameObject.SetActive(true);
        else if (magneticShield)
            transform.Find("MagneticShield").gameObject.SetActive(true);

        if(fearless > 5)
        {
            runSpeed += 1;
            aim += 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            crossHair.color = new Color(1, 0, 0, 0.5f);//turn red
            crossHair.gameObject.transform.DOScale(Vector3.one * 0.5f, crosshairScaleSpeed);
            other.transform.Find("Icon").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            other.transform.Find("Icon").gameObject.SetActive(true);
            other.transform.Find("Icon").GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0.7f), crosshairScaleSpeed);
            if (other.GetComponent<Item>().items == Item.Items.KeyCard)
            {
                itemTrigger = true;
                itemObj = other.gameObject;
            }  
            else if (other.GetComponent<Item>().items == Item.Items.Coin)
            {
                itemTrigger = true;
                itemObj = other.gameObject;
            }
        }
        if (other.CompareTag("Keypad"))
        {
            crossHair.color = new Color(1, 0, 0, 0.5f);//turn red
            crossHair.gameObject.transform.DOScale(Vector3.one * 0.5f, crosshairScaleSpeed);
            keypadActive = true;
            itemObj = other.gameObject;
        }
        if (other.CompareTag("NewLevel"))
        {
            SceneManager.LoadScene(0);
        }
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);            
            PlayerPrefs.SetInt("PlayerHealth", health);
        }
    }

    void OpenDoor(GameObject doors)
    {
        doors.transform.Find("LeftDoor").transform.DOLocalMoveZ(4.5f, 1);
        doors.transform.Find("RightDoor").transform.DOLocalMoveZ(-2.5f, 1);
    }

    IEnumerator IconFadeOut(GameObject icon)
    {
        icon.transform.Find("Icon").GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0.0f), crosshairScaleSpeed);
        yield return new WaitForSeconds(crosshairScaleSpeed);
        icon.transform.Find("Icon").gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Door"))
        //{
        //    other.transform.Find("LeftDoor").transform.DOLocalMoveZ(2.5f, 1);
        //    other.transform.Find("RightDoor").transform.DOLocalMoveZ(-0.2f, 1);
        //}
        if (other.CompareTag("Item"))
        {
            crossHair.color = new Color(0, 0, 0, 0.5f);
            crossHair.gameObject.transform.DOScale(Vector3.one * 0.2f, crosshairScaleSpeed);
            StartCoroutine(IconFadeOut(other.gameObject));
            if (other.GetComponent<Item>().items == Item.Items.KeyCard)
            {
                itemTrigger = false;
                itemObj = null;
            }
            else if (other.GetComponent<Item>().items == Item.Items.Coin)
            {
                itemTrigger = false;
                itemObj = null;
            }
        }
        if (other.CompareTag("Keypad"))
        {
            crossHair.color = new Color(0, 0, 0, 0.5f);
            crossHair.gameObject.transform.DOScale(Vector3.one * 0.2f, crosshairScaleSpeed);
            keypadActive = false;
            itemObj = null;
        }
    }

    void ItemCollecting(Item.Items items)
    {
        if(items == Item.Items.KeyCard)
        {
            itemSlot1.sprite = itemObj.GetComponent<Item>().icon;
            itemSlot1.enabled = true;
            holdingKeyCard = true;
        }
        else if (items == Item.Items.Coin)
        {
            itemSlot2.sprite = itemObj.GetComponent<Item>().icon;
            itemSlot2.enabled = true;
            coin = true;
        }
        crossHair.color = new Color(0,0,0,0.5f);
        crossHair.gameObject.transform.DOScale(Vector3.one * 0.2f, crosshairScaleSpeed);
        itemObj.SetActive(false);
        itemTrigger = false;
        itemObj = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && itemTrigger)
        {
            ItemCollecting(itemObj.GetComponent<Item>().items);
        }
        if (Input.GetKeyDown(KeyCode.E) && keypadActive && holdingKeyCard)
        {
            StartCoroutine(TurnKeytoGreen());            
            holdingKeyCard = false;
        }
        //if(ai.GetComponent<Raycast>().isActive == false)
        //    aiAgent.GetComponent<NavMeshAgent>().destination = this.gameObject.transform.position;
    }

    IEnumerator TurnKeytoGreen()
    {
        itemObj.transform.Find("KeyCard").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        itemObj.transform.Find("KeyLightRed").gameObject.SetActive(false);        
        itemObj.transform.Find("KeyLightGreen").gameObject.SetActive(true);
        OpenDoor(itemObj.transform.parent.gameObject);
        PlayerPrefs.SetString("MainDoor", "Open");
        ai.GetComponent<Raycast>().isActive = false;
    }

    public void MainDoorOpened()
    {
        GameObject.Find("KeyCard").gameObject.SetActive(false);
        GameObject.Find("Keypad").GetComponent<Collider>().enabled = false;
        GameObject.Find("Keypad").transform.Find("KeyCard").gameObject.SetActive(true);
        GameObject.Find("Keypad").transform.Find("KeyLightGreen").gameObject.SetActive(true);
        GameObject.Find("KeyLightRed").gameObject.SetActive(false);
    }
}
