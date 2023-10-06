using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI_Manager : MonoBehaviour
{
    public void PlayButton(string sceneName)
    {
        if (Start_Exception())
        {
            GameManager.instance.LoadScenceName(sceneName);
        }
    }
    
    private bool Start_Exception()
    {
        return true;
    }
}
