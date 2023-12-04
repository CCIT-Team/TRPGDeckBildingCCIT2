using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private GameObject panel;
    private bool ispause;

    private void Start()
    {
        ispause = false;
        panel = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !ispause)
        {
            ispause = true;
            panel.SetActive(ispause);
            Map.instance.isOutofUI = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && ispause)
        {
            PlayButton();
            Map.instance.isOutofUI = false;
        }
    }

    #region ButtonÇÔ¼ö
    public void PlayButtonSound()
    {
        SoundManager.instance.PlayUICilckSound();
    }

    public void PlayButton()
    {
        Time.timeScale = 1;
        ispause = false;
        panel.SetActive(ispause);
    }

    public void BackToTitleButton()
    {
        GameManager.instance.LoadScenceName("Title");
    }

    public void GameSetting()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion
}
