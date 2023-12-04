using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SixthMainMission : MonoBehaviour
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
                talk = "���� �����մϴ�!";
                break;
            case 1:
                talk = "���谡�Ե鲲���� ���� ������ �����̼���!";
                break;
            case 2:
                talk = "������ �帮�� ������ ���� ���� ������ ���ðھ��?";
                break;
            case 3:
                Map.instance.wolrdMission.mainMissionNum = 6;
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
        if(chatint < 3)
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
