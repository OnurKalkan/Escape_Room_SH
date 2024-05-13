using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject resetButton;
    public int escNo = 0;
    // Start is called before the first frame update
    void Start()
    {
        resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) && escNo == 0)
        //{
        //    escNo++;
        //    resetButton.SetActive(true);
        //    Cursor.visible = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape) && escNo == 1)
        //{
        //    escNo--;
        //    resetButton.SetActive(false);
        //    Cursor.visible = false;
        //}

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    resetButton.SetActive(!resetButton.activeSelf);
        //    //Cursor.visible = true;
        //}

        //if (Input.GetKeyDown(KeyCode.Escape) && resetButton.activeInHierarchy)
        //{
        //    resetButton.SetActive(false);
        //    Cursor.visible = false;
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape) && !resetButton.activeInHierarchy)
        //{
        //    resetButton.SetActive(true);
        //    Cursor.visible = true;
        //}
    }
}
