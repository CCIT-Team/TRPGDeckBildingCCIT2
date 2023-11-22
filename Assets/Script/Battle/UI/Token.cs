using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    [SerializeField]
    Image image;
    public Sprite[] token_Success;
    public Sprite[] token_Fail;


    public void CheckToken(StatusType type ,bool isSuccess)
    {
        int x = (int) type;
        if (isSuccess)
            image.sprite = token_Success[x];
        else
            image.sprite = token_Fail[x];
    }
}

public enum StatusType { Intelligence, Luck, Speed, Strength }