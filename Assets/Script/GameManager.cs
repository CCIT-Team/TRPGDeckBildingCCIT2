using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<GameObject> players = new List<GameObject>();
    public GameObject map;
    private string sceneName = null; //scene변경


    #region 싱글턴 Awake

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

    #endregion

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
        GameObject unit = Instantiate(Resources.Load("Prefabs/Character/Player1", typeof(GameObject))) as GameObject;
        //unit.transform.position = position;//나중에 맵 포지션 받아올거임
        AvatarTypeSetting(unit, index);
        AvatarPositionSetting(unit, index);
        AvatarStatSetting(unit, index);
        AvatarCardSetting(unit, index);
        if (unit.GetComponent<Character_type>().pos == Vector3.zero)
        {
            unit.transform.position = position;//나중에 맵 포지션 받아올거임
        }
        else
        {
            unit.transform.position = unit.GetComponent<Character_type>().pos;
        }
        //Debug.Log(SceneManager.GetActiveScene().name.ToString());
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
    private void AvatarPositionSetting(GameObject unit, int index)
    {
        unit.GetComponent<Character_type>().SetUnitPosition(DataBase.instance.loadPositionData[index]);
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

                if (SceneManager.GetActiveScene().name == "New Battle")
                {
                    DataBase.instance.SaveDB(players[i].GetComponent<Character_type>().GetWorldPositionDBQuery());
                }
                else
                {
                    DataBase.instance.SaveDB(players[i].GetComponent<Character_type>().GetPositionDBQuery());
                }


                DataBase.instance.SaveDB(players[i].GetComponent<Character>().GetStatDBQuery());
                DataBase.instance.SaveDB(players[i].GetComponent<Character_Card>().GetCardDBQuery());
            }
            DataBase.instance.LoadData();
        }  

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1.0f);
                op.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.sceneLoaded += ActiveSceneMap;
    }


    void ActiveSceneMap(Scene scene0, LoadSceneMode mode)
    {
        if (scene0.name == "Map1" && map != null && !map.activeSelf) { map.SetActive(true); Map.instance.ReSearchPlayer(); }
        else if (scene0.name != "Map1" && map != null) { map.SetActive(false); }
    }
    #endregion
}
