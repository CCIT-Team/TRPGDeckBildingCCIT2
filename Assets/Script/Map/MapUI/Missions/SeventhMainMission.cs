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
                talk = "����.... ���谡��!";
                break;
            case 1:
                talk = "�۱������� ���� ��Ź�帮�� ���� ���� �ֽ��ϴ�.";
                break;
            case 2:
                talk = "�ֱ� ��Ÿ�� �붧���� ���� �ֺ��� ���͵��� �þ,";
                break;
            case 3:
                talk = "��ó ������ ��ô� ��Ӵϰ� �����˴ϴ�.";
                break;
            case 4:
                talk = "���� ���� ���� �������ؼ� ���� �ڸ��� ������ �����ϴ�.";
                break;
            case 5:
                talk = "���� ��Ź�帳�ϴ� ���谡��.............";
                break;
            case 6:
                talk = " �ٷ� �� ������ ���� ���� �ϳ��� �缭 ��Ӵϲ� �������ּ���.";
                break;
            case 7:
                talk = "����� �� �ִٰ� �����ּ���.........��ʴ� �� �ϰڽ��ϴ�.........";
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
