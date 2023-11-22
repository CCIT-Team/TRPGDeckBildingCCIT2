using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WolrdMission : MonoBehaviour
{
    public Animator missionButtonAni;
    bool isWindowOn = false;
    public TMP_Text mainMissionText;
    public TMP_Text subMissionText;

    public Transform mainMissionUITransform;
    public Transform subMissionUITransform;

    public GameObject firstMainMission;
    public GameObject subMissionUIObject;

    public List<GameObject> missions = new List<GameObject>();
    public bool missionCleard = false;

    public int mainMissionNum = 0;

    void Start()
    {
        Map.instance.wolrdMission = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMissionNum();
    }

    public void OnOffMissionWindow()
    {
        if (!isWindowOn)
        {
            missionButtonAni.SetBool("OnOff", true);
            isWindowOn = true;
        }
        else
        {
            missionButtonAni.SetBool("OnOff", false);
            isWindowOn = false;
        }
    }

    public void CheckMissionNum()
    {
        switch (mainMissionNum)
        {
            case 0:
                    FirstMainMission();
                break;
            case 1:
                    SecondMainMission();
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
    }

    void FirstMainMission()
    {
        mainMissionText.text = "왕국으로 가세요";
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.kingdomTile[0].GetComponent<Tile>().isMissionOn = true;
        Map.instance.kingdomTile[0].GetComponent<Tile>().MissionMarkerOnOff();
    }

    void SecondMainMission()
    {
        mainMissionText.text = "몬스터를 사냥하세요";
        Map.instance.currentMissionTile = Map.instance.monsterTile[0];
        Map.instance.monsterTile[0].GetComponent<Tile>().isMissionOn = true;
        Map.instance.monsterTile[0].GetComponent<Tile>().MissionMarkerOnOff();
    }
}
