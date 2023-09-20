using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<Character> characters;
    Character currentCharacter;

    bool isBattle = false;
    public bool Battle
    {
        get { return isBattle; }
        set
        {
            if (isBattle = value)
                StartBattle();
            else
                EndBattle();
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(this);
    }

    public Button EndTurnBtn;


    void Start()
    {
        if (EndTurnBtn != null)
            EndTurnBtn.onClick.AddListener(() => ChangeTurn());
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Battle = !isBattle;
            print("Battle : " + Battle+"("+isBattle+")");
        }
            

        if (Input.GetKeyDown(KeyCode.C))
            ChangeTurn();

        if (Input.GetKeyDown(KeyCode.S))
            SortSpeed();

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            print("���� ĳ���� : " + currentCharacter.name);
    }

    void StartBattle()
    {
        //characters.Add();
        SortSpeed();
        currentCharacter = characters[0];
    }

    void EndBattle()
    {
        characters.Clear();
        currentCharacter = null;
    }

    public void JoinBattle(Character character)     //��ȯ, ��Ȱ �� ĳ���� ���Կ�
    {
        for (int i = 0; i <= characters.Count; i++)
        {
            if (characters[i].speed < character.speed)
            {
                characters.Insert(i, character);
                return;
            }
        }
        characters.Add(character);
    }

    public void ExitBattle(Character character)     //��� ������ ���� ����
    {
        characters.Remove(character);
    }


    void SortSpeed()    //�ӵ��� ���� ĳ���� ����
    {
        characters.Sort((a, b) => a.speed > b.speed ? 1 : -1);
        foreach (Character character in characters)
            print(character.gameObject.name);
    }

    public void ChangeTurn(Character character, int turn = -1)      //������ ĳ������ ���� turn���� ����, turn �������� ���� �ڷ� ����
    {
        characters.Remove(character);
        if (turn == -1)
            characters.Insert(characters.Count, character);
        else
            characters.Insert(turn, character);
        currentCharacter = characters[0];
    }
    public void ChangeTurn(int listNum = 0, int turn = -1)      //������ ĳ������ ���� turn���� ����,  turn �������� ���� �ڷ� ����, listNum �������� ���� ���� ĳ���� ����
    {
        if (currentCharacter == null)
            currentCharacter = characters[listNum];

        if (turn == -1)
        {
            characters.Remove(currentCharacter);
            characters.Insert(characters.Count, currentCharacter);
        }
        else
        {
            characters.Remove(characters[listNum]);
            characters.Insert(turn, characters[listNum]);
        }
        currentCharacter = characters[0];
    }


    void DoAction()
    {
        float daf = 4;
        string dk = "d";
        Dictionary<float, string> jkj = new();
        jkj.TryGetValue(daf,out dk);
        jkj[daf] = "jh";
        Character c;
        this.TryGetComponent<Character>(out c);
    }



}

