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
        //avatar[0].SetUnitData(DataBase.instance.stat[0]);
        //avatar[1].SetUnitData(DataBase.instance.stat[1]);
        //avatar[2].SetUnitData(DataBase.instance.stat[2]);
        //LoadScenceName("Character");
    }

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
                break;
            case 1:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({avatar_1[0]}, '{avatar_1[1]}', '{avatar_1[2]}', '{avatar_1[3]}', '{avatar_1[4]}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_1[0], avatar_1[2]));
                break;
            case 2:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({avatar_2[0]}, '{avatar_2[1]}', '{avatar_2[2]}', '{avatar_2[3]}', '{avatar_2[4]}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_2[0], avatar_2[2]));
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
               return insertQuery = $"INSERT INTO Stat (playerNum, strength, intelligence, luck, speed, currentHp, hp, cost, level, exp, maxExp) VALUES " +
            $"({playerNum}, {DataBase.instance.defaultData[i].strength}, {DataBase.instance.defaultData[i].intelligence}, {DataBase.instance.defaultData[i].luck}, {DataBase.instance.defaultData[i].speed}, {DataBase.instance.defaultData[i].hp}, {DataBase.instance.defaultData[i].hp}, {DataBase.instance.defaultData[i].cost}, {DataBase.instance.defaultData[i].level}, {DataBase.instance.defaultData[i].exp}, {DataBase.instance.defaultData[i].maxExp})";
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
