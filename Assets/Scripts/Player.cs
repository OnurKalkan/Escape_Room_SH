using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool holdingKeyCard, coin, keypadActive;
    public bool itemTrigger;
    public GameObject itemObj = null;
    public Image itemSlot1, itemSlot2, crossHair;
    float crosshairScaleSpeed = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Door") && holdingKeyCard)
        //{
        //    other.transform.Find("LeftDoor").transform.DOLocalMoveZ(4.5f, 1);
        //    other.transform.Find("RightDoor").transform.DOLocalMoveZ(-2.5f, 1);
        //}
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
    }

    IEnumerator TurnKeytoGreen()
    {
        itemObj.transform.Find("KeyCard").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        itemObj.transform.Find("KeyLightRed").gameObject.SetActive(false);        
        itemObj.transform.Find("KeyLightGreen").gameObject.SetActive(true);
        OpenDoor(itemObj.transform.parent.gameObject);
    }
}
