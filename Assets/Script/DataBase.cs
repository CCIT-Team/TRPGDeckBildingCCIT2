using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;

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
    private const string statPath = "/StatDB.csv";  //csv파일 읽기 방식
    private const string dbPath = "/testDB.db";


    private void Start()
    {
        //LoadStatDB(statPath);
        ConnectionTestDB(dbPath);
    }

    private void LoadStatDB(string path) //csv파일 읽기 방식
    {
        string[] statDB = File.ReadAllLines(Application.streamingAssetsPath + path);

        for(int i = 1; i < statDB.Length; i++)
        {
            stat.Add(new PlayerStat(statDB[i].Split(',')));
        }
    }

    private void ConnectionTestDB(string path)
    {
        string testDB = "URI=file:" + Application.streamingAssetsPath + path;
        IDbConnection dbConnection = new SqliteConnection(testDB);
        dbConnection.Open();

        LoadTestDB(dbConnection);
    }

    private void LoadTestDB(IDbConnection dbConnection)
    {
        string tableName = "StatDB";

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while(dataReader.Read())
        {
            int playerNum = dataReader.GetInt32(0);
            PlayerStat.AvatarType type = (PlayerStat.AvatarType)Enum.Parse(typeof(PlayerStat.AvatarType), dataReader.GetString(1));
            PlayerStat.Major major = (PlayerStat.Major)Enum.Parse(typeof(PlayerStat.Major), dataReader.GetString(2));
            int strength = dataReader.GetInt32(3);
            int intelligence = dataReader.GetInt32(4);
            int luck = dataReader.GetInt32(5);
            int speed = dataReader.GetInt32(6);
            float hp = dataReader.GetFloat(7);
            float atk = dataReader.GetFloat(8);
            float def = dataReader.GetFloat(9);
            int cost = dataReader.GetInt32(10);

            stat.Add(new PlayerStat(playerNum, type, major, strength, intelligence, luck, speed, hp, atk, def, cost));
        }
        dataReader.Close();
    }
}


[Serializable]
public class PlayerStat
{
    public enum AvatarType
    {
        Human,
        Elf,
        DarkElf,
        HalfOrc
    }

    public enum Major
    {
        Fighter,
        Magician,
        Cleric
    }

    public enum Sex
    {
        Male,
        female
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

    public PlayerStat(string[] data) //csv파일 읽기 방식
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
    public PlayerStat(int _playerNum, AvatarType _type, Major _major, int _strength, int _intelligence, int _luck, int _speed, float _hp, float _atk, float _def, int _cost)
    {
        playerNum = _playerNum;
        type = _type;
        major = _major;
        strength = _strength;
        intelligence = _intelligence;
        luck = _luck;
        speed = _speed;
        hp = _hp;
        atk = _atk;
        def = _def;
        cost = _cost;
    }
}
