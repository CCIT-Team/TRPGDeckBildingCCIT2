using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI_Manager : MonoBehaviour
{
    public GameObject panel;
    public GameObject loadButton;
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
    
    private bool Start_Exception()
    {
        return DataBase.instance.EmptyDB();
    }

    private void Start()
    {
        if (Start_Exception())
        {
            
        }
        else
        {
            panel.gameObject.SetActive(true);
        }
    }
}
