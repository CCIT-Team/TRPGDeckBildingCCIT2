using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]private List<Character> avatar = new List<Character>();
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
        //StartCoroutine(LoadScene());
    }

    #region 비동기 씬전환

    public void LoadScenceName(string name)
    {
        sceneName = name;
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
