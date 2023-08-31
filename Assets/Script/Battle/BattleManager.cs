using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<CharacterDummy> characters;
    CharacterDummy freeSlot;

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
    

    void Start()
    {
        EndTurnBtn.onClick.AddListener(()=>ChangeTurn());
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SortSpeed();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            GiveTurn();

        if (Input.GetKeyDown(KeyCode.Q))
            for (int i = 0; i < characters.Count; i++)
                Debug.Log(i + "��° ĳ���� : " + characters[i]);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangeTurn();
    }

    void StartBattle()
    {

    }

    void SortSpeed()
    {
        characters.Sort((a, b) => a.SPD > b.SPD ? 1 : -1);
        foreach (CharacterDummy character in characters)
            print(character.gameObject.name);
    }

    void Battle()
    {

    }

    void GiveTurn()
    {
        foreach (CharacterDummy character in characters)
        {
            if (character.State != State.Dead)
                character.State = State.Wait;
        }
        characters[0].State = State.MyTurn;

        //ChangeTurn(characters[0]);
    }

    public void ChangeTurn(CharacterDummy character, int inputNum = -1)
    {
        character.State = State.Wait;
        characters.Remove(character);
        if (inputNum == -1)
            characters.Insert(characters.Count, character);
        else
            characters.Insert(inputNum, character);
    }
    public void ChangeTurn(int listNum = 0, int inputNum = -1)
    {
        characters[listNum].State = State.Wait;
        characters.Remove(characters[listNum]);
        if (inputNum == -1)
            characters.Insert(characters.Count, characters[listNum]);
        else
            characters.Insert(inputNum, characters[listNum]);
    }
}
