using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] public List<GameObject> players = new List<GameObject>();

    [SerializeField]public List<string> avatar_0 = new List<string>();
    [SerializeField]public List<string> avatar_1 = new List<string>();
    [SerializeField]public List<string> avatar_2 = new List<string>();
    public int avatarCounter = 0;
    private string sceneName = null; //scene변경

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
    }

    private void Start()
    {
        
    }
    #region 아바타 데이타 로딩

    public void GetLoadAvatar(Vector3 position)
    {
        players.Clear();
        for (int i = 0; i < DataBase.instance.loadTypeData.Count; i++)
        {
            LoadAvatar(i, position);
        }
    }

    private void LoadAvatar(int index, Vector3 position)
    {
        GameObject unit = Instantiate(Resources.Load("Test_Assets/Prefab/Avatar", typeof(GameObject))) as GameObject;
        unit.transform.position = position;//나중에 맵 포지션 받아올거임
        AvatarTypeSetting(unit, index);
        AvatarStatSetting(unit, index);
        AvatarCardSetting(unit, index);
        players.Add(unit);
    }
    private void AvatarTypeSetting(GameObject unit, int index)
    {
        unit.GetComponent<Character_type>().SetUnitType(DataBase.instance.loadTypeData[index]);
    }
    private void AvatarStatSetting(GameObject unit, int index)
    {
        unit.GetComponent<Character>().SetUnitData(DataBase.instance.loadStatData[index]);
    }
    private void AvatarCardSetting(GameObject unit, int index)
    {
        unit.GetComponent<Character_Card>().SetUnitCard(DataBase.instance.loadCardData[index]);
    }

    #endregion

    #region 맵, 배틀 씬 몬스터 생성

    public void MonsterMapInstance(int index, Vector3 position)
    {
        LoadMonster(index, position);
    }

    public void MonsterInstance(int[] indexs, Vector3 position)
    {
        for (int i = 0; i < indexs.Length; i++)
        {
            LoadMonster(indexs[i], position);
        }
    }

    private void LoadMonster(int index, Vector3 position)
    {
        for (int i = 0; i < DataBase.instance.monsterData.Count; i++)
        {
            if (DataBase.instance.monsterData[i].no == index)
            {
                GameObject unit = Instantiate(Resources.Load("Test_Assets/Prefab/Monster", typeof(GameObject))) as GameObject;
                unit.transform.position = position;//나중에 맵 포지션 받아올거임
                MonsterStatSetting(unit, i);
            }
        }
    }
    private void MonsterStatSetting(GameObject unit, int index)
    {
        unit.GetComponent<MonsterStat>().SetMonsterData(DataBase.instance.monsterData[index]);
    }

    #endregion

    #region 로비씬 아바타 세팅 -> 오브젝트 화

    public void GetLobbyAvatar()
    {
        for (int i = 0; i < avatarCounter; i++)
        {
            CreateDataAvatar(i);
        }
        DataBase.instance.LoadData();
    }

    private void CreateDataAvatar(int index)
    {
        string insertQuery = null;
        switch (index)
        {
            case 0:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({avatar_0[0]}, '{avatar_0[1]}', '{avatar_0[2]}', '{avatar_0[3]}', '{avatar_0[4]}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_0[0], avatar_0[2]));
                DataBase.instance.SaveDB(AvatarCardSetting(avatar_0[0], avatar_0[2]));
                break;
            case 1:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({avatar_1[0]}, '{avatar_1[1]}', '{avatar_1[2]}', '{avatar_1[3]}', '{avatar_1[4]}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_1[0], avatar_1[2]));
                DataBase.instance.SaveDB(AvatarCardSetting(avatar_0[0], avatar_1[2]));
                break;
            case 2:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({avatar_2[0]}, '{avatar_2[1]}', '{avatar_2[2]}', '{avatar_2[3]}', '{avatar_2[4]}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_2[0], avatar_2[2]));
                DataBase.instance.SaveDB(AvatarCardSetting(avatar_0[0], avatar_2[2]));
                break;
        }
    }

    private string AvatarStatSetting(string playerNum, string major)
    {
        string insertQuery;
        for (int i = 0; i < DataBase.instance.defaultData.Count; i++)
        {
            if(major == DataBase.instance.defaultData[i].major.ToString())
            {
               return insertQuery = $"INSERT INTO Stat (playerNum, strength, intelligence, luck, speed, currentHp, hp, cost, level, exp, maxExp, gold, turn) VALUES " +
            $"({playerNum}, {DataBase.instance.defaultData[i].strength}, {DataBase.instance.defaultData[i].intelligence}, {DataBase.instance.defaultData[i].luck}, {DataBase.instance.defaultData[i].speed}, {DataBase.instance.defaultData[i].hp}, {DataBase.instance.defaultData[i].hp}, {DataBase.instance.defaultData[i].cost}, {DataBase.instance.defaultData[i].level}, {DataBase.instance.defaultData[i].exp}, {DataBase.instance.defaultData[i].maxExp}, {0}, {0})";
                
            }
        }
        return null;
    }
    private string AvatarCardSetting(string playerNum, string major)
    {
        int[] card = new int[40];
        for (int i = 0; i < 40; i++)
        {
            card[i] = 0;
        }
        int cardCount = 0;
        for (int i = 0; i < DataBase.instance.defaultData.Count; i++)
        {
            if (major == DataBase.instance.defaultData[i].major.ToString())
            {
                cardCount = DataBase.instance.defaultData[i].card1Count;
                for (int j = 0; j < cardCount; j++)
                {
                    card[j] = DataBase.instance.defaultData[i].card1;
                }
                for (int j = cardCount; j < cardCount + DataBase.instance.defaultData[i].card2Count; j++)
                {
                    card[j] = DataBase.instance.defaultData[i].card2;
                }
                cardCount += DataBase.instance.defaultData[i].card2Count;
                for (int j = cardCount; j < cardCount + DataBase.instance.defaultData[i].card3Count; j++)
                {
                    card[j] = DataBase.instance.defaultData[i].card3;
                }

                string query = "INSERT INTO Deck (playerNum";
                for (int j = 1; j < 41; j++)
                {
                    query += ", no" + j.ToString();
                }
                query += ") VALUES (" + playerNum;

                for (int j = 0; j < 40; j++)
                {
                    query += ", " + card[j];
                }
                query += ")";
                return query;
            }
        }
        return null;
    }

    #endregion

    #region 비동기 씬전환

    public void LoadScenceName(string name)
    {
        sceneName = name;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        //yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); //씬전환 연출 애니메이션
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        if (players.Count != 0)
        {
            DataBase.instance.ResetDB();
            for (int i = 0; i < players.Count; i++)
            {
                DataBase.instance.SaveDB(players[i].GetComponent<Character_type>().GetTypeDBQuery());
                DataBase.instance.SaveDB(players[i].GetComponent<Character>().GetStatDBQuery());
                DataBase.instance.SaveDB(players[i].GetComponent<Character_Card>().GetCardDBQuery());
            }
            DataBase.instance.LoadData();
        }

        while(!op.isDone)
        {
            if(op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1.0f);
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    #endregion
}
