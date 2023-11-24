using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FistMissionChat : MonoBehaviour
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
                talk = "�ȳ��Ͻʴϱ�" + " " +  Map.instance.wolrdTurn.currentPlayer.name;
                break;

            case 1:
                talk = "���� �ź� �׷������ �մϴ�.";
                break;

            case 2:
                talk = "���� �Ƹ�ī�� ����� ȭ������ ��Ÿ�� ���� ������ ���¿�� �ֽ��ϴ�.";
                break;

            case 3:
                talk = Map.instance.wolrdTurn.currentPlayer.name  + " "+ "�̽ÿ�.....";
                break;
            case 4:
                talk = "���� ������ �¿������ ������ֽʽÿ�.";
                break;
            case 5:
                talk = "���� ��Ǯ��� ��ó�� �ִ� ���� ������ ����غ��ñ�.........";
                break;
            case 6:
                Map.instance.currentMissionTile.GetComponent<Tile>().isMissionOn = false;
                Map.instance.currentMissionTile.GetComponent<Tile>().MainMissionMarkerOnOff();
                Map.instance.isOutofUI = false;
                Map.instance.wolrdMission.mainMissionNum = 1;
                gameObject.SetActive(false);
                break;


        }

    }

    public void NextChat()
    {
        SoundManager.instance.PlayUICilckSound();
        if (chatint < 6)
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
