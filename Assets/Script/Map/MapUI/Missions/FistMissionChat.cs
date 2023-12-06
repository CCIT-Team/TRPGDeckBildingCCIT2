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
        Map.instance.isOutofUI = true;
        delay = 0.05f;
    }

    void ChatFlow()
    {
        switch (chatint)
        {
            case 0:
                talk = "어서오게, 모험가들이여";
                break;

            case 1:
                talk = "나는 아발로니아의 왕 아발론 3세라네.";
                break;

            case 2:
                talk = "현재 아르카나 대륙에 화염룡이 나타나 성과 마을을 불태우고 있다네.";
                break;

            case 3:
                talk = "용의 등장으로 몬스터들이 난폭해져서 곤란한 상황이네.......";
                break;
            case 4:
                talk = "자네들에게 그 드래곤을 토벌하는 임무를 맡기겠네.";
                break;
            case 5:
                talk = "이 임무를 완수한다면 큰 상을 내리겠네.";
                break;
            case 6:
                talk = "우선 페드로 성당에 그레고리 신부에게 내 말을 전해주겠나? 그가 자네들을 도울걸세.";
                break;
            case 7:
                Map.instance.isOutofUI = false;
                Map.instance.wolrdMission.mainMissionNum = 1;
                Map.instance.startTile = null;
                Map.instance.currentInteracteUITile = null;
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
