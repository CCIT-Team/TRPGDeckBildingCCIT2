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

    #region Button�Լ�
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

    private bool Start_Exception() //���� �� ���嵥���� ����ó��
    {
        return DataBase.instance.IsEmptyDB();
    }

    private void IsLoadData() //������ �� ���嵥���� �ִ��� ������ �Ǵ� �� �ε��ư ��/Ȱ��ȭ
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
