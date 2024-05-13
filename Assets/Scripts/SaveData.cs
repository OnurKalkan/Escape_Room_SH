using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public GameObject doors;
    public bool resetData, mainDoor;


    private void Awake()
    {
        if (resetData)
        {
            PlayerPrefs.DeleteAll();
            //PlayerPrefs.DeleteKey("MainDoor");
        }
        if (mainDoor)
        {
            PlayerPrefs.SetString("MainDoor","Open");
        }
        InitialiseLevel();        
    }

    public void ResetLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    void InitialiseLevel()
    {
        if (PlayerPrefs.GetString("MainDoor","Close") == "Open")
        {
            doors.transform.Find("LeftDoor").transform.DOLocalMoveZ(4.5f, 0);
            doors.transform.Find("RightDoor").transform.DOLocalMoveZ(-2.5f, 0);
            GameObject.Find("AI").GetComponent<Raycast>().isActive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MainDoorOpened();
        }
    }

}
