using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private List<Character> avatar = new List<Character>(); //GameObject ĳ���� ��ũ��Ʈ�� ��ü

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //avatar.Add();
        //avatar.Add();
        //avatar.Add();
    }
}
