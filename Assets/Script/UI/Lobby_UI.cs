using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class Lobby_UI : MonoBehaviour
{
    List<string> majorList = new List<string>();
    List<string> avatarSexList = new List<string>();
    List<string> avatarTypeList = new List<string>();

    TMP_InputField avatarNickName;
    TMP_Text majorText;
    TMP_Text avatarSexText;
    TMP_Text avatarTypeText;
    TMP_Text skinColorText; //아직 없음

    public Button[] next_button;
    public Button[] pre_button;

    string avatarNickName_index = null; //연결 안댐
    int major_index = 0;
    int avatarSex_index = 0;
    int avatarType_index = 0;
    int skinColor_index = 0; //아직 없음
    
    private void Start()
    {
        avatarNickName = transform.GetChild(0).GetComponent<TMP_InputField>();
        majorText = transform.GetChild(1).GetComponent<TMP_Text>();
        avatarSexText = transform.GetChild(2).GetComponent<TMP_Text>();
        avatarTypeText = transform.GetChild(3).GetComponent<TMP_Text>();
        skinColorText = transform.GetChild(4).GetComponent<TMP_Text>();

        majorList = Enum.GetNames(typeof(PlayerStat.Major)).ToList();
        avatarSexList = Enum.GetNames(typeof(PlayerStat.Sex)).ToList();
        avatarTypeList = Enum.GetNames(typeof(PlayerStat.AvatarType)).ToList();
        //피부색 enum -> List

        majorText.text = majorList[major_index];
        avatarSexText.text = avatarSexList[avatarSex_index];
        avatarTypeText.text = avatarTypeList[avatarType_index];
        //피부색 text

        next_button[0].onClick.AddListener(() => NextButton(majorText));
        next_button[1].onClick.AddListener(() => NextButton(avatarSexText));
        next_button[2].onClick.AddListener(() => NextButton(avatarTypeText));
        //피부색 next버튼

        pre_button[0].onClick.AddListener(() => PreButton(majorText));
        pre_button[1].onClick.AddListener(() => PreButton(avatarSexText));
        pre_button[2].onClick.AddListener(() => PreButton(avatarTypeText));
        //피부색 pre버튼
    }

    public void NextButton(TMP_Text text)
    {
        switch(text.name)
        {
            case "Major":
                if (major_index >= majorList.Count -1)
                    major_index = -1; 

                majorText.text = majorList[++major_index];
                break;
            case "AvatarSex":
                if (avatarSex_index >= avatarSexList.Count -1)
                    avatarSex_index = -1;

                avatarSexText.text = avatarSexList[++avatarSex_index];
                break;
            case "AvatarType":
                if (avatarType_index >= avatarTypeList.Count -1)
                    avatarType_index = -1;

                avatarTypeText.text = avatarTypeList[++avatarType_index];
                //아바타 모델 변경
                break;

            //피부색 case
        }
    }

    public void PreButton(TMP_Text text)
    {
        switch (text.name)
        {
            case "Major":
                if (major_index <= 0)
                    major_index = majorList.Count;

                majorText.text = majorList[--major_index];
                break;
            case "AvatarSex":
                if (avatarSex_index <= 0)
                    avatarSex_index = avatarSexList.Count;

                avatarSexText.text = avatarSexList[--avatarSex_index];
                break;
            case "AvatarType":
                if (avatarType_index <= 0)
                    avatarType_index = avatarTypeList.Count;

                avatarTypeText.text = avatarTypeList[--avatarType_index];
                //아바타 모델 변경
                break;

            //피부색 case
        }
    }
}
