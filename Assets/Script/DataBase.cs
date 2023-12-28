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

    public List<CardData> fighterCardData = new List<CardData>();
    public List<CardData> wizardCardData = new List<CardData>();
    public List<CardData> clericCardData = new List<CardData>();

    public List<MonsterData> monsterData = new List<MonsterData>();
    public List<WeaponData> weaponData = new List<WeaponData>();
    public List<ArmorData> armorData = new List<ArmorData>();
    public List<ItemData> itemData = new List<ItemData>();
    public List<PlayerType> loadTypeData = new List<PlayerType>();
    public List<PlayerPosition> loadPositionData = new List<PlayerPosition>();
    public List<PlayerStat> loadStatData = new List<PlayerStat>();
    public List<PlayerCard> loadCardData = new List<PlayerCard>();

    private List<WeaponData> tempWeaponData = new List<WeaponData>();
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

    public WeaponData SelectWeapon(int weaponNo)
    {
        tempWeaponData.Clear();
        IDbConnection dbConnection = ConnectionDB(defaultDatadbPath);
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "SELECT * FROM WeaponData Where no = " + weaponNo.ToString();

        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            WeaponData.Type type = (WeaponData.Type)Enum.Parse(typeof(WeaponData.Type), dataReader.GetString(2));
            Grade grade = (Grade)Enum.Parse(typeof(Grade), dataReader.GetString(3));
            int level = dataReader.GetInt32(4);
            WeaponData.EquipType equipType = (WeaponData.EquipType)Enum.Parse(typeof(WeaponData.EquipType), dataReader.GetString(5));
            int strength = dataReader.GetInt32(6);
            int intelligence = dataReader.GetInt32(7);
            int luck = dataReader.GetInt32(8);
            int speed = dataReader.GetInt32(9);

            int getCard1 = dataReader.GetInt32(10);
            int getCard1Count = dataReader.GetInt32(11);

            int getCard2 = dataReader.GetInt32(12);
            int getCard2Count = dataReader.GetInt32(13);

            int getCard3 = dataReader.GetInt32(14);
            int getCard3Count = dataReader.GetInt32(15);

            int getCard4 = dataReader.GetInt32(16);
            int getCard4Count = dataReader.GetInt32(17);

            int getCard5 = dataReader.GetInt32(18);
            int getCard5Count = dataReader.GetInt32(19);

            int getCard6 = dataReader.GetInt32(20);
            int getCard6Count = dataReader.GetInt32(21);

            int getCard7 = dataReader.GetInt32(22);
            int getCard7Count = dataReader.GetInt32(23);

            int getCard8 = dataReader.GetInt32(24);
            int getCard8Count = dataReader.GetInt32(25);

            int buyGold = dataReader.GetInt32(26);
            int sellGold = dataReader.GetInt32(27);

            tempWeaponData.Add(new WeaponData(no, name, type, grade, level, equipType, strength, intelligence, luck, speed,
                getCard1, getCard1Count, getCard2, getCard2Count, getCard3, getCard3Count, getCard4, getCard4Count, getCard5, getCard5Count, getCard6, getCard6Count, getCard7, getCard7Count, getCard8, getCard8Count,
                buyGold, sellGold));
        }

        dataReader.Close();
        dbConnection.Close();
        return tempWeaponData[0];
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
            //Debug.Log(dataReader.IsDBNull(0) + " Empty DB");
            dataReader.Close();
            dbConnection.Close();
            return true;
        }
        else
        {
            //Debug.Log(dataReader.IsDBNull(0) + " Not Empty DB");
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

        dbCommand.CommandText = "DELETE FROM Position";
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
        loadPositionData.Clear();
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
            PlayerType.Gender gender = (PlayerType.Gender)Enum.Parse(typeof(PlayerType.Gender), dataReader.GetString(3));
            PlayerType.AvatarType avatarType = (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), dataReader.GetString(4));
            float skinColor = float.Parse(dataReader.GetString(5));
            float eyeColor = float.Parse(dataReader.GetString(6));

            loadTypeData.Add(new PlayerType(playerNo, nickname, major, gender, avatarType, skinColor, eyeColor));
        }
        dataReader.Close();

        tableName = "Position";
        //dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            int playerNo = dataReader.GetInt32(0);
            float positionX = float.Parse(dataReader.GetString(1));
            float positionY = float.Parse(dataReader.GetString(2));
            float positionZ = float.Parse(dataReader.GetString(3));

            loadPositionData.Add(new PlayerPosition(playerNo, positionX, positionY, positionZ));
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
            int portionRegular = dataReader.GetInt32(12);
            int portionLarge = dataReader.GetInt32(13);
            int turn = dataReader.GetInt32(14);

            loadStatData.Add(new PlayerStat(playerNo, strength, intelligence, luck, speed, hp, maxHp, cost, level, exp, maxExp, gold, portionRegular, portionLarge, turn));
        }

        dataReader.Close();

        tableName = "Deck";
        //dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();
        int[] no = new int[dataReader.FieldCount - 1];
        //Debug.Log(dataReader.FieldCount);
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
            int gold = dataReader.GetInt32(22);
            int exp = dataReader.GetInt32(23);
            int maxExp = dataReader.GetInt32(24);

            defaultData.Add(new PlayerDefaultData(no, major, hp, hpRise, strength, strengthRise, intelligence, intelligenceRise, luck, luckRise, speed, speedRise, cost, 
                card1, card1Count, card2, card2Count, card3, card3Count, weapon1, weapon2, level, gold, exp, maxExp));
        }
        dataReader.Close();

        LoadCardData(dbCommand, dataReader);

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

            string appearanceArea1 = dataReader.GetString(20);
            string appearanceArea2 = dataReader.GetString(21);
            string appearanceArea3 = dataReader.GetString(22);

            monsterData.Add(new MonsterData(no, name, level, type, hp, strength, intelligence, luck, speed, action1, action2, action3, giveExp, dropGold, dropitem1, dropitem1Percentage, dropitem2, dropitem2Percentage, dropitem3, dropitem3Percentage, appearanceArea1, appearanceArea2, appearanceArea3));
        }
        dataReader.Close();

        tableName = "WeaponData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            WeaponData.Type type = (WeaponData.Type)Enum.Parse(typeof(WeaponData.Type), dataReader.GetString(2));
            Grade grade = (Grade)Enum.Parse(typeof(Grade), dataReader.GetString(3));
            int level = dataReader.GetInt32(4);
            WeaponData.EquipType equipType = (WeaponData.EquipType)Enum.Parse(typeof(WeaponData.EquipType), dataReader.GetString(5));
            int strength = dataReader.GetInt32(6);
            int intelligence = dataReader.GetInt32(7);
            int luck = dataReader.GetInt32(8);
            int speed = dataReader.GetInt32(9);

            int getCard1 = dataReader.GetInt32(10);
            int getCard1Count = dataReader.GetInt32(11);

            int getCard2 = dataReader.GetInt32(12);
            int getCard2Count = dataReader.GetInt32(13);

            int getCard3 = dataReader.GetInt32(14);
            int getCard3Count = dataReader.GetInt32(15);

            int getCard4 = dataReader.GetInt32(16);
            int getCard4Count = dataReader.GetInt32(17);

            int getCard5 = dataReader.GetInt32(18);
            int getCard5Count = dataReader.GetInt32(19);

            int getCard6 = dataReader.GetInt32(20);
            int getCard6Count = dataReader.GetInt32(21);

            int getCard7 = dataReader.GetInt32(22);
            int getCard7Count = dataReader.GetInt32(23);

            int getCard8 = dataReader.GetInt32(24);
            int getCard8Count = dataReader.GetInt32(25);

            int buyGold = dataReader.GetInt32(26);
            int sellGold = dataReader.GetInt32(27);

            weaponData.Add(new WeaponData(no, name, type, grade, level, equipType, strength, intelligence, luck, speed,
                getCard1, getCard1Count, getCard2, getCard2Count, getCard3, getCard3Count, getCard4, getCard4Count, getCard5, getCard5Count, getCard6, getCard6Count, getCard7, getCard7Count, getCard8, getCard8Count,
                buyGold, sellGold));
        }
        dataReader.Close();

        tableName = "ArmorData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            ArmorData.Type type = (ArmorData.Type)Enum.Parse(typeof(ArmorData.Type), dataReader.GetString(2));
            Grade grade = (Grade)Enum.Parse(typeof(Grade), dataReader.GetString(3));
            int level = dataReader.GetInt32(4);
            int strength = dataReader.GetInt32(5);
            int intelligence = dataReader.GetInt32(6);
            int luck = dataReader.GetInt32(7);
            int speed = dataReader.GetInt32(8);

            int getCard1 = dataReader.GetInt32(9);
            int getCard1Count = dataReader.GetInt32(10);

            int getCard2 = dataReader.GetInt32(11);
            int getCard2Count = dataReader.GetInt32(12);

            int getCard3 = dataReader.GetInt32(13);
            int getCard3Count = dataReader.GetInt32(14);

            int getCard4 = dataReader.GetInt32(15);
            int getCard4Count = dataReader.GetInt32(16);

            int buyGold = dataReader.GetInt32(17);
            int sellGold = dataReader.GetInt32(18);

            armorData.Add(new ArmorData(no, name, type, grade, level, strength, intelligence, luck, speed,
                getCard1, getCard1Count, getCard2, getCard2Count, getCard3, getCard3Count, getCard4, getCard4Count,
                buyGold, sellGold));
        }
        dataReader.Close();

        tableName = "ItemData";
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            int no = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            string effect = dataReader.GetString(2);

            int buyGold = dataReader.GetInt32(3);
            int sellGold = dataReader.GetInt32(4);
            int useCost = dataReader.GetInt32(5);

            itemData.Add(new ItemData(no, name, effect, buyGold, sellGold, useCost));
        }
        dataReader.Close();

        dbConnection.Close();
    }
    private void LoadCardData(IDbCommand dbCommand, IDataReader dataReader)
    {
        //string[] tableNames = { "Fighter_CardDeck", "Wizard_CardDeck", "Cleric_CardDeck", "OnehandSword_CardData", "TwohandSword_CardData" };
        string[] tableNames = { "Axe_CardData", "Mace_CardData", "Shield_CardData", "StaffWand_CardData", "TwohandSword_CardData", "Culb_CardData", "OnehandSword_CardData", "Hammer_CardData" };

        for (int i = 0; i < tableNames.Length; i++)
        {
            dbCommand.CommandText = "SELECT * FROM " + tableNames[i];
            dataReader = dbCommand.ExecuteReader();

            while (dataReader.Read())
            {
                int no = dataReader.GetInt32(0);
                string name = dataReader.GetString(1);
                string variableName = dataReader.GetString(2);
                CardData.SkillType skillType = (CardData.SkillType)Enum.Parse(typeof(CardData.SkillType), dataReader.GetString(3));
                CardData.AttackType attackType = (CardData.AttackType)Enum.Parse(typeof(CardData.AttackType), dataReader.GetString(4));
                CardData.UseStatType useStatType = (CardData.UseStatType)Enum.Parse(typeof(CardData.UseStatType), dataReader.GetString(5));
                string description = dataReader.GetString(6);
                int defaultXvalue = dataReader.GetInt32(7);
                string effect = dataReader.GetString(8);
                int effectUseTurn = dataReader.GetInt32(9);
                int useCost = dataReader.GetInt32(10);
                int token = dataReader.GetInt32(11);


                cardData.Add(new CardData(no, name, variableName, skillType, attackType, useStatType, description, defaultXvalue, effect, effectUseTurn, useCost, token));

                //if (tableNames[i] == "Fighter_CardDeck")
                //{
                //    fighterCardData.Add(new CardData(no, name, variableName, type, description, defaultXvalue, effect, effectUseTurn, useCost, token));
                //}
                //else if (tableNames[i] == "Wizard_CardDeck")
                //{
                //    wizardCardData.Add(new CardData(no, name, variableName, type, description, defaultXvalue, effect, effectUseTurn, useCost, token));
                //}
                //else if (tableNames[i] == "Cleric_CardDeck")
                //{
                //    clericCardData.Add(new CardData(no, name, variableName, type, description, defaultXvalue, effect, effectUseTurn, useCost, token));
                //}
                //else
                //{
                //    cardData.Add(new CardData(no, name, variableName, skillType, attackType, useStatType, description, defaultXvalue, effect, effectUseTurn, useCost, token));
                //}
            }

            dataReader.Close();
        }
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

    public enum Gender
    {
        Male,
        Female
    }

    public int playerNum;
    public string nickname;
    [SerializeField] public Major major;
    [SerializeField] public Gender gender;
    [SerializeField] public AvatarType type;
    public float skinColor;
    public float eyeColor;

    public PlayerType(int _playerNum, string _nickname, Major _major, Gender _gender, AvatarType _avatartype, float _skinColor, float _eyeColor)
    {
        playerNum = _playerNum;
        nickname = _nickname;
        major = _major;
        gender = _gender;
        type = _avatartype;
        skinColor = _skinColor;
        eyeColor = _eyeColor;
    }
}
#endregion

#region 플레이어 포지션
[Serializable]
public class PlayerPosition
{
    public int playerNum;
    public float positionX;
    public float positionY;
    public float positionZ;

    public PlayerPosition(int _playerNum, float _positionX, float _positionY, float _positionZ)
    {
        playerNum = _playerNum;
        positionX = _positionX;
        positionY = _positionY;
        positionZ = _positionZ;
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
    public int portionRegular;
    public int portionLarge;
    public bool turn;
    public PlayerStat(int _playerNum, int _strength, int _intelligence, int _luck, int _speed, float _hp, float _maxHp, int _cost, int _level, int _exp, int _maxExp, int _gold, int _portionRegular, int _portionLarge, int _turn)
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
        portionRegular = _portionRegular;
        portionLarge = _portionLarge;
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
    public int gold;
    public int exp;
    public int maxExp;


    public PlayerDefaultData(int _no, PlayerType.Major _major, float _hp, int _hpRise, int _strength, int _strengthRise, int _intelligence, int _intelligenceRise, int _luck, int _luckRise, int _speed, int _speedRise, int _cost,
                            int _card1, int _card1Count, int _card2, int _card2Count, int _card3, int _card3Count, int _weapon1, int _weapon2, int _level, int _gold, int _exp, int _maxExp)
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
        gold = _gold;
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
        SingleAttack,
        MultiAttack,
        AllAttack,

        SingleDefence,
        MultiDefence,
        AllDenfence,

        SingleIncrease,
        MultiIncrease,
        AllIncrease,

        SingleEndow,
        MultiEndow,
        AllEndow,

        CardDraw
    }

    public enum SkillType
    {
        SingleEnemy,
        MultiEnemy,
        All,

        SingleMyself,
        MultiMyself,

        SingleFriendly
    }

    public enum AttackType
    {
        Attack,
        Defence,
        CardDraw,
        Increase,
        Enemy,
        Endow
    }

    public enum UseStatType
    {
        None,
        Strength,
        Intellect
    }


    public int no;
    public string name;
    public string variableName;
    public CardType type; //지울거
    public SkillType skillType;
    public AttackType attackType;
    public UseStatType useStatType;
    public string description;
    public int defaultXvalue;
    public string effect;
    public int effectUseTurn;
    public int useCost;
    public int token;

    public CardData(int _no, string _name, string _variableName, SkillType _skillType, AttackType _attackType, UseStatType _useStatType, string _description, int _defaultXvalue, string _effect, int _effectUseTurn, int _useCost, int _token)
    {
        no = _no;
        name = _name;
        variableName = _variableName;
        skillType = _skillType;
        attackType = _attackType;
        useStatType = _useStatType;
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
        undead,
        human,
        monster,
        dragon
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
    public string appearanceArea1;
    public string appearanceArea2;
    public string appearanceArea3;

    public MonsterData(int _no, string _name, int _level, Type _type, float _hp, int _strength, int _intelligence, int _luck, int _speed, 
        int _action1, int _action2, int _action3, int _giveExp, int _dropGold, 
        int _dropitem1, int _dropitem1Percentage, int _dropitem2, int _dropitem2Percentage, int _dropitem3, int _dropitem3Percentage,
        string _appearanceArea1, string _appearanceArea2, string _appearanceArea3)
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

        if(_appearanceArea1 == "null")
        {
            appearanceArea1 = null;
        }
        else
        {
            appearanceArea1 = _appearanceArea1;
        }

        if (_appearanceArea2 == "null")
        {
            appearanceArea2 = null;
        }
        else
        {
            appearanceArea2 = _appearanceArea2;
        }

        if (_appearanceArea3 == "null")
        {
            appearanceArea3 = null;
        }
        else
        {
            appearanceArea3 = _appearanceArea3;
        }
    }
}
#endregion

#region 무기 데이터
public enum Grade
{
    none,
    uncommon,
    common,
    rare,
    unique,
    legend
}
[Serializable]
public class WeaponData
{
    public enum Type
    {
        None,
        OnehandSword,
        TwohandSword,
        Axe,
        Mace,
        Shield,
        Wand,
        Staff,
        Culb,
        Hammer
    }
    public enum EquipType
    {
        None,
        OneHand,
        TwoHand
    }

    public int no;
    public string name;
    public Type type;
    public Grade grade;
    public int level;
    public EquipType equipType;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public int getCard1;
    public int getCard1Count;
    public int getCard2;
    public int getCard2Count;
    public int getCard3;
    public int getCard3Count;
    public int getCard4;
    public int getCard4Count;
    public int getCard5;
    public int getCard5Count;
    public int getCard6;
    public int getCard6Count;
    public int getCard7;
    public int getCard7Count;
    public int getCard8;
    public int getCard8Count;

    public int buyGold;
    public int sellGold;

    public WeaponData(int _no, string _name, Type _type, Grade _grade, int _level, EquipType _equipType, int _strength, int _intelligence, int _luck, int _speed,
        int _getCard1, int _getCard1Count, int _getCard2, int _getCard2Count, int _getCard3, int _getCard3Count, int _getCard4, int _getCard4Count, int _getCard5, int _getCard5Count, int _getCard6, int _getCard6Count, int _getCard7, int _getCard7Count, int _getCard8, int _getCard8Count, 
        int _buyGold, int _sellGold)
    {
        no = _no;
        name = _name;
        type = _type;
        grade = _grade;
        level = _level;
        equipType = _equipType;
        strength = _strength;
        intelligence = _intelligence;
        luck = _luck;
        speed = _speed;

        getCard1 = _getCard1;
        getCard1Count = _getCard1Count;

        getCard2 = _getCard2;
        getCard2Count = _getCard2Count;

        getCard3 = _getCard3;
        getCard3Count = _getCard3Count;

        getCard4 = _getCard4;
        getCard4Count = _getCard4Count;

        getCard5 = _getCard5;
        getCard5Count = _getCard5Count;

        getCard6 = _getCard6;
        getCard6Count = _getCard6Count;

        getCard7 = _getCard7;
        getCard7Count = _getCard7Count;

        getCard8 = _getCard8;
        getCard8Count = _getCard8Count;

        buyGold = _buyGold;
        sellGold = _sellGold;
    }
}
#endregion

#region 방어구 데이터
[Serializable]
public class ArmorData
{
    public enum Type
    {
        Head,
        Armor,
        Jewel
    }

    public int no;
    public string name;
    public Type type;
    public Grade grade;
    public int level;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public int getCard1;
    public int getCard1Count;
    public int getCard2;
    public int getCard2Count;
    public int getCard3;
    public int getCard3Count;
    public int getCard4;
    public int getCard4Count;

    public int buyGold;
    public int sellGold;

    public ArmorData(int _no, string _name, Type _type, Grade _grade, int _level, int _strength, int _intelligence, int _luck, int _speed,
        int _getCard1, int _getCard1Count, int _getCard2, int _getCard2Count, int _getCard3, int _getCard3Count, int _getCard4, int _getCard4Count,
        int _buyGold, int _sellGold)
    {
        no = _no;
        name = _name;
        type = _type;
        grade = _grade;
        level = _level;
        strength = _strength;
        intelligence = _intelligence;
        luck = _luck;
        speed = _speed;

        getCard1 = _getCard1;
        getCard1Count = _getCard1Count;

        getCard2 = _getCard2;
        getCard2Count = _getCard2Count;

        getCard3 = _getCard3;
        getCard3Count = _getCard3Count;

        getCard4 = _getCard4;
        getCard4Count = _getCard4Count;

        buyGold = _buyGold;
        sellGold = _sellGold;
    }
}
#endregion

#region 아이템 데이터
[Serializable]
public class ItemData
{
    public int no;
    public string name;
    public string effect;

    public int buyGold;
    public int sellGold;
    public int useCost;

    public ItemData(int _no, string _name, string _effect, int _buyGold, int _sellGold, int _useCost)
    {
        no = _no;
        name = _name;
        effect = _effect;

        buyGold = _buyGold;
        sellGold = _sellGold;
        useCost = _useCost;
    }
}
#endregion