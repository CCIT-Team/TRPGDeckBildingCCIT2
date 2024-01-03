using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    public bool isEndScript = false;
    public float delay;
    public TMP_Text text;
    public TMP_Text nexttext;
    string talk;
    string mainmissiontext;
    int t = 0;
    int chatint = 0;

    List<Dictionary<string, object>> data_Dialog;

    void Start()
    {
        data_Dialog = CSVReader.Read("MissionCSV/MissionDialog");
        mainmissiontext = "";
        SCVDataReadAndSet();
        t = 0;
        StartCoroutine(Output_text());
        Map.instance.isOutofUI = true;
    }

    public void OnStartMission()
    {
        data_Dialog = CSVReader.Read("MissionCSV/MissionDialog");
        SCVDataReadAndSet();
        t = 0;
        StartCoroutine(Output_text());
        Map.instance.isOutofUI = true;
    }

    public void NextChat()
    {
        SoundManager.instance.PlayUICilckSound();
        if ((int)data_Dialog[Map.instance.missionChatNum]["Chapter"] == Map.instance.missionNum)
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
        if(data_Dialog[Map.instance.missionChatNum]["Chracter"].ToString() != "")
        {
            Map.instance.wolrdMission.missionCharacter.sprite = Map.instance.wolrdMission.missionChraterImage[(int)data_Dialog[Map.instance.missionChatNum]["Chracter"]];
        }
        if (data_Dialog[Map.instance.missionChatNum]["ChracterName"].ToString() != "")
        {
            Map.instance.wolrdMission.chracterName.text = data_Dialog[Map.instance.missionChatNum]["ChracterName"].ToString();
        }
    }

    IEnumerator Output_text()
    {
        nexttext.enabled = false;
        if (data_Dialog[Map.instance.missionChatNum]["Content"].ToString() != "") 
        { text.text += data_Dialog[Map.instance.missionChatNum]["Content"].ToString()[t]; }
        else 
        {
            if (data_Dialog[Map.instance.missionChatNum]["Battle"].ToString() == "")
            {
                isEndScript = true;
                Map.instance.startTile = null;
                Map.instance.currentInteracteUITile = null;
                Map.instance.isOutofUI = false;
                gameObject.SetActive(false);        
                Map.instance.wolrdMission.NextMission();
                StopCoroutine(Output_text());
            }
            StopCoroutine(Output_text());
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
