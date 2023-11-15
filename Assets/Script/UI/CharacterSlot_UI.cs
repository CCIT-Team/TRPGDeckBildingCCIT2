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
    List<string> avatarGenderList = new List<string>();
    List<string> avatarTypeList = new List<string>();

    //0~3 휴먼
    //4~6 엘프
    //7~10 드로우
    //11~14 하프오크
    List<float> skinColor = new List<float>();
    //0~12 눈 색
    List<float> eyeColor = new List<float>();

    public LobbyUI_Manager manager;

    public GameObject[] major;
    public GameObject[] gender;
    public GameObject[] type;
    public SkinnedMeshRenderer[] gender_mesh;
    public SkinnedMeshRenderer[] gender_skinColor;
    public SkinnedMeshRenderer customEyeColor;

    TMP_InputField avatarNickName;
    TMP_Text majorText;
    TMP_Text avatarGenderText;
    TMP_Text avatarTypeText;
    TMP_Text skinColorText;
    TMP_Text eyeColorText;

    string avatarNickName_index = null;
    int major_index = 0;
    int avatarGender_index = 0;
    int avatarType_index = 0;
    int skinColor_index = 0;
    int eyeColor_index = 0;
    const float skinColor_BaseOffset = -0.015625f;
    const float eyeColor_BaseOffset = -0.015625f;

    private void Start()
    {
        avatarNickName = transform.GetChild(0).GetComponent<TMP_InputField>();
        majorText = transform.GetChild(1).GetComponent<TMP_Text>();
        avatarGenderText = transform.GetChild(2).GetComponent<TMP_Text>();
        avatarTypeText = transform.GetChild(3).GetComponent<TMP_Text>();
        skinColorText = transform.GetChild(4).GetComponent<TMP_Text>();
        eyeColorText = transform.GetChild(5).GetComponent<TMP_Text>();

        avatarNickName.name = "AvatarNickname_Inputbox";
        majorText.name = "Major";
        avatarGenderText.name = "AvatarGender";
        avatarTypeText.name = "AvatarType";
        skinColorText.name = "SkinColor";
        eyeColorText.name = "EyeColor";

        majorList = Enum.GetNames(typeof(PlayerType.Major)).ToList();
        avatarGenderList = Enum.GetNames(typeof(PlayerType.Gender)).ToList();
        avatarTypeList = Enum.GetNames(typeof(PlayerType.AvatarType)).ToList();
        float skinColor_Offset;
        for(int i = 0; i < 15; i++)
        {
            skinColor_Offset = skinColor_BaseOffset * (i + 1);
            skinColor.Add(skinColor_Offset);
        }

        float eyeColor_Offset;
        for (int i = 0; i < 13; i++)
        {
            eyeColor_Offset = eyeColor_BaseOffset * i;
            eyeColor.Add(eyeColor_Offset);
        }

        majorText.text = majorList[major_index];
        avatarGenderText.text = avatarGenderList[avatarGender_index];
        avatarTypeText.text = avatarTypeList[avatarType_index];
        skinColorText.text = skinColor[skinColor_index].ToString();
        eyeColorText.text = eyeColor[eyeColor_index].ToString();

        SetSkinColor(skinColor_BaseOffset);
    }
    //직업 순서) 파이터 0 - 매지션 1 - 클레릭 2
    //종족 순서) 인간 0 - 엘프 1 - 다크엘프 2 - 하프오크 3
    public void NextButton(TMP_Text text)
    {
        switch(text.name)
        {
            case "Major":
                if (major_index >= majorList.Count -1)
                    major_index = -1;

                majorText.text = majorList[++major_index];
                SwitchingMajor(major_index);
                break;
            case "AvatarGender":
                if (avatarGender_index >= avatarGenderList.Count -1)
                    avatarGender_index = -1;

                avatarGenderText.text = avatarGenderList[++avatarGender_index];
                SwitchingGender(avatarGender_index);
                break;
            case "AvatarType":
                if (avatarType_index >= avatarTypeList.Count -1)
                    avatarType_index = -1;

                avatarTypeText.text = avatarTypeList[++avatarType_index];

                if (avatarType_index == 0)
                    skinColor_index = 0;
                else if(avatarType_index == 1)
                    skinColor_index = 4;
                else if (avatarType_index == 2)
                    skinColor_index = 7;
                else if (avatarType_index == 3)
                    skinColor_index = 11;

                skinColorText.text = skinColor[skinColor_index].ToString();

                SwitchingType(avatarType_index, skinColor_index);
                break;
            case "SkinColor":
                if(avatarType_index == 0)
                {
                    if (skinColor_index >= 3)
                        skinColor_index = -1;
                }
                else if (avatarType_index == 1)
                {
                    if (skinColor_index >= 6)
                        skinColor_index = 3;
                }
                else if (avatarType_index == 2)
                {
                    if (skinColor_index >= 10)
                        skinColor_index = 6;
                }
                else if (avatarType_index == 3)
                {
                    if (skinColor_index >= 14)
                        skinColor_index =10;
                }

                skinColorText.text = skinColor[++skinColor_index].ToString();
                SetSkinColor(skinColor[skinColor_index]);
                break;
            case "EyeColor":
                if (eyeColor_index >= eyeColor.Count - 1)
                    eyeColor_index = -1;

                eyeColorText.text = eyeColor[++eyeColor_index].ToString();
                SetEyeColor(eyeColor[eyeColor_index]);
                break;
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
                SwitchingMajor(major_index);
                break;
            case "AvatarGender":
                if (avatarGender_index <= 0)
                    avatarGender_index = avatarGenderList.Count;

                avatarGenderText.text = avatarGenderList[--avatarGender_index];
                SwitchingGender(avatarGender_index);
                break;
            case "AvatarType":
                if (avatarType_index <= 0)
                    avatarType_index = avatarTypeList.Count;

                avatarTypeText.text = avatarTypeList[--avatarType_index];

                if (avatarType_index == 0)
                    skinColor_index = 0;
                else if (avatarType_index == 1)
                    skinColor_index = 4;
                else if (avatarType_index == 2)
                    skinColor_index = 7;
                else if (avatarType_index == 3)
                    skinColor_index = 11;

                skinColorText.text = skinColor[skinColor_index].ToString();

                SwitchingType(avatarType_index, skinColor_index);
                break;
            case "SkinColor":
                if (avatarType_index == 0)
                {
                    if (skinColor_index <= 0)
                        skinColor_index = 4;
                }
                else if (avatarType_index == 1)
                {
                    if (skinColor_index <= 4)
                        skinColor_index = 7;
                }
                else if (avatarType_index == 2)
                {
                    if (skinColor_index <= 7)
                        skinColor_index = 11;
                }
                else if (avatarType_index == 3)
                {
                    if (skinColor_index <= 11)
                        skinColor_index = 15;
                }

                skinColorText.text = skinColor[--skinColor_index].ToString();
                SetSkinColor(skinColor[skinColor_index]);
                break;
            case "EyeColor":
                if (eyeColor_index <= 0)
                    eyeColor_index = eyeColor.Count;

                eyeColorText.text = eyeColor[--eyeColor_index].ToString();
                SetEyeColor(eyeColor[eyeColor_index]);
                break;
        }
    }

    private void SwitchingMajor(int index)
    {
        switch (index)
        {
            case 0:
                major[0].SetActive(false);
                major[1].SetActive(false);
                break;
            case 1:
                major[0].SetActive(true);
                major[1].SetActive(false);
                break;
            case 2:
                major[0].SetActive(false);
                major[1].SetActive(true);
                break;
        }
    }
    
    private void SwitchingGender(int index)
    {
        switch (index)
        {
            case 0:
                gender[0].SetActive(true);
                gender[1].SetActive(false);

                for(int i = 0; i < gender_mesh.Length; i++)
                {
                    gender_mesh[i].SetBlendShapeWeight(0, 0);
                }

                break;
            case 1:
                gender[0].SetActive(false);
                gender[1].SetActive(true);
                for (int i = 0; i < gender_mesh.Length; i++)
                {
                    gender_mesh[i].SetBlendShapeWeight(0, 100);
                }
                break;
        }
    }
    //종족 순서) 인간 0 - 엘프 1 - 다크엘프 2 - 하프오크 3
    private void SwitchingType(int typeIndex, int colorIndex)
    {
        switch (typeIndex)
        {
            case 0:
                type[0].SetActive(true);
                type[1].SetActive(false);
                type[2].SetActive(false);
                break;
            case 1:
                type[0].SetActive(false);
                type[1].SetActive(true);
                type[2].SetActive(false);
                break;
            case 2:
                type[0].SetActive(false);
                type[1].SetActive(true);
                type[2].SetActive(false);
                break;
            case 3:
                type[0].SetActive(false);
                type[1].SetActive(false);
                type[2].SetActive(true);
                break;
        }
        SetSkinColor(skinColor[colorIndex]);
    }

    private void SetSkinColor(float offset)
    {
        for (int i = 0; i < gender_skinColor.Length; i++)
        {
            gender_skinColor[i].materials[0].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        }
    }

    private void SetEyeColor(float offset)
    {
        customEyeColor.materials[2].SetTextureOffset("_BaseMap", new Vector2(0, offset));
    }

    public void SetType(int num)
    {
        switch(num)
        {
            case 0:
                manager.avatar_0.Add("1");
                manager.avatar_0.Add(avatarNickName_index);
                manager.avatar_0.Add(majorText.text);
                manager.avatar_0.Add(avatarGenderText.text);
                manager.avatar_0.Add(avatarTypeText.text);
                manager.avatar_0.Add(skinColor[skinColor_index].ToString());
                manager.avatar_0.Add(eyeColor[eyeColor_index].ToString());
                break;

            case 1:
                manager.avatar_1.Add("2");
                manager.avatar_1.Add(avatarNickName_index);
                manager.avatar_1.Add(majorText.text);
                manager.avatar_1.Add(avatarGenderText.text);
                manager.avatar_1.Add(avatarTypeText.text);
                manager.avatar_1.Add(skinColor[skinColor_index].ToString());
                manager.avatar_1.Add(eyeColor[eyeColor_index].ToString());
                break;

            case 2:
                manager.avatar_2.Add("3");
                manager.avatar_2.Add(avatarNickName_index);
                manager.avatar_2.Add(majorText.text);
                manager.avatar_2.Add(avatarGenderText.text);
                manager.avatar_2.Add(avatarTypeText.text);
                manager.avatar_2.Add(skinColor[skinColor_index].ToString());
                manager.avatar_2.Add(eyeColor[eyeColor_index].ToString());
                break;
        }
        if (manager.avatar_1.Count == 0 && manager.avatar_2.Count != 0)
        {
            manager.avatar_1 = manager.avatar_2.ToList();
            manager.avatar_1[0] = "2";
            manager.avatar_2.Clear();
        }
    }

    public void SetNickName()
    {
        avatarNickName_index = avatarNickName.text;
    }
}
