using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private GameObject panel;

    private void Start()
    {
        panel = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            Time.timeScale = 0;
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
        panel.SetActive(false);
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
