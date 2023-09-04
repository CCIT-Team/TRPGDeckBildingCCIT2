using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]private List<Character> avatar = new List<Character>(); //GameObject 캐릭터 스크립트로 교체

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
        avatar[0].SetUnitData(DataBase.instance.stat[0]);
        avatar[1].SetUnitData(DataBase.instance.stat[1]);
        avatar[2].SetUnitData(DataBase.instance.stat[2]);
    }
}
