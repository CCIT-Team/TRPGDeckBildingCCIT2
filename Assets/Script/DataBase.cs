using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    public List<PlayerDefaultData> defaultData = new List<PlayerDefaultData>();
    public List<CardData> cardData = new List<CardData>();
    public List<MonsterData> monsterData = new List<MonsterData>();
    public List<PlayerType> loadTypeData = new List<PlayerType>();
    public List<PlayerStat> loadStatData = new List<PlayerStat>();
    public List<PlayerCard> loadCardData = new List<PlayerCard>();
    private const string defaultDatadbPath = "/DefaultData.db";
    private const string playerDataTable = "PlayerData";
    private const string playerDataPath_1 = "/Save/Slot1/PlayerData.db";

    private void Start()
    {
        InitDB();
    }

    private IDbConnection ConnectionDB(string path)
    {
        string currentDB = "URI=file:" + Application.streamingAssetsPath + path;
        IDbConnection dbConnection = new SqliteConnection(currentDB);        
        dbConnection.Open();
        
        return dbConnection;
    }

    private void InsertQuery(IDbConnection dbConnection, string _query)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = _query;
        dbCommand.ExecuteNonQuery();
        dbCommand.Dispose();
    }

    public void Deliver_column(string typeQuery, string statQuery)
    {
        //SaveDB(typeQuery, statQuery);
    }

    public bool IsEmptyDB()
    {
        IDbConnection dbConnection = ConnectionDB(playerDataPath_1);
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Type";

        IDataReader dataReader = dbCommand.ExecuteReader();
        dbCommand.Dispose();

        if (dataReader.IsDBNull(0))
        {
            Debug.Log(dataReader.IsDBNull(0) + " Empty DB");
            dataReader.Close();
            dbConnection.Close();
            return true;
        }
        else
        {
            Debug.Log(dataReader.IsDBNull(0) + " Not Empty DB");
            dataReader.Close();
            dbConnection.Close();
            return false;
        }
    }

    public void ResetDB()
    {
        IDbConnection dbConnection = ConnectionDB(playerDataPath_1);
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "DELETE FROM Deck";
        using (IDataReader dataReader = dbCommand.ExecuteReader())
        {
            dbCommand.Dispose();
            dataReader.Close();
            dbConnection.Close();
        }

        dbConnection = ConnectionDB(playerDataPath_1);
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "DELETE FROM Stat";
        using (IDataReader dataReader = dbCommand.ExecuteReader())
        {
            dbCommand.Dispose();
            dataReader.Close();
            dbConnection.Close();
        }

        dbConnection = ConnectionDB(playerDataPath_1);
        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "DELETE FROM Type";
        using (IDataReader dataReader = dbCommand.ExecuteReader())
        {
            dbCommand.Dispose();
            dataReader.Close();
            dbConnection.Close();
        }

        loadCardData.Clear();
        loadStatData.Clear();
        loadTypeData.Clear();
    }

    public void LoadData()
    {
        IDbConnection dbConnection = ConnectionDB(playerDataPath_1);

        string tableName = "Type";
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int playerNo = dataReader.GetInt32(0);
            string nickname = dataReader.GetString(1);
            PlayerType.Major major = (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), dataReader.GetString(2));
            PlayerType.Sex sex = (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), dataReader.GetString(3));
            PlayerType.AvatarType avatarType = (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), dataReader.GetString(4));

            loadTypeData.Add(new PlayerType(playerNo, nickname, major, sex, avatarType));
        }
        dataReader.Close();

        tableName = "Stat";
        //dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            int playerNo = dataReader.GetInt32(0);
            int strength = dataReader.GetInt32(1);
            int intelligence = dataReader.GetInt32(2);
            int luck = dataReader.GetInt32(3);
            int speed = dataReader.GetInt32(4);
            float hp = dataReader.GetFloat(5);
            float maxHp = dataReader.GetFloat(6);
            int cost = dataReader.GetInt32(7);
            int level = dataReader.GetInt32(8);
            int exp = dataReader.GetInt32(9);
            int maxExp = dataReader.GetInt32(10);
            int gold = dataReader.GetInt32(11);
            int turn = dataReader.GetInt32(12);

            loadStatData.Add(new PlayerStat(playerNo, strength, intelligence, luck, speed, hp, maxHp, cost, level, exp, maxExp, gold, turn));
        }

        dataReader.Close();

        tableName = "Deck";
        //dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();
        int[] no = new int[dataReader.FieldCount - 1];
        Debug.Log(dataReader.FieldCount);
        while (dataReader.Read())
        {
            int player = dataReader.GetInt32(0);
            for(int i = 0; i < dataReader.FieldCount-1; i++)
            {
                no[i] = dataReader.GetInt32(i+1);
            }

            loadCardData.Add(new PlayerCard(player, no));
        }

        dataReader.Close();
        dbConnection.Close();
    }

    public void SaveDB(string query)
    {
        IDbConnection dbConnection = ConnectionDB(playerDataPath_1);

        InsertQuery(dbConnection, query);

        dbConnection.Close();
    }

    private void InitDB()
    {
        string tableName = playerDataTable;

        IDbConnection dbConnection = ConnectionDB(defaultDatadbPath);
        IDbCommand dbCommand = dbConnection.CreateCommand();        
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();
        
        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            PlayerType.Major major = (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), dataReader.GetString(1));
            float hp = dataReader.GetFloat(2);
            int hpRise = dataReader.GetInt32(3);
            int strength = dataReader.GetInt32(4);
            int strengthRise = dataReader.GetInt32(5);
            int intelligence = dataReader.GetInt32(6);
            int intelligenceRise = dataReader.GetInt32(7);
            int luck = dataReader.GetInt32(8);
            int luckRise = dataReader.GetInt32(9);
            int speed = dataReader.GetInt32(10);
            int speedRise = dataReader.GetInt32(11);
            int cost = dataReader.GetInt32(12);

            int card1 = dataReader.GetInt32(13);
            int card1Count = dataReader.GetInt32(14);
            int card2 = dataReader.GetInt32(15);
            int card2Count = dataReader.GetInt32(16);
            int card3 = dataReader.GetInt32(17);
            int card3Count = dataReader.GetInt32(18);
            int weapon1 = dataReader.GetInt32(19);
            int weapon2 = dataReader.GetInt32(20);

            int level = dataReader.GetInt32(21);
            int exp = dataReader.GetInt32(22);
            int maxExp = dataReader.GetInt32(23);

            defaultData.Add(new PlayerDefaultData(no, major, hp, hpRise, strength, strengthRise, intelligence, intelligenceRise, luck, luckRise, speed, speedRise, cost, 
                card1, card1Count, card2, card2Count, card3, card3Count, weapon1, weapon2, level, exp, maxExp));
        }
        dataReader.Close();

        tableName = "Fighter_CardData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            string variableName = dataReader.GetString(2);
            CardData.CardType type = (CardData.CardType)Enum.Parse(typeof(CardData.CardType), dataReader.GetString(3));
            string description = dataReader.GetString(4);
            int defaultXvalue = dataReader.GetInt32(5);
            string effect = dataReader.GetString(6);
            int effectUseTurn = dataReader.GetInt32(7);
            int useCost = dataReader.GetInt32(8);
            int token = dataReader.GetInt32(9);

            cardData.Add(new CardData(no, name, variableName, type, description, defaultXvalue, effect, effectUseTurn, useCost, token));
        }
        dataReader.Close();

        tableName = "Wizard_CardData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            string variableName = dataReader.GetString(2);
            CardData.CardType type = (CardData.CardType)Enum.Parse(typeof(CardData.CardType), dataReader.GetString(3));
            string description = dataReader.GetString(4);
            int defaultXvalue = dataReader.GetInt32(5);
            string effect = dataReader.GetString(6);
            int effectUseTurn = dataReader.GetInt32(7);
            int useCost = dataReader.GetInt32(8);
            int token = dataReader.GetInt32(9);

            cardData.Add(new CardData(no, name, variableName, type, description, defaultXvalue, effect, effectUseTurn, useCost, token));
        }
        dataReader.Close();

        tableName = "Cleric_CardData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            string variableName = dataReader.GetString(2);
            CardData.CardType type = (CardData.CardType)Enum.Parse(typeof(CardData.CardType), dataReader.GetString(3));
            string description = dataReader.GetString(4);
            int defaultXvalue = dataReader.GetInt32(5);
            string effect = dataReader.GetString(6);
            int effectUseTurn = dataReader.GetInt32(7);
            int useCost = dataReader.GetInt32(8);
            int token = dataReader.GetInt32(9);

            cardData.Add(new CardData(no, name, variableName, type, description, defaultXvalue, effect, effectUseTurn, useCost, token));
        }
        dataReader.Close();

        tableName = "MonsterData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            int level = dataReader.GetInt32(2);
            MonsterData.Type type = (MonsterData.Type)Enum.Parse(typeof(MonsterData.Type), dataReader.GetString(3));
            float hp = dataReader.GetFloat(4);
            int strength = dataReader.GetInt32(5);
            int intelligence = dataReader.GetInt32(6);
            int luck = dataReader.GetInt32(7);
            int speed = dataReader.GetInt32(8);

            int action1 = dataReader.GetInt32(9);
            int action2 = dataReader.GetInt32(10);
            int action3 = dataReader.GetInt32(11);

            int giveExp = dataReader.GetInt32(12);
            int dropGold = dataReader.GetInt32(13);

            int dropitem1 = dataReader.GetInt32(14);
            int dropitem1Percentage = dataReader.GetInt32(15);
            int dropitem2 = dataReader.GetInt32(16);
            int dropitem2Percentage = dataReader.GetInt32(17);
            int dropitem3 = dataReader.GetInt32(18);
            int dropitem3Percentage = dataReader.GetInt32(19);

            monsterData.Add(new MonsterData(no, name, level, type, hp, strength, intelligence, luck, speed, action1, action2, action3, giveExp, dropGold, dropitem1, dropitem1Percentage, dropitem2, dropitem2Percentage, dropitem3, dropitem3Percentage));
        }
        dataReader.Close();
        dbConnection.Close();
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
        Wizard,
        Cleric
    }

    public enum Sex
    {
        Male,
        Female
    }

    public int playerNum;
    public string nickname;
    [SerializeField] public Major major;
    [SerializeField] public Sex sex;
    [SerializeField] public AvatarType type;
    public Color skinColor;

    public PlayerType(int _playerNum, string _nickname, Major _major, Sex _sex, AvatarType _avatartype)
    {
        playerNum = _playerNum;
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
    public int playerNum;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;
    public float hp;
    public float maxHp;
    public int cost;
    public int level;
    public int exp;
    public int maxExp;
    public int gold;
    public bool turn;
    public PlayerStat(int _playerNum, int _strength, int _intelligence, int _luck, int _speed, float _hp, float _maxHp, int _cost, int _level, int _exp, int _maxExp, int _gold, int _turn)
    {
        playerNum = _playerNum;
        strength = _strength;
        intelligence = _intelligence;
        luck = _luck;
        speed = _speed;
        hp = _hp;
        maxHp = _maxHp;
        cost = _cost;
        level = _level;
        exp = _exp;
        maxExp = _maxExp;
        gold = _gold;
        turn = Convert.ToBoolean(_turn);
    }
}
#endregion

#region 플레이어 카드
[Serializable]
public class PlayerCard
{
    public int playerNum;
    public int[] no = new int[40];

    public PlayerCard(int _playerNum, int[] _no)
    {
        for(int i = 0; i < no.Length; i++)
        {
            no[i] = 0;
        }

        playerNum = _playerNum;
        for (int i = 0; i < _no.Length; i++)
        {
            no[i] = _no[i];
        }
    }
}
#endregion

#region 플레이어 기본 세팅데이터
[Serializable]
public class PlayerDefaultData
{
    public int no;
    public PlayerType.Major major;
    public float hp;
    public int hpRise;
    public int strength;
    public int strengthRise;
    public int intelligence;
    public int intelligenceRise;
    public int luck;
    public int luckRise;
    public int speed;
    public int speedRise;
    public int cost;

    public int card1;
    public int card1Count;
    public int card2;
    public int card2Count;
    public int card3;
    public int card3Count;

    public int weapon1;
    public int weapon2;

    public int level;
    public int exp;
    public int maxExp;


    public PlayerDefaultData(int _no, PlayerType.Major _major, float _hp, int _hpRise, int _strength, int _strengthRise, int _intelligence, int _intelligenceRise, int _luck, int _luckRise, int _speed, int _speedRise, int _cost,
                            int _card1, int _card1Count, int _card2, int _card2Count, int _card3, int _card3Count, int _weapon1, int _weapon2, int _level, int _exp, int _maxExp)
    {
        no = _no;
        major = _major;
        hp = _hp;
        hpRise = _hpRise; 
        strength = _strength;
        strengthRise = _strengthRise;
        intelligence = _intelligence;
        intelligenceRise = _intelligenceRise;
        luck = _luck;
        luckRise = _luckRise;
        speed = _speed;
        speedRise = _speedRise;
        cost = _cost;

        card1 = _card1;
        card1Count = _card1Count;
        card2 = _card2;
        card2Count = _card2Count;
        card3 = _card3;
        card3Count = _card3Count;

        weapon1 = _weapon1;
        weapon2 = _weapon2;

        level = _level;
        exp = _exp;
        maxExp = _maxExp;
    }
}
#endregion

#region 카드 데이터
[Serializable]
public class CardData
{
    public enum CardType
    {
        Single_Attack,
        Multi_Attack,
        All_Attack,

        Single_Defence,
        Multi_Defence,
        All_Denfence,

        Single_Increase,
        Multi_Increase,
        All_Increase,

        Single_Endow,
        Multi_Endow,
        All_Endow,

        Card_Draw
    }
    public int no;
    public string name;
    public string variableName;
    public CardType type;
    public string description;
    public int defaultXvalue;
    public string effect;
    public int effectUseTurn;
    public int useCost;
    public int token;

    public CardData(int _no, string _name, string _variableName, CardType _type, string _description, int _defaultXvalue, string _effect, int _effectUseTurn, int _useCost, int _token)
    {
        no = _no;
        name = _name;
        variableName = _variableName;
        type = _type;
        description = _description;
        defaultXvalue = _defaultXvalue;
        effect = _effect;
        effectUseTurn = _effectUseTurn;
        useCost = _useCost;
        token = _token;
    }
}
#endregion

#region 몬스터 데이터
[Serializable]
public class MonsterData
{
    public enum Type
    {
        Undead
    }
    public int no;
    public string name;
    public int level;
    public Type type;
    public float hp;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;
    public int action1;
    public int action2;
    public int action3;
    public int giveExp;
    public int dropGold;
    public int dropitem1;
    public int dropitem1Percentage;
    public int dropitem2;
    public int dropitem2Percentage;
    public int dropitem3;
    public int dropitem3Percentage;

    public MonsterData(int _no, string _name, int _level, Type _type, float _hp, int _strength, int _intelligence, int _luck, int _speed, 
        int _action1, int _action2, int _action3, int _giveExp, int _dropGold, 
        int _dropitem1, int _dropitem1Percentage, int _dropitem2, int _dropitem2Percentage, int _dropitem3, int _dropitem3Percentage)
    {
        no = _no;
        name = _name;
        level = _level;
        type = _type;
        hp = _hp;
        strength = _strength;
        intelligence = _intelligence;
        luck = _luck;
        speed = _speed;

        action1 = _action1;
        action2 = _action2;
        action3 = _action3;

        giveExp = _giveExp;
        dropGold = _dropGold;

        dropitem1 = _dropitem1;
        dropitem1Percentage = _dropitem1Percentage;
        dropitem2 = _dropitem2;
        dropitem2Percentage = _dropitem2Percentage;
        dropitem3 = _dropitem3;
        dropitem3Percentage = _dropitem3Percentage;
    }
}
#endregion