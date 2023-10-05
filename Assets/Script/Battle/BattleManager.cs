using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour  //�ѹ� �ڵ����� �ؾ���. ����
{
    public static BattleManager instance;
    public List<Character> characters;
    Character currentCharacter;

    public List<Character> PlayerDummy; //�Ҵ� ���� �� �ӽ�����

    public DrawSystem drawSystem;

    Card selectedCard;

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
            EndTurnBtn.onClick.AddListener(() => EndTurn());
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            EndTurn();

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            print("���� ĳ���� : " + currentCharacter.name);
    }

    void StartBattle()
    {
        print("Battle Start!");
        foreach(Character character in PlayerDummy)
        {
            characters.Add(character);
            drawSystem.EnterBattle(character.GetComponent<Deck>());
        }
        SortSpeed();
        currentCharacter = characters[0];
    }

    void EndBattle()
    {
        foreach (Character character in characters)
        {
            drawSystem.EndBattle(character.GetComponent<Deck>());
        }
        characters.Clear();
        currentCharacter = null;
        print("Battle is Finish!");
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
        characters.Sort((a, b) => a.speed < b.speed ? 1 : -1);
        //�Ʒ��� ������
        int debugInt = 1;
        foreach (Character character in characters)
        {
            print(debugInt++ +"��° : "+character.gameObject.name+"(�ӵ�: "+character.speed+")");
        }
    }

    public void StartTurn()
    {
        currentCharacter = characters[0];
        drawSystem.DrawCard();
    }

    public void EndTurn()    //�� ����
    {
        if (currentCharacter == null)
            currentCharacter = characters[0];

        characters.Remove(currentCharacter);
        characters.Add(currentCharacter);
        currentCharacter = characters[0];
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
    public void ChangeTurn(int listNum, int turn = -1)      //������ ĳ������ ���� turn���� ����,  turn �������� ���� �ڷ� ����, listNum �������� ���� ���� ĳ���� ����
    {
        if (turn == -1)
        {
            characters.Remove(characters[listNum]);
            characters.Add(characters[listNum]);
        }
        else
        {
            characters.Remove(characters[listNum]);
            characters.Insert(turn, characters[listNum]);
        }
        currentCharacter = characters[0];
    }

    public void SettingForCurrentPlayer()
    {

    }

    public void CardSelect(Card card)
    {
        selectedCard = card;
        Debug.Log(selectedCard.gameObject.name+"���õ�");
        StartCoroutine(TargetSelect());
    }

    


    public void DoAction()
    {
        selectedCard.cardEffect();
        
        selectedCard = null;
    }

    IEnumerator TargetSelect()
    {
        bool isrunning = true;
        while(isrunning)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log("���� ����");
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.name + "���� ����");
                    selectedCard.cardTarget = hit.transform.gameObject;
                    isrunning = false;
                }
                else
                {
                    Debug.Log("Ÿ�پ���");
                }
            }
            yield return null;
        }
    }

}

