using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeventhMainMission : MonoBehaviour
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
                talk = "저기.... 모험가님!";
                break;
            case 1:
                talk = "송구하지만 제가 부탁드리고 싶은 것이 있습니다.";
                break;
            case 2:
                talk = "최근 나타난 용때문에 마을 주변에 몬스터들이 늘어서,";
                break;
            case 3:
                talk = "근처 마을에 사시는 어머니가 걱정됩니다.";
                break;
            case 4:
                talk = "저는 마을 방어선을 만들어야해서 쉽게 자리를 비울수가 없습니다.";
                break;
            case 5:
                talk = "제발 부탁드립니다 모험가님.............";
                break;
            case 6:
                talk = " 바로 옆 성에서 작은 포션 하나만 사서 어머니께 전달해주세요.";
                break;
            case 7:
                talk = "저희는 잘 있다고 전해주세요.........사례는 꼭 하겠습니다.........";
                break;
            case 8:
                Map.instance.currentMissionTile.isMissionOn = false;
                Map.instance.currentMissionTile.MainMissionMarkerOnOff();
                Map.instance.wolrdMission.mainMissionNum = 7;
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
        if (chatint < 8)
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
