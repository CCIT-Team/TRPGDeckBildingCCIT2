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
                talk = "�����, ���谡���̿�";
                break;

            case 1:
                talk = "���� �ƹ߷δϾ��� �� �ƹ߷� 3�����.";
                break;

            case 2:
                talk = "���� �Ƹ�ī�� ����� ȭ������ ��Ÿ�� ���� ������ ���¿�� �ִٳ�.";
                break;

            case 3:
                talk = "���� �������� ���͵��� ���������� ����� ��Ȳ�̳�.......";
                break;
            case 4:
                talk = "�ڳ׵鿡�� �� �巡���� ����ϴ� �ӹ��� �ñ�ڳ�.";
                break;
            case 5:
                talk = "�� �ӹ��� �ϼ��Ѵٸ� ū ���� �����ڳ�.";
                break;
            case 6:
                talk = "�켱 ���� ���翡 �׷��� �źο��� �� ���� �����ְڳ�? �װ� �ڳ׵��� ����ɼ�.";
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
