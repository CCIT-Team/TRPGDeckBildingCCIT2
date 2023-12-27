using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    public WolrdMission wolrdMission;
    public bool isEndScript = false;
    public float delay;
    public TMP_Text text;
    public TMP_Text nexttext;
    string talk;
    int t = 0;
    int chatint = 0;

    List<Dictionary<string, object>> data_Dialog;

    void Start()
    {
        data_Dialog = CSVReader.Read("MissionCSV/MissionDialog");
        SCVDataReadAndSet();
        StartCoroutine(Output_text());
        Map.instance.isOutofUI = true;
    }

    public void NextChat()
    {
        SoundManager.instance.PlayUICilckSound();
        if ((int)data_Dialog[Map.instance.missionChatNum]["Chapter"] == wolrdMission.mainMissionNum)
        {
            t = 0;
            text.text = "";
            Map.instance.missionChatNum++;
            StartCoroutine(Output_text());
        }
    }

    public void SCVDataReadAndSet()
    {
        Debug.Log("¹Ù²ã!");
        data_Dialog = CSVReader.Read("MissionCSV/MissionDialog");
        wolrdMission.missionCharacter.sprite = wolrdMission.missionChraterImage[(int)data_Dialog[Map.instance.missionChatNum]["Chracter"]];
        wolrdMission.chracterName.text = data_Dialog[Map.instance.missionChatNum]["ChracterName"].ToString();
        wolrdMission.mainMissionText.text = data_Dialog[Map.instance.missionChatNum]["MainMissionText"].ToString();
    }

    IEnumerator Output_text()
    {
        nexttext.enabled = false;
        if (data_Dialog[Map.instance.missionChatNum]["Content"].ToString() != "") { text.text += data_Dialog[Map.instance.missionChatNum]["Content"].ToString()[t]; }
        else 
        {
            isEndScript = true;
            Map.instance.startTile = null;
            Map.instance.currentInteracteUITile = null;
            Map.instance.isOutofUI = false;
            Map.instance.missionChatNum += 1;
            gameObject.SetActive(false);
            wolrdMission.NextMission();
            //StopCoroutine(Output_text());
        }
        yield return new WaitForSeconds(delay);
        if (t < data_Dialog[Map.instance.missionChatNum]["Content"].ToString().Length - 1)
        {
            t++;
            StartCoroutine(Output_text());
        }
        else
        {
            nexttext.enabled = true;
        }
    }
}
