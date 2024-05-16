using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int level, strength, hp, stamina, mind, power, intelligence, charisma, skillPoint;
    public TextMeshProUGUI strengthText, hpText;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("HP");
        hp = PlayerPrefs.GetInt("HP", 2);
        stamina = PlayerPrefs.GetInt("Stamina", 3);
        strength = PlayerPrefs.GetInt("Strength", 3);
        mind = PlayerPrefs.GetInt("Mind", 3);
        power = PlayerPrefs.GetInt("Power", 2);
        intelligence = PlayerPrefs.GetInt("Intelligence", 4);
        charisma = PlayerPrefs.GetInt("Charisma", 2);
        skillPoint = PlayerPrefs.GetInt("SkillPoint", 1);
        level = hp + stamina + strength + mind + power + charisma + intelligence;
    }

    // Start is called before the first frame update
    void Start()
    {
        strengthText.text = "Strength: " + strength.ToString();
        transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level: " + level.ToString();
        transform.Find("HP").GetComponent<TextMeshProUGUI>().text = "HP: " + hp.ToString();
        transform.Find("Stamina").GetComponent<TextMeshProUGUI>().text = "Stamina: " + stamina.ToString();
        transform.Find("Mind").GetComponent<TextMeshProUGUI>().text = "Mind: " + mind.ToString();
        transform.Find("Power").GetComponent<TextMeshProUGUI>().text = "Power: " + power.ToString();
        transform.Find("Intelligence").GetComponent<TextMeshProUGUI>().text = "Intelligence: " + intelligence.ToString();
        transform.Find("Charisma").GetComponent<TextMeshProUGUI>().text = "Charisma: " + charisma.ToString();
        transform.Find("SkillPoint").GetComponent<TextMeshProUGUI>().text = "SkillPoint: " + skillPoint.ToString();
        if (skillPoint <= 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
    }

    public void IncreaseStat(string statName)
    {
        if(skillPoint > 0)
        {
            skillPoint--;
            PlayerPrefs.SetInt("SkillPoint", skillPoint);
            PlayerPrefs.SetInt(statName, PlayerPrefs.GetInt(statName) + 1);
            transform.Find(statName).GetComponent<TextMeshProUGUI>().text = statName + ": " + PlayerPrefs.GetInt(statName).ToString();
        }
        if (skillPoint <= 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
        level = hp + stamina + strength + mind + power + charisma + intelligence;
        transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level: " + level.ToString();
        transform.Find("SkillPoint").GetComponent<TextMeshProUGUI>().text = "SkillPoint: " + skillPoint.ToString();
    }
}
