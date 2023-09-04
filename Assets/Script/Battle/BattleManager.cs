using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    //public List<CharacterDummy> characters;  //주석 달린부분 CharacterDummy class 지워서 생기는 부분 수정 필요
    //CharacterDummy currentCharacter;

    bool isBattle = false;
    //public bool Battle
    //{
    //    get { return isBattle; }
    //    set
    //    {
    //        if (value = isBattle)
    //            StartBattle();
    //        else
    //            EndBattle();
    //    }
    //}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(this);
    }

    public Button EndTurnBtn;
    

    //void Start()
    //{
    //    if(EndTurnBtn != null)
    //        EndTurnBtn.onClick.AddListener(()=>ChangeTurn());
    //}


    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        ChangeTurn();

    //    if (Input.GetKeyDown(KeyCode.Return))
    //        SortSpeed();

    //    if (Input.GetKeyDown(KeyCode.KeypadEnter))
    //        print("현재 캐릭터 : " + currentCharacter.name);
        
    //}

    //void StartBattle()
    //{
    //    //characters.Add();
    //    SortSpeed();
    //    currentCharacter = characters[0];
    //}

    void EndBattle()
    {
        
    }

    //void JoinBattle(CharacterDummy joinCharacter)
    //{
    //    for(int i = 0; i<= characters.Count;i++)
    //    {
    //        if(characters[i].SPD < joinCharacter.SPD)
    //        {
    //            characters.Add(joinCharacter);
    //            return;
    //        }
    //    } 
    //}


    //void SortSpeed()
    //{
    //    characters.Sort((a, b) => a.SPD > b.SPD ? 1 : -1);
    //    foreach (CharacterDummy character in characters)
    //        print(character.gameObject.name);
    //}

    //void GiveTurn()
    //{
    //    foreach (CharacterDummy character in characters)
    //    {
    //        if (character.State != State.Dead)
    //            character.State = State.Wait;
    //    }
    //    characters[0].State = State.MyTurn;
    //}

    //public void ChangeTurn(CharacterDummy character, int inputNum = -1)
    //{
    //    //character.State = State.Wait;
    //    characters.Remove(character);
    //    if (inputNum == -1)
    //        characters.Insert(characters.Count, character);
    //    else
    //        characters.Insert(inputNum, character);
    //    currentCharacter = characters[0];
    //}
    //public void ChangeTurn(int listNum = 0, int inputNum = -1)
    //{
    //    //characters[listNum].State = State.Wait;
    //    if (currentCharacter == null)
    //        currentCharacter = characters[listNum];
        
    //    if (inputNum == -1)
    //    {
    //        characters.Remove(currentCharacter);
    //        characters.Insert(characters.Count, currentCharacter);
    //    }   
    //    else
    //    {
    //        characters.Remove(characters[listNum]);
    //        characters.Insert(inputNum, characters[listNum]);
    //    }    
    //    currentCharacter = characters[0];
    //}
}
