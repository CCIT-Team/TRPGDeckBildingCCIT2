using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataBase : MonoBehaviour
{
    public static DataBase instance = null;
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
    public List<PlayerStat> stat = new List<PlayerStat>();
    private const string statPath = "/StatDB.csv";


    private void Start()
    {
        LoadStatDB(statPath);
    }

    private void LoadStatDB(string path)
    {
        string[] statDB = File.ReadAllLines(Application.streamingAssetsPath + path);

        for(int i = 1; i < statDB.Length; i++)
        {
            stat.Add(new PlayerStat(statDB[i].Split(',')));
        }
    }
}


[Serializable]
public class PlayerStat
{
    public enum AvatarType
    {
        None,
        Human,
        Elf,
        DarkElf,
        HalfOrc
    }

    public enum Major
    {
        None,
        Fighter,
        Magician,
        Cleric
    }

    public int playerNum;
    [SerializeField] public AvatarType type;
    [SerializeField] public Major major;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public float hp;
    public float atk;
    public float def;
    public int cost;

    public PlayerStat(string[] data)
    {
        int count = 0;
        playerNum = int.Parse(data[count++]);
        type = (AvatarType)Enum.Parse(typeof(AvatarType), data[count++]);
        major = (Major)Enum.Parse(typeof(Major), data[count++]);
        strength = int.Parse(data[count++]);
        intelligence = int.Parse(data[count++]);
        luck = int.Parse(data[count++]);
        speed = int.Parse(data[count++]);
        hp = float.Parse(data[count++]);
        atk = float.Parse(data[count++]);
        def = float.Parse(data[count++]);
        cost = int.Parse(data[count++]);
    }
}
