using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretInfo : MonoBehaviour
{
    public Sprite icon;
    public SecretName secretName;
    public int secretNo;
    public string secretInfo;

    public enum SecretName
    {        
        SecretOfTheTomb,
        PreciousGem,
        SecretOfTheBear
    }
}
