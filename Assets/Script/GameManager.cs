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
    private string sceneName = null; //scene����

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
    #region �κ�� �ƹ�Ÿ ���� -> ������Ʈ ȭ

    public void GetLobbyAvatar()
    {
        for (int i = 0; i < avatarCounter; i++)
        {
            CreateAvatar(i);
        }
    }
    private void CreateAvatar(int index)
    {        
        GameObject unit = Instantiate(Resources.Load("Test_Assets/Prefab/Avatar", typeof(GameObject))) as GameObject;
        unit.transform.position = Vector3.zero;//���߿� �� ������ �޾ƿð���
        switch (index)
        {
            case 0:
                unit.GetComponent<Character_type>().SetUnitType(avatar_0[0], (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), avatar_0[1]), (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), avatar_0[2]), (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), avatar_0[3]));
                break;
            case 1:
                unit.GetComponent<Character_type>().SetUnitType(avatar_1[0], (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), avatar_1[1]), (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), avatar_1[2]), (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), avatar_1[3]));
                break;
            case 2:
                unit.GetComponent<Character_type>().SetUnitType(avatar_2[0], (PlayerType.Major)Enum.Parse(typeof(PlayerType.Major), avatar_2[1]), (PlayerType.Sex)Enum.Parse(typeof(PlayerType.Sex), avatar_2[2]), (PlayerType.AvatarType)Enum.Parse(typeof(PlayerType.AvatarType), avatar_2[3]));
                break;
        }
    }

    #endregion

    #region �񵿱� ����ȯ

    public void LoadScenceName(string name)
    {
        sceneName = name;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        //yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); //����ȯ ���� �ִϸ��̼�
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
