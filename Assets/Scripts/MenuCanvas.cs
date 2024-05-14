using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public int menuNo = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Canvas>().sortingOrder = 0;
            transform.GetChild(i).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        transform.GetChild(0).GetComponent<Canvas>().sortingOrder = 1;
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    }

    public void MenuSwitching(Canvas myCanvas)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Canvas>().sortingOrder = 0;
            transform.GetChild(i).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        menuNo = myCanvas.transform.GetSiblingIndex();
        myCanvas.sortingOrder = 1;
        myCanvas.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
}
