using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwelfthMainMission : MonoBehaviour
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
        Map.instance.isOutofUI = true;
        delay = 0.05f;
    }

    void ChatFlow()
    {
        switch (chatint)
        {
            case 0:
                talk = "������ ���� ������ �޾Ҵٱ���....?";
                break;
            case 1:
                talk = "���� �ȵ�................";
                break;
            case 2:
                text.color = Color.red;
                delay = 0.01f;
                talk = "������ ������ ������ ������ ������ ������ ������ ������ " +
                    "������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������" +
                    "������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������ ������";
                break;
            case 3:
                Map.instance.currentMissionTile.isMissionOn = false;
                Map.instance.currentMissionTile.MainMissionMarkerOnOff();
                Map.instance.isOutofUI = false;
                Map.instance.missionNum = 13;
                Map.instance.startTile = null;
                Map.instance.pathTileObjectList.Clear();
                Map.instance.isPlayerOnEndTile = true;
                Map.instance.currentInteracteUITile = null;
                Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
                Map.instance.isOutofUI = false;
                gameObject.SetActive(false);
                break;
        }

    }

    public void NextChat()
    {
        if (chatint < 3)
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