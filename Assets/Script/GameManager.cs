using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private List<Character> avatar = new List<Character>(); //GameObject 캐릭터 스크립트로 교체

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
