using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour  //한번 코드정리 해야함. 난잡
{
    public static BattleManager instance;
    public List<Character> characters;
    Character currentCharacter;

    public List<Character> PlayerDummy; //할당 구현 전 임시적용
    public List<Character> EnemyDummy;

    public GameObject playerUI;

    Card selectedCard;

    bool rayOn= true;

    bool playerAlive = false;
    bool monsterAlive = false;

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

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn(); 
    }

    void StartBattle()
    {
        print("Battle Start!");
        foreach(Character character in PlayerDummy)
        {
            characters.Add(character);
            character.GetComponent<DrawSystem>().EnterBattle(character.GetComponent<Deck>());
        }
        foreach (Character character in EnemyDummy)
        {
            characters.Add(character);
        }
        playerAlive = true;
        monsterAlive = true;
        SortSpeed();
        StartTurn();
    }

    void EndBattle()
    {
        foreach (Character character in characters)
        {
            if(PlayerDummy.Contains(character))
                character.GetComponent<DrawSystem>().EndBattle(character.GetComponent<Deck>());
        }
        characters.Clear();
        currentCharacter = null;
        print("Battle is Finish!");
    }

    public void JoinBattle(Character character)     //소환, 부활 등 캐릭터 난입용
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

    public void ExitBattle(Character character)     //사망 등으로 인한 제거
    {
        characters.Remove(character);

        if (EnemyDummy.Contains(character))
            foreach (Character c in PlayerDummy)
            {
                if (characters.Contains(c))
                    monsterAlive = true;
                else
                    monsterAlive = false;
            }
        else
            foreach (Character c in EnemyDummy)
            {
                if (characters.Contains(c))
                    playerAlive = true;
                else
                    playerAlive = false;
            }



        Debug.Log("플레이어"+ playerAlive+", 몬스터" +monsterAlive +", 비교"+ (playerAlive ^ monsterAlive));
        if (playerAlive ^ monsterAlive)
            EndBattle();
    }


    void SortSpeed()    //속도에 따른 캐릭터 정렬
    {
        characters.Sort((a, b) => a.speed < b.speed ? 1 : -1);
        //아래는 디버깅용
        int debugInt = 1;
        foreach (Character character in characters)
        {
            print(debugInt++ +"번째 : "+character.gameObject.name+"(속도: "+character.speed+")");
        }
    }

    public void StartTurn()
    {
        currentCharacter = characters[0];
        currentCharacter.isMyturn = true;
        if(PlayerDummy.Contains(currentCharacter))
        {
            playerUI.SetActive(true);
            currentCharacter.GetComponent<DrawSystem>().DrawCard();
        }
        else  playerUI.SetActive(false);
    }

    public void EndTurn()    //턴 종료
    {
        if (currentCharacter == null)
            currentCharacter = characters[0];

        currentCharacter.isMyturn = false;
        characters.Remove(currentCharacter);
        characters.Add(currentCharacter);
        StartTurn();
    }
    public void ChangeTurn(Character character, int turn = -1)      //지정된 캐릭터의 턴을 turn으로 변경, turn 미지정시 가장 뒤로 변경
    {
        characters.Remove(character);
        if (turn == -1)
            characters.Insert(characters.Count, character);
        else
            characters.Insert(turn, character);
        currentCharacter = characters[0];
    }
    public void ChangeTurn(int listNum, int turn = -1)      //지정된 캐릭터의 턴을 turn으로 변경,  turn 미지정시 가장 뒤로 변경, listNum 미지정시 현재 턴의 캐릭터 지정
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
        rayOn = true;
        Debug.Log(selectedCard.gameObject.name+"선택됨");
        StartCoroutine(TargetSelect());
    }

    


    public void DoAction()
    {
        rayOn = false;
        selectedCard.cardEffect();
        
        selectedCard = null;
    }

    IEnumerator TargetSelect()
    {
        while(rayOn)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log("레이 나감");
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.name + "레이 맞음");
                    selectedCard.cardTarget = hit.transform.gameObject;
                }
                else
                {
                    Debug.Log("타겟없음");
                }
            }
            yield return null;
        }
    }

}

