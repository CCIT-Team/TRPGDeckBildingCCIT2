using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CardDataBase : MonoBehaviour   //���� DataBase�� �����ϴ°� �����Ͱ���
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
    public int cardID;  //ī�� ��ȣ
    public string cardName; //ī�� �̸�
    public int cost;    //ī�� �ڽ�Ʈ
    public CARDRARITY rarity;   //�ӽ�
    public string cardImage;    //�̹���, �ӽ÷� �̸� ����
    public string cardText; //����
    public int tokenAmount; //��ū ��
    public CARDEFFECT effect1;  //ȿ��
    public float power1;    //ȿ�� ����
    public CARDEFFECT effect2;
    public float power2;
    public CARDEFFECT effect3;
    public float power3;
    public CARDEFFECT effect4;
    public float power4;
    public CARDEFFECT effect5;
    public float power5;

    public CardData(string[] data)  //�Ϻ� ĭ�� ������ ���ɼ��� ����
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