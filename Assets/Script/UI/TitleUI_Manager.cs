using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI_Manager : MonoBehaviour
{
    public GameObject panel;
    public GameObject loadButton;
    private void Start()
    {
        IsLoadData();
    }

    #region Button함수
    public void PlayButton(string sceneName)
    {
        if (Start_Exception())
        {
            GameManager.instance.LoadScenceName(sceneName);
        }
        else
        {
            panel.gameObject.SetActive(true);
        }
    }
    
    public void ResetDataPlay(string sceneName)
    {
        DataBase.instance.ResetDB();
        GameManager.instance.LoadScenceName(sceneName);
    }

    public void LoadButton(string sceneName)
    {
        GameManager.instance.LoadScenceName(sceneName);
    }

    public void ExitButton()
    {

    }
    #endregion

    private bool Start_Exception() //시작 시 저장데이터 예외처리
    {
        return DataBase.instance.IsEmptyDB();
    }

    private void IsLoadData() //시작할 때 저장데이터 있는지 없는지 판단 후 로드버튼 비/활성화
    {
        if (Start_Exception())
        {
            loadButton.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            loadButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            DataBase.instance.LoadData();
            loadButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            loadButton.GetComponent<Button>().enabled = true;
        }
    }
}
