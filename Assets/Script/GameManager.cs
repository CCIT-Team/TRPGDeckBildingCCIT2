using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
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

    public void LoadAvatar(int index, Vector3 position)
    {
        GameObject unit = Instantiate(Resources.Load("Test_Assets/Prefab/Avatar", typeof(GameObject))) as GameObject;
        //N_BattleManager.instance.player = unit;
        unit.transform.position = position;//나중에 맵 포지션 받아올거임

        AvatarTypeSetting(unit, index);
        AvatarStatSetting(unit, index);
    }

    #region 로비씬 아바타 세팅 -> 오브젝트 화

    public void GetLobbyAvatar(Vector3 position)
    {
        for (int i = 0; i < avatarCounter; i++)
        {
            CreateAvatar(i, position);
        }
    }
    public void GetLoadAvatar(Vector3 position)
    {
        for (int i = 0; i < DataBase.instance.loadTypeData.Count; i++)
        {
            LoadAvatar(i, position);
        }
    }

    private void CreateAvatar(int index, Vector3 position)
    {        
        GameObject unit = Instantiate(Resources.Load("Test_Assets/Prefab/Avatar", typeof(GameObject))) as GameObject;
        Map.instance.player =  unit;
        unit.transform.position = position;//나중에 맵 포지션 받아올거임
        switch (index)
        {
            case 0:
                unit.GetComponent<Character_type>().SetUnitType(int.Parse(avatar_0[0]), avatar_0[1],(PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), avatar_0[2]), (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), avatar_0[3]), (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), avatar_0[4]));
                AvatarStatSetting(unit);
                DataBase.instance.Deliver_column(unit.GetComponent<Character_type>().GetTypeDBQuery(), unit.GetComponent<Character>().GetStatDBQuery());
                break;
            case 1:
                unit.GetComponent<Character_type>().SetUnitType(int.Parse(avatar_1[0]), avatar_1[1], (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), avatar_1[2]), (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), avatar_1[3]), (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), avatar_1[4]));
                AvatarStatSetting(unit);
                DataBase.instance.Deliver_column(unit.GetComponent<Character_type>().GetTypeDBQuery(), unit.GetComponent<Character>().GetStatDBQuery());
                break;
            case 2:
                unit.GetComponent<Character_type>().SetUnitType(int.Parse(avatar_2[0]), avatar_2[1], (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), avatar_2[2]), (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), avatar_2[3]), (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), avatar_2[4]));
                AvatarStatSetting(unit);
                DataBase.instance.Deliver_column(unit.GetComponent<Character_type>().GetTypeDBQuery(), unit.GetComponent<Character>().GetStatDBQuery());
                break;
        }
    }

    private void AvatarStatSetting(GameObject unit)
    {
        for (int i = 0; i < DataBase.instance.defaultData.Count; i++)
        {
            if (unit.GetComponent<Character_type>().major == DataBase.instance.defaultData[i].major)
            {
                unit.GetComponent<Character>().SetUnitData(DataBase.instance.defaultData[i]);
            }
        }
    }
    private void AvatarTypeSetting(GameObject unit, int index)
    {
        unit.GetComponent<Character_type>().SetUnitType(DataBase.instance.loadTypeData[index]);
    }
    private void AvatarStatSetting(GameObject unit, int index)
    {
        unit.GetComponent<Character>().SetUnitData(DataBase.instance.loadStatData[index]);
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
