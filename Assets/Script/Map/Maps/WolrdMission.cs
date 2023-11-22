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

    public GameObject mainMissionUIObject;
    public GameObject subMissionUIObject;

    public GameObject mainMission;
    public List<GameObject> subMissions = new List<GameObject>(3);

    int mainMissionNum = 0;

    void Start()
    {
        Map.instance.wolrdMission = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOffMissionWindow()
    {
        if(!isWindowOn)
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
        switch(mainMissionNum)
        {
            case 0:
                AddMainMission();
                break;
            case 1:

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


}
