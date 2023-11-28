using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondMissionChat : MonoBehaviour
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
                talk = "��, ��ŵ��� ���谡�е��̽ñ���.";
                break;

            case 1:
                talk = "�ݰ����ϴ�. �� �ź� �׷����Դϴ�.";
                break;

            case 2:
                talk = "���� �ձ� ��ó�� ��Ȳ�� ���� �ʽ��ϴ�. �巡���� ���� �����̰�����.";
                break;

            case 3:
                talk = "�α� ���� �ֹε鿡�� ���ذ� ����°͵� �ð� ������ �� �����ϴ�.";
                break;
            case 4:
                talk = "��ó�� �ִ� ������������ �ذ� ������ �ôٴ� ������ �ֽ��ϴ�.";
                break;
            case 5:
                talk = "�� ���͵��� ó���� �ֽðڽ��ϱ�?";
                break;
            case 6:
                talk = "���� ��ȣ�� �ֱ⸦....";
                break;
            case 7:
                Map.instance.currentMissionTile.GetComponent<Tile>().isMissionOn = false;
                Map.instance.currentMissionTile.GetComponent<Tile>().MainMissionMarkerOnOff();
                Map.instance.isOutofUI = false;
                Map.instance.wolrdMission.mainMissionNum = 2;
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
        SoundManager.instance.PlayUICilckSound();
        if (chatint < 7)
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
