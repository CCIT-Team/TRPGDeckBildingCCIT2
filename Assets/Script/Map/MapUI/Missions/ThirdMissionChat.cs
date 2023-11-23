using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThirdMissionChat : MonoBehaviour
{
    public float delay;
    public TMP_Text text;
    public TMP_Text nexttext;
    string talk;
    int t = 0;

    int chatint = 0;
    void Start()
    {
        StartCoroutine(output_text());
        delay = 0.1f;
    }

    void ChatFlow()
    {
        switch (chatint)
        {
            case 0:
                talk = "안녕하십니까" + " " +  Map.instance.wolrdTurn.currentPlayer.name;
                break;

            case 1:
                talk = "저는 그리라고 합니다.";
                break;

            case 2:
                talk = "현재 르카나 대이 나타나 성우고 있니다.";
                break;

            case 3:
                talk = Map.instance.wolrdTurn.currentPlayer.name  + " "+ ".....";
                break;
            case 4:
                talk = "용이 든것을 태전에 벌십시요.";
                break;
            case 5:
                talk = " 있는 몬스터 무리를 토벌시길.........";
                break;
            case 6:
                Map.instance.currentMissionTile.GetComponent<Tile>().isMissionOn = false;
                Map.instance.currentMissionTile.GetComponent<Tile>().MainMissionMarkerOnOff();
                Map.instance.isOutofUI = false;
                Map.instance.wolrdMission.mainMissionNum = 3;
                gameObject.SetActive(false);
                break;


        }

    }

    public void NextChat()
    {
        if(chatint < 6)
        {
            t = 0;
            text.text = "";
            chatint++;
            StartCoroutine(output_text());
        }
    }

    IEnumerator output_text()
    {
        nexttext.enabled = false;
        ChatFlow();
        text.text += talk[t];
        yield return new WaitForSeconds(delay);
        if (t < talk.Length - 1)
        {
            t++;
            StartCoroutine(output_text());
        }
        else
        {
            nexttext.enabled = true;
        }
    }

}
