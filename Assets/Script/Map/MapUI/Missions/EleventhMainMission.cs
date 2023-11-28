using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EleventhMainMission : MonoBehaviour
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
                talk = "(인기척이 느껴지지 않는다......................)";
                break;
            case 1:
                talk = "(불타는 소리만 들려온다........................)";
                break;
            case 2:
                Map.instance.currentMissionTile.isMissionOn = false;
                Map.instance.currentMissionTile.MainMissionMarkerOnOff();
                Map.instance.isOutofUI = false;
                Map.instance.wolrdMission.mainMissionNum =12;
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
        if(chatint < 2)
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
