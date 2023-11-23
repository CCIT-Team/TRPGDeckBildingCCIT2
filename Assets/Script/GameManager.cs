using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum SceneType
    {
        none,
        Wolrd,
        Battle
    }
    public static GameManager instance = null;
    public SceneType currentScene;
    public List<GameObject> players = new List<GameObject>();
    private List<int> deliveryMonsterData = new List<int>();
    public GameObject map;
    public bool isVictory;
    public GameObject playerUI;
    private GameObject loading_Panel;
    private Slider loadingBar;
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
        //ui 연결
        playerUI.GetComponent<PlayerUIManager>().SetPlayer();
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

    public void SetBattleMonsterSetting(List<int> mosterNo) //맵에서 몬스터 리스트 값 저장
    {
        deliveryMonsterData.Clear();
        deliveryMonsterData = mosterNo.ToList();       
    }

    public List<int> GetBattleMonsterSetting() //저장되어있는 리스트 값 가져오기
    {
        if (deliveryMonsterData.Any())
            return deliveryMonsterData;
        else
            return null;
    }

    private void LoadMonster(int index, Vector3 position)
    {
        for (int i = 0; i < DataBase.instance.monsterData.Count; i++)
        {
            if (DataBase.instance.monsterData[i].no == index)
            {
                GameObject unit = Instantiate(Resources.Load("Prefabs/Monster/Monster", typeof(GameObject))) as GameObject;
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
        loading_Panel = Instantiate(Resources.Load("Prefabs/UI/Loading_Panel", typeof(GameObject))) as GameObject;
        //loading_Panel.transform.SetParent(GameObject.Find("Canvas").transform);
        loadingBar = loading_Panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        StartCoroutine(LoadScene());
        if(name == "New Battle")
        {
            currentScene = SceneType.Battle;
        }
        else if(name == "Map1")
        {
            currentScene = SceneType.Wolrd;
        }
    }

    private IEnumerator LoadScene()
    {
        //yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); //씬전환 연출 애니메이션
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;

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
            yield return null;
            timer += Time.deltaTime;
            if(op.progress < 0.9f)
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, op.progress, timer);
                if(loadingBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1.0f, timer);
                if(loadingBar.value == 1.0f)
                {
                    //loadingBar.value = 0.9f;
                    yield return new WaitForSeconds(1.0f);
                    op.allowSceneActivation = true;                  
                }
                yield return null;
            }

            //if (op.progress >= 0.9f)
            //{

            //    yield return new WaitForSeconds(1.0f);
            //    op.allowSceneActivation = true;
            //}
            //yield return null;
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
