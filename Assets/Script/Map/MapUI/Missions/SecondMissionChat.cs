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
                talk = "아, 당신들이 모험가분들이시군요.";
                break;

            case 1:
                talk = "반갑습니다. 전 신부 그레고리입니다.";
                break;

            case 2:
                talk = "현재 왕국 근처에 상황이 좋지 않습니다. 드래곤의 영향 때문이겠지요.";
                break;

            case 3:
                talk = "인근 마을 주민들에게 피해가 생기는것도 시간 문제일 것 같습니다.";
                break;
            case 4:
                talk = "근처에 있는 공동묘지에서 해골 무리를 봤다는 전보가 있습니다.";
                break;
            case 5:
                talk = "그 몬스터들을 처리해 주시겠습니까?";
                break;
            case 6:
                talk = "신의 가호가 있기를....";
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
