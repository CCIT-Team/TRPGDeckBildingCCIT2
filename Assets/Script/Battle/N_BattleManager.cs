using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_BattleManager : MonoBehaviour //전투, 턴 관리
{
    public static N_BattleManager instance;

    public BattleUI battleUI;
    public RewardUI rewardUI;

    public List<Unit> units;
    public Unit currentUnit;

    public Transform playerPosition;
    public Transform monsterPosition;

    public int startHandCount = 5;

    [Tooltip("직업/무기 별 카드 종류의 수\n 0 = 워리어\n 1 = 메지션\n 2 = 클레릭\n 3 = 한손검\n 4 = 양손검\n 5 = 방패\n 6 = 스태프\n 7 = 완드\n 8 = 클럽\n 9 = 메이스\n 10 = 헤머\n 11 = 도끼")]
    public int[] CardStartIndexOfType = { -1,-1,-1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    bool isAction = false;
    public bool IsAction
    {
        get { return isAction;}
        set
        {
            if (isAction == value)
                return;
            else
                isAction = value;

            if(isAction)
                StartCoroutine(WaitingWhileAction());
        }
    }

    Character inMapTurnCharacter;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currentUnit.GetComponent<Monster>().IsMyturn = false;
        if (Input.GetKeyDown(KeyCode.Return))
            ExitBattle(currentUnit);
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            EndBattle();
        if (Input.GetKeyDown(KeyCode.Q))
            isAction = !isAction;
    }

    void StartBattle()
    {
        CountCardPreBattle();
        AddTurnPool(); //전투할 유닛 추가
        battleUI.SetTurnSlider(units); //턴테이블, 타임라인 생성
        SoltSpeed(units); //속도 정렬
        StartCoroutine(PlayTurn());
    }

    public void EndBattle()
    {
        StopCoroutine(PlayTurn());
        if (currentUnit.CompareTag("Player"))
            currentUnit.GetComponent<Character>().isMyturn = false;
        inMapTurnCharacter.isMyturn = true;
        GameManager.instance.LoadScenceName("Map1");
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
            if (playerAlive)
            {
                battleUI.gameObject.SetActive(false);
                rewardUI.gameObject.SetActive(true);
                rewardUI.GiveReward();
            }
               
            else
                EndBattle();

    } 

    void JoinBattle(Unit joinUnit)
    {
        int joinUnitSpeed;
        if (joinUnit.TryGetComponent<Character>(out Character joinCharacter))
            joinUnitSpeed = joinCharacter.speed;
        else
            joinUnitSpeed = joinUnit.GetComponent<Monster>().speed;

        int unitSpeed;
        for (int i = 0; i< units.Count;i++)
        {
            if (units[i].TryGetComponent<Character>(out Character character))
                unitSpeed = character.speed;
            else
                unitSpeed = units[i].GetComponent<Monster>().speed;

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

    void CountCardPreBattle()
    {
        int indexByWeapon;
        foreach (CardData cardData in DataBase.instance.cardData)
        {
            indexByWeapon = int.Parse(cardData.no.ToString().Substring(0, 2));
            switch (indexByWeapon)  //직업,장비 별 시작 인덱스이므로 그 직업의 번호가 아닌 다음 번호에 ++
            {
                case 50:    //워리어
                    CardStartIndexOfType[1]++;
                    goto case 51;
                case 51:
                    CardStartIndexOfType[2]++;
                    goto case 52;
                case 52:
                    CardStartIndexOfType[3]++;
                    goto case 53;
                case 53:
                    CardStartIndexOfType[4]++;
                    goto case 54;
                case 54:
                    CardStartIndexOfType[5]++;
                    goto case 55;
                case 55:
                    CardStartIndexOfType[6]++;
                    goto case 56;
                case 56:
                    CardStartIndexOfType[7]++;
                    goto case 57;
                case 57:
                    CardStartIndexOfType[8]++;
                    goto case 58;
                case 58:
                    CardStartIndexOfType[9]++;
                    goto case 59;
                case 59:
                    CardStartIndexOfType[10]++;
                    goto case 60;
                case 60:    //매지션
                    CardStartIndexOfType[11]++;
                    break;
                    //70 부터 클레릭 시작이므로 case x
            }
        }
    }
    
    IEnumerator WaitingWhileAction()
    {
        battleUI.inputBlocker.SetActive(true);
        while(true)
        {
            yield return new WaitForSeconds(1.5f);
            if(!isAction)
            {
                battleUI.inputBlocker.SetActive(false);
                break;
            }
        }
    }

    //--------------------------------------
    #region 턴
    void AddTurnPool()//시작 시 유닛 추가용 코드
    {
        if(GameManager.instance != null)
            GameManager.instance.GetLoadAvatar(playerPosition.position);

        GameObject[] playerarray = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i< playerarray.Length;i++)
        {
            if (playerarray[i].GetComponent<Character>().isMyturn)
            {
                inMapTurnCharacter = playerarray[i].GetComponent<Character>();
                playerarray[i].GetComponent<Character>().isMyturn = false;
            } 
            playerarray[i].transform.SetParent(playerPosition);
            playerarray[i].transform.localPosition = new Vector3(3*(i - playerarray.Length/2 + (playerarray.Length+1) % 2 / 2f), 0, 0);
            units.Add(playerarray[i].GetComponent<Unit>());
        }
        for (int i = 0; i < monsterPosition.transform.childCount; i++)
        {
            units.Add(monsterPosition.transform.GetChild(i).GetComponent<Unit>());
            monsterPosition.transform.GetChild(i).localPosition = new Vector3(3 * (i - monsterPosition.transform.childCount / 2 + (monsterPosition.transform.childCount + 1) % 2 / 2f), 0, 0);
        }
        battleUI.BindPlayer(playerarray);
    }

    public void SoltSpeed(List<Unit> units)
    {
        int speedA, speedB;
        units.Sort((a, b) =>
        {
            if (a.TryGetComponent(out Character characterA))
                speedA = characterA.speed;
            else
                speedA = a.GetComponent<Monster>().speed;

            if (b.TryGetComponent(out Character characterB))
                speedB = characterB.speed;
            else
                speedB = b.GetComponent<Monster>().speed;

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
        if (currentUnit.TryGetComponent(out Character character))
        {
            character.isMyturn = true;
            foreach (PlayerBattleUI ui in battleUI.playerUI)
            {
                if(ui.gameObject.activeSelf)
                    ui.StartCoroutine(ui.ActIfTurn());
            } 
            yield return new WaitUntil(() => !character.isMyturn && !IsAction);
        }   
        else
        {
            Monster monster = currentUnit.GetComponent<Monster>();
            monster.IsMyturn = true;
            yield return new WaitUntil(() => !monster.IsMyturn && !IsAction);
        }
        units.Add(currentUnit);
        currentUnit = null;
        StartCoroutine(PlayTurn());
    }
    #endregion
}
