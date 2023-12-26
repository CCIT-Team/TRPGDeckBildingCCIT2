using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission : MonoBehaviour
{
    public float delay;
    public TMP_Text text;
    public TMP_Text nexttext;
    string talk;
    int t = 0;
    int chatint = 0;
    int mainMissionnum = 1;

    List<Dictionary<string, object>> data_Dialog;

    void Start()
    {
        data_Dialog = CSVReader.Read("MissionCSV/MissionDialog");
        mainMissionnum = Map.instance.missionNum;
        StartCoroutine(Output_text());
        Map.instance.isOutofUI = true;
    }

    void Update()
    {

    }

    void ChatFlow()
    {
        switch (mainMissionnum)
        {

        }


    }

    public void NextChat()
    {
        SoundManager.instance.PlayUICilckSound();
        //if (chatint < data_Dialog.Count-1 && (int)data_Dialog[chatint]["Chapter"] == mainMissionnum)
        //{
        //    t = 0;
        //    text.text = "";
        //    chatint++;
        //    StartCoroutine(output_text());

        if ((int)data_Dialog[chatint]["Chapter"] == Map.instance.missionNum)
        {
            t = 0;
            text.text = "";
            chatint++;
            StartCoroutine(Output_text());
        }
        else if(data_Dialog[chatint]["Chapter"] == null )
        {
            Map.instance.isOutofUI = false;
            //Map.instance.wolrdMission.mainMissionNum = 1;
            Map.instance.missionNum += 1;
            Map.instance.startTile = null;
            Map.instance.currentInteracteUITile = null;
            Map.instance.isOutofUI = false;
            chatint++;
            gameObject.SetActive(false);
        }
        else
        {
            Map.instance.isOutofUI = false;
            //Map.instance.wolrdMission.mainMissionNum = 1;
            Map.instance.missionNum += 1;
            Map.instance.startTile = null;
            Map.instance.currentInteracteUITile = null;
            Map.instance.isOutofUI = false;
            chatint++;
            gameObject.SetActive(false);
        }
    }
    IEnumerator Output_text()
    {
        nexttext.enabled = false;
        if(data_Dialog[chatint]["Content"].ToString() != "") { text.text += data_Dialog[chatint]["Content"].ToString()[t]; }
        else {StopCoroutine(Output_text()); }
        yield return new WaitForSeconds(delay);
        if (t < data_Dialog[chatint]["Content"].ToString().Length - 1)
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
