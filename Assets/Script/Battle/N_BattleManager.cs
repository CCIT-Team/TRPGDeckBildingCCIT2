using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class N_BattleManager : MonoBehaviour //전투, 턴 관리
{
    //exitbattle(유닛 퇴장) 코드 수정 필요
    public static N_BattleManager instance;

    [SerializeField]
    List<GameObject> characterUIs;
    public BattleUI battleUI;

    public List<Unit> units;
    public Unit currentUnit;

    public int startHandCount = 5;

    private void Awake()
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
        StartBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currentUnit.GetComponent<MonsterDummy>().isMyturn = false;
        if (Input.GetKeyDown(KeyCode.Return))
            ExitBattle(currentUnit);
    }

    void StartBattle()
    {
        AddTurnPool();
        battleUI.SetTurnSlider(units);
        SoltSpeed(units);
        foreach(GameObject characterUI in characterUIs)
        {
            characterUI.GetComponent<N_DrawSystem>().DrawCard(startHandCount);

        } 
        StartCoroutine(PlayTurn());
    }

    void EndBattle()
    {
        StopCoroutine(PlayTurn());
        SceneManager.LoadScene("Map1");
    }

    void CheckBattleState()
    {
        bool playerAlive = false, monsterAlive = false;
        foreach(Unit unit in units)
        {
            if (unit.CompareTag("Player"))
                playerAlive = true;
            else
                monsterAlive = true;
        }
        if (currentUnit.CompareTag("Player"))
            playerAlive = true;
        else
            monsterAlive = true;

        if (playerAlive ^ monsterAlive)
            EndBattle();
    } 

    void JoinBattle(Unit joinUnit)
    {
        int joinUnitSpeed;
        if (joinUnit.TryGetComponent<Character>(out Character joinCharacter))
            joinUnitSpeed = joinCharacter.speed;
        else
            joinUnitSpeed = joinUnit.GetComponent<MonsterDummy>().speed;

        int unitSpeed;
        for (int i = 0; i< units.Count;i++)
        {
            if (units[i].TryGetComponent<Character>(out Character character))
                unitSpeed = character.speed;
            else
                unitSpeed = units[i].GetComponent<MonsterDummy>().speed;

            if(joinUnitSpeed < unitSpeed)
            {
                units.Insert(i, joinUnit);
                return;
            }
        }
        units.Add(joinUnit);
    }

    public void ExitBattle(Unit exitUnit)
    {
        if (exitUnit == currentUnit)
        {
            StopCoroutine(PlayTurn());
            StartCoroutine(PlayTurn());
        }
        exitUnit.gameObject.SetActive(false);
        units.Remove(exitUnit);
        CheckBattleState();
    }


    //--------------------------------------

    void AddTurnPool()//유닛 추가용 코드, 미완
    {
        int uiNum = 0;
        foreach(Unit unit in units)
        {
            if (unit.TryGetComponent(out Character character))
            {
                Debug.Log(uiNum);
                characterUIs[uiNum++].GetComponent<N_DrawSystem>().BindCharacter(character);
            }
                
        }
    }

    public void SoltSpeed(List<Unit> units)
    {
        int speedA, speedB;
        units.Sort((a, b) =>
        {
            if (a.TryGetComponent(out Character characterA))
                speedA = characterA.speed;
            else
                speedA = a.GetComponent<MonsterDummy>().speed;

            if (b.TryGetComponent(out Character characterB))
                speedB = characterB.speed;
            else
                speedB = b.GetComponent<MonsterDummy>().speed;

            if (speedA >= speedB)
                return -1;
            else
                return 1;
        });
    }
    
    IEnumerator PlayTurn()
    {
        currentUnit = units[0];
        units.Remove(currentUnit);
        //currentUnit.GetComponent<Deck>().gameObject.SetActive(true);
        if (currentUnit.TryGetComponent(out Character character))
        {
            character.isMyturn = true;
            foreach (GameObject ui in characterUIs)
                ui.GetComponent<N_DrawSystem>().CheckTurn();
            yield return new WaitUntil(() => !character.isMyturn);
        }
            
        else
        {
            MonsterDummy monster = currentUnit.GetComponent<MonsterDummy>();
            monster.isMyturn = true;
            yield return new WaitUntil(() => !monster.isMyturn);
        }
        //currentUnit.GetComponent<Deck>().gameObject.SetActive(false);
        units.Add(currentUnit);
        currentUnit = null;
        StartCoroutine(PlayTurn());
    }
}
