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
    private const string StatdbPath = "/StatDB.db";
    private const string table = "BasicStat";
    private const string playerDataPath_1 = "/Save/Slot1/PlayerData.db";

    private void Start()
    {
        ConnectionDB(StatdbPath);
        GameManager.instance.GetLobbyAvatar(); //test용
    }


    private void ConnectionDB(string path)
    {
        string statDB = "URI=file:" + Application.streamingAssetsPath + path;
        IDbConnection dbConnection = new SqliteConnection(statDB);
        dbConnection.Open();

        LoadStartStatDB(dbConnection);
    }

    public void Deliver_column(string typeQuery, string statQuery)
    {
        UpdateDB(playerDataPath_1, typeQuery, statQuery); //디비 경로 설정
    }

    private void UpdateDB(string path, string typeQuery, string statQuery)
    {
        string playerdataDB = "URI=file:" + Application.streamingAssetsPath + path;
        IDbConnection dbConnection = new SqliteConnection(playerdataDB);
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = typeQuery;
        dbCommand.ExecuteNonQuery();
        dbCommand.Dispose();


        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = statQuery;
        dbCommand.ExecuteNonQuery();
        dbCommand.Dispose();
        //스텟데이터 칼럼 추가

        dbConnection.Close();
    }

    private void LoadStartStatDB(IDbConnection dbConnection)
    {
        string tableName = table;

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while(dataReader.Read())
        {
            PlayerType.Major major = (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), dataReader.GetString(0));
            int strength = dataReader.GetInt32(1);
            int intelligence = dataReader.GetInt32(2);
            int luck = dataReader.GetInt32(3);
            int speed = dataReader.GetInt32(4);
            float hp = dataReader.GetFloat(5);
            float atk = dataReader.GetFloat(6);
            float ap = dataReader.GetFloat(7);
            float def = dataReader.GetFloat(8);
            float apdef = dataReader.GetFloat(9);
            int cost = dataReader.GetInt32(10);

            stat.Add(new PlayerStat(major, strength, intelligence, luck, speed, hp, atk, ap, def, apdef, cost));
        }
        dataReader.Close();
    }
}

#region 플레이어 타입

[Serializable]
public class PlayerType
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
        Female
    }

    public string nickname;
    [SerializeField] public Major major;
    [SerializeField] public Sex sex;
    [SerializeField] public AvatarType type;
    public Color skinColor;

    public PlayerType(string _nickname, Major _major, Sex _sex, AvatarType _avatartype)
    {
        nickname = _nickname;
        major = _major;
        sex = _sex;
        //skinColor = _skinColor;
    }
}

#endregion

#region 플레이어 스텟

[Serializable]
public class PlayerStat
{
    public PlayerType.Major major;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public float hp;
    public float atk;
    public float ap;
    public float def;
    public float apdef;
    public int cost;

    public PlayerStat(PlayerType.Major _major, int _strength, int _intelligence, int _luck, int _speed, float _hp, float _atk, float _ap, float _def, float _apdef, int _cost)
    {
        major = _major;
        strength = _strength;
        intelligence = _intelligence;
        luck = _luck;
        speed = _speed;
        hp = _hp;
        atk = _atk;
        ap = _ap;
        def = _def;
        apdef = _apdef;
        cost = _cost;
    }
}

#endregion
