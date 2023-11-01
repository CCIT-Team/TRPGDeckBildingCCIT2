using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CardDataBase : MonoBehaviour   //이후 DataBase와 병합하는게 나을것같음
{
    public static CardDataBase instance = null;
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
        LoadcardDB(dataPath);
    }
    public List<CardData> cards = new List<CardData>();
    private const string dataPath = "/CardsData.csv";


    private void Start()
    {
        
    }

    private void LoadcardDB(string path)
    {
        string[] cardDB = File.ReadAllLines(Application.streamingAssetsPath + path);

        for (int i = 1; i < cardDB.Length; i++)
        {
            cards.Add(new CardData(cardDB[i].Split(',')));
        }
    }
}

[Serializable]
public class CardData
{
    public int cardID;  //카드 번호
    public string cardName; //카드 이름
    public int cost;    //카드 코스트
    public CARDRARITY rarity;   //임시
    public string cardImage;    //이미지, 임시로 이름 지정
    public string cardText; //설명
    public int tokenAmount; //토큰 수
    public CARDEFFECT effect1;  //효과
    public float power1;    //효과 위력
    public CARDEFFECT effect2;
    public float power2;
    public CARDEFFECT effect3;
    public float power3;
    public CARDEFFECT effect4;
    public float power4;
    public CARDEFFECT effect5;
    public float power5;

    public CardData(string[] data)  //일부 칸이 공백일 가능성도 존재
    {
        int count = 0;
        cardID = int.Parse(data[count++]);
        cardName = data[count] != "" ? data[count++] : "Null";
        cost = data[count] != "" ? int.Parse(data[count++]) : 0;
        tokenAmount = data[count] != "" ? int.Parse(data[count++]) : 0;
        cardImage = data[count] != "" ? data[count++] : "Null";
        cardText = data[count] != "" ? data[count++] : "Null";
        rarity = (CARDRARITY)(data[count] != "" ? Enum.Parse(typeof(CARDRARITY), data[count++]) : -1);
        effect1 = (CARDEFFECT)(data[count] != "" ? Enum.Parse(typeof(CARDEFFECT), data[count++]) : -1);
        power1 = data[count] != "" ? float.Parse(data[count++]) : 0;
        effect2 = (CARDEFFECT)(data[count] != "" ? Enum.Parse(typeof(CARDEFFECT), data[count++]) : -1);
        power2 = data[count] != "" ? float.Parse(data[count++]) : 0;
        effect3 = (CARDEFFECT)(data[count] != "" ? Enum.Parse(typeof(CARDEFFECT), data[count++]) : -1);
        power3 = data[count] != "" ? float.Parse(data[count++]) : 0;
        effect4 = (CARDEFFECT)(data[count] != "" ? Enum.Parse(typeof(CARDEFFECT), data[count++]) : -1);
        power4 = data[count] != "" ? float.Parse(data[count++]) : 0;
        effect5 = (CARDEFFECT)(data[count] != "" ? Enum.Parse(typeof(CARDEFFECT), data[count++]) : -1);
        power5 = data[count] != "" ? float.Parse(data[count++]) : 0;
    }
}

public enum CARDEFFECT
{
    None = -1,
    SingleAttack,
    Buff,
    Draw
}

public enum CARDRARITY
{
    None = -1,
    Normal,
    Rare,
    Unique
}