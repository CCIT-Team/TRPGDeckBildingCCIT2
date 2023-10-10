using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class CharacterSlot_UI : MonoBehaviour
{
    List<string> majorList = new List<string>();
    List<string> avatarSexList = new List<string>();
    List<string> avatarTypeList = new List<string>();

    public GameObject model;
    TMP_InputField avatarNickName;
    TMP_Text majorText;
    TMP_Text avatarSexText;
    TMP_Text avatarTypeText;
    TMP_Text skinColorText; //아직 없음

    string avatarNickName_index = null;
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

        avatarNickName.name = "AvatarNickname_Inputbox";
        majorText.name = "Major";
        avatarSexText.name = "AvatarSex";
        avatarTypeText.name = "AvatarType";
        skinColorText.name = "SkinColor";

        majorList = Enum.GetNames(typeof(PlayerType.Major)).ToList();
        avatarSexList = Enum.GetNames(typeof(PlayerType.Sex)).ToList();
        avatarTypeList = Enum.GetNames(typeof(PlayerType.AvatarType)).ToList();
        //피부색 enum -> List

        majorText.text = majorList[major_index];
        avatarSexText.text = avatarSexList[avatarSex_index];
        avatarTypeText.text = avatarTypeList[avatarType_index];
        //피부색 text
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
    public void SetType(int num)
    {
        switch(num)
        {
            case 0:
                GameManager.instance.avatar_0.Add("1");
                GameManager.instance.avatar_0.Add(avatarNickName_index);
                GameManager.instance.avatar_0.Add(majorText.text);
                GameManager.instance.avatar_0.Add(avatarSexText.text);
                GameManager.instance.avatar_0.Add(avatarTypeText.text);
                break;

            case 1:
                GameManager.instance.avatar_1.Add("2");
                GameManager.instance.avatar_1.Add(avatarNickName_index);
                GameManager.instance.avatar_1.Add(majorText.text);
                GameManager.instance.avatar_1.Add(avatarSexText.text);
                GameManager.instance.avatar_1.Add(avatarTypeText.text);
                break;

            case 2:
                GameManager.instance.avatar_2.Add("3");
                GameManager.instance.avatar_2.Add(avatarNickName_index);
                GameManager.instance.avatar_2.Add(majorText.text);
                GameManager.instance.avatar_2.Add(avatarSexText.text);
                GameManager.instance.avatar_2.Add(avatarTypeText.text);
                break;
        }

        if(GameManager.instance.avatar_1.Count == 0 && GameManager.instance.avatar_2.Count != 0)
        {
            GameManager.instance.avatar_1 = GameManager.instance.avatar_2.ToList();
            GameManager.instance.avatar_1[0] = "2";
            GameManager.instance.avatar_2.Clear();
        }
    }

    public void SetNickName()
    {
        avatarNickName_index = avatarNickName.text;
    }
}
