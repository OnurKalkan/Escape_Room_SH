using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int levelNo, strength, hp, stamina, mind, power, intelligence, charisma, skillPoint;
    public TextMeshProUGUI strengthText, hpText;

    private void Awake()
    {
        //levelNo = PlayerPrefs.GetInt("LevelNo", 7);
        //levelNo = PlayerPrefs.GetInt("LevelNo", 1);
        //levelNo = PlayerPrefs.GetInt("LevelNo", 1);
        //levelNo = PlayerPrefs.GetInt("LevelNo", 1);
        //levelNo = PlayerPrefs.GetInt("LevelNo", 1);
        //levelNo = PlayerPrefs.GetInt("LevelNo", 1);
        //levelNo = PlayerPrefs.GetInt("LevelNo", 1);
        hp = PlayerPrefs.GetInt("HP", 5);
        stamina = PlayerPrefs.GetInt("Stamina", 5);
    }
    // Start is called before the first frame update
    void Start()
    {
        strengthText.text = "Strength: " + strength.ToString();
        transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level: " + levelNo.ToString();
        transform.Find("HP").GetComponent<TextMeshProUGUI>().text = "HP: " + hp.ToString();
        transform.Find("Stamina").GetComponent<TextMeshProUGUI>().text = "Stamina: " + stamina.ToString();
        transform.Find("Mind").GetComponent<TextMeshProUGUI>().text = "Mind: " + mind.ToString();
        transform.Find("Power").GetComponent<TextMeshProUGUI>().text = "Power: " + power.ToString();
        transform.Find("Intelligence").GetComponent<TextMeshProUGUI>().text = "Intelligence: " + intelligence.ToString();
        transform.Find("Charisma").GetComponent<TextMeshProUGUI>().text = "Charisma: " + charisma.ToString();
    }

    public void IncreaseStat(string statName)
    {
        if(skillPoint > 0)
        {
            skillPoint--;
            PlayerPrefs.SetInt(statName, PlayerPrefs.GetInt(statName) + 1);
            transform.Find(statName).GetComponent<TextMeshProUGUI>().text = statName + ": " + PlayerPrefs.GetInt(statName).ToString();
        }
        else
        {
            //butonlari inaktif edebilirsin
        }
        
    }
}
