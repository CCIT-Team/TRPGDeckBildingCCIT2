using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_BattleManager : MonoBehaviour //전투, 턴 관리
{
    public static N_BattleManager instance;

    public RewardUI rewardUI;

    public List<Unit> units;
    public Unit currentUnit;

    public Transform playerPosition;
    public Transform monsterPosition;

    public int startHandCount = 3;
    public int maxHandCount = 5;

    [Tooltip("직업/무기 별 카드 시작 인덱스\n 0 = 워리어\n 1 = 메지션\n 2 = 클레릭\n 3 = 한손검\n 4 = 양손검\n 5 = 방패\n 6 = 스태프/완드\n 7 = 클럽\n 8 = 메이스\n 9 = 헤머\n 10 = 도끼")]
    public int[] CardStartIndexOfType = { -1,-1,-1, -1, -1, -1, -1, -1, -1, -1, -1};

    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    public MonsterUIManager monsterUI;

    bool isAction = false;
    bool isTurnAnnounce = false;
    public bool isBuffRun_All = false;
    public bool isHandOver = false;
    [HideInInspector]
    public bool isDragonLanding = false;

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
            {
                StartCoroutine(WaitingWhileAction());
                StartCoroutine(DoActionOff());
            }
                
        }
    }

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
        audioSource = GetComponent<AudioSource>();
        StartBattle();
    }

    void Update()
    {
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
        BattleUI.instance.SetTurnSlider(units); //턴테이블, 타임라인 생성
        SoltSpeed(units); //속도 정렬
        StartCoroutine(PlayTurn());
    }

    public void EndBattle()
    {
        AccelerateTime();
        BattleUI.instance.Accelerator.SetActive(false);
        isBuffRun_All = false;
        StopCoroutine(PlayTurn());
        if (currentUnit.CompareTag("Player"))
            currentUnit.GetComponent<Character>().isMyturn = false;
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
        if (currentUnit != null)
        {
            if(currentUnit.CompareTag("Player"))
                playerAlive = true;
            else
                monsterAlive = true;
        }
        if (playerAlive ^ monsterAlive)
            if (playerAlive)
            {
                GameManager.instance.isVictory = true;
                audioSource.PlayOneShot(audioClips[0]);
                BattleUI.instance.gameObject.SetActive(false);
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
            if (currentUnit.TryGetComponent(out Character character))
            {
                currentUnit = null;
                character.isMyturn = false;
            }
            else
            {
                Monster monster = currentUnit.GetComponent<Monster>();
                currentUnit = null;
                monster.IsMyturn = false;
            }
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
                    goto case 60;
                case 60:    //매지션
                    CardStartIndexOfType[2]++;
                    goto case 70;
                case 70:    //클레릭
                    CardStartIndexOfType[3]++;
                    goto case 51;
                case 51:
                    CardStartIndexOfType[4]++;
                    goto case 52;
                case 52:
                    CardStartIndexOfType[5]++;
                    goto case 53;
                case 53:
                    CardStartIndexOfType[6]++;
                    goto case 54;
                case 54:
                    CardStartIndexOfType[7]++;
                    goto case 55;
                case 55:
                    CardStartIndexOfType[8]++;
                    goto case 56;
                case 56:
                    CardStartIndexOfType[9]++;
                    goto case 57;
                case 57:
                    CardStartIndexOfType[10]++;
                    goto case 58;
                case 58:
                    CardStartIndexOfType[11]++;
                    goto case 59;
                case 59:
                    break;
            }
        }
    }
    
    IEnumerator WaitingWhileAction()
    {
        BattleUI.instance.inputBlocker.SetActive(true);
        yield return new WaitWhile(() => isAction);
        yield return new WaitForSeconds(1.5f);
        if (!isAction)
            BattleUI.instance.inputBlocker.SetActive(false);
        else
            StartCoroutine(WaitingWhileAction());
    }

    IEnumerator DoActionOff()
    {
        yield return new WaitForSeconds(4);
        isAction = false;
    }

    public void AccelerateTime(float mul = 1)
    {
        Time.timeScale = mul;
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
            playerarray[i].transform.localScale *= 1.8f;
            if (playerarray[i].GetComponent<Character>().isMyturn)
            {
                playerarray[i].GetComponent<Character>().isMyturn = false;
            }
            playerarray[i].name = playerarray[i].GetComponent<Character_type>().nickname;
            playerarray[i].transform.SetParent(playerPosition);
            playerarray[i].transform.localPosition = new Vector3(2.3f*(i - playerarray.Length/2 + (playerarray.Length+1) % 2 / 2f), 0, 0);
            playerarray[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            units.Add(playerarray[i].GetComponent<Unit>());
            if (!playerarray[i].TryGetComponent<UnitAnimationControl>(out UnitAnimationControl animControl))
            {
                playerarray[i].AddComponent<UnitAnimationControl>().SetAnimator();
            }
            else
            {
                animControl.SetAnimator();
            }
        }

        GameManager.instance.MonsterInstance(GameManager.instance.GetBattleMonsterSetting().ToArray(), monsterPosition.position);
        GameObject[] monsterArray = GameObject.FindGameObjectsWithTag("Monster");
        for (int i =0; i < monsterArray.Length; i++)
        {
            monsterArray[i].transform.SetParent(monsterPosition);
            monsterArray[i].GetComponent<Monster>().SetMonster();
            monsterArray[i].transform.localPosition = new Vector3(3 * (i - monsterArray.Length / 2 + (monsterArray.Length + 1) % 2 / 2f), 0, 0);
            monsterArray[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            units.Add(monsterArray[i].GetComponent<Unit>());
        }
        monsterUI.SetMonster(monsterArray);
        BattleUI.instance.BindPlayer(playerarray);
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
        if(!isDragonLanding)
        {
            foreach (Unit unit in units)
            {
                if (unit.gameObject.name.Contains("드래곤"))
                {
                    yield return new WaitForSeconds(3f);
                }
            }
            isDragonLanding = true;
        }    
        currentUnit = units[0];
        units.Remove(currentUnit);
        if (currentUnit.gameObject.CompareTag("Player"))
        {
            BattleUI.instance.playerBar.gameObject.SetActive(true);
            BattleUI.instance.playerBar.LinkingPlayer(currentUnit.gameObject);
        }    
        else
            BattleUI.instance.playerBar.gameObject.SetActive(false);
        Camera.main.GetComponent<BattleCameraMove>().cameratarget = currentUnit.gameObject;
        isTurnAnnounce = true;
        GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
        StartCoroutine(DisplayCurrentTurn());
        yield return new WaitUntil(() => !isTurnAnnounce);
        if (currentUnit.TryGetComponent(out Character character))
        {
            character.isMyturn = true;
            character.cost = character.maxCost;
            foreach (PlayerBattleUI ui in BattleUI.instance.playerUI)
            {
                if(ui.gameObject.activeSelf)
                    ui.StartCoroutine(ui.ActIfTurn());
            }
            yield return new WaitUntil(() => !character.isMyturn);
        }   
        else
        {
            Monster monster = currentUnit.GetComponent<Monster>();
            monster.IsMyturn = true;
            yield return new WaitUntil(() => !monster.IsMyturn && !IsAction);
        }
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (!IsAction)
            {
                yield return new WaitForSeconds(1.4f);
                if (!IsAction)
                    break;
            }
        }
        if(currentUnit != null)
            units.Add(currentUnit);
        currentUnit = null;
        StartCoroutine(PlayTurn());
    }

    IEnumerator DisplayCurrentTurn()
    {
        /*if(currentUnit.CompareTag("Player"))
        {
            BattleUI.instance.announceImage.texture = (Texture)Resources.Load("");
        }
        else
        {
            BattleUI.instance.announceImage.texture = (Texture)Resources.Load("");
        }*/
        BattleUI.instance.announceText.text = currentUnit.name + "의 턴!";
        BattleUI.instance.TurnAnnounce.SetActive(true);
        yield return new WaitForSeconds(3);
        BattleUI.instance.TurnAnnounce.SetActive(false);
        isTurnAnnounce = false;
    }
    #endregion
}
