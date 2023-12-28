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

    private readonly string[] majorK = new string[] { "전사", "마법사", "성직자" };
    private readonly string[] genderK = new string[] { "남", "여" };
    private readonly string[] typeK = new string[] { "휴먼", "엘프", "다크엘프", "하프오크" };

    public LobbyUI_Manager manager;
    public Animator animationModel;

    public GameObject[] weapon;
    public SkinnedMeshRenderer[] genderAndType;
    public GameObject[] major;
    //public SkinnedMeshRenderer[] gender_skinColor;
    //public SkinnedMeshRenderer[] _gender_skinColor;
    //public SkinnedMeshRenderer customEyeColor;
    //public SkinnedMeshRenderer[] customHairColor;

    TMP_InputField avatarNickName;
    TMP_Text majorText;
    TMP_Text avatarGenderText;
    TMP_Text avatarTypeText;
    Image skinColorImage;
    Image hairColorImage;
    Image eyeColorImage;

    string avatarNickName_index = null;
    int major_index = 0;
    int avatarGender_index = 0;
    int avatarType_index = 0;
    int skinColor_index = 0;
    int eyeColor_index = 0;
    const float skinColor_BaseOffset = -0.015625f;
    const float eyeColor_BaseOffset = -0.015625f;
    const float hiarColor_BaseOffset = -0.015625f;

    private void Start()
    {
        avatarNickName = transform.GetChild(0).GetComponent<TMP_InputField>();
        majorText = transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>();
        avatarGenderText = transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>();
        avatarTypeText = transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>();
        skinColorImage = transform.GetChild(4).transform.GetChild(0).GetComponent<Image>();
        //머리색 컴포넌트 참조
        hairColorImage = transform.GetChild(5).transform.GetChild(0).GetComponent<Image>();
        eyeColorImage = transform.GetChild(5).transform.GetChild(1).GetComponent<Image>();

        avatarNickName.name = "AvatarNickname_Inputbox";
        majorText.name = "Major_Text";
        avatarGenderText.name = "AvatarGender_Text";
        avatarTypeText.name = "AvatarType_Text";
        skinColorImage.transform.name = "SkinColor";
        eyeColorImage.transform.parent.name = "EyeHairColor"; //머리색 눈색 둘중 하나만 해도댐

        majorList = Enum.GetNames(typeof(PlayerType.Major)).ToList();
        avatarGenderList = Enum.GetNames(typeof(PlayerType.Gender)).ToList();
        avatarTypeList = Enum.GetNames(typeof(PlayerType.AvatarType)).ToList();
        float skinColor_Offset;
        for(int i = 0; i < 15; i++)
        {
            skinColor_Offset = skinColor_BaseOffset * i;
            skinColor.Add(skinColor_Offset);
        }

        float eyeColor_Offset;
        for (int i = 0; i < 13; i++)
        {
            eyeColor_Offset = eyeColor_BaseOffset * i;
            eyeColor.Add(eyeColor_Offset);
        }

        majorText.text = majorK[major_index];
        avatarGenderText.text = genderK[avatarGender_index];
        avatarTypeText.text = typeK[avatarType_index];
        skinColorImage.color = SwitchSkinUIColor(skinColor_index);
        eyeColorImage.color = SwitchEyeHairUIColor(eyeColor_index)[0];
        hairColorImage.color = SwitchEyeHairUIColor(eyeColor_index)[1];
        SetSkinColor(0);
        SetEyeColor(0);
    }
    //직업 순서) 파이터 0 - 매지션 1 - 클레릭 2
    //종족 순서) 인간 0 - 엘프 1 - 다크엘프 2 - 하프오크 3
    public void NextButton(GameObject text)
    {
        SoundManager.instance.PlayUICilckSound();
        switch (text.name)
        {
            case "Major_Text":
                if (major_index >= majorList.Count -1)
                    major_index = -1;

                majorText.text = majorK[++major_index];
                SwitchingMajor(major_index);
                break;
            case "AvatarGender_Text":
                if (avatarGender_index >= avatarGenderList.Count -1)
                    avatarGender_index = -1;

                avatarGenderText.text = genderK[++avatarGender_index];
                SwitchingGender(avatarGender_index);
                break;
            case "AvatarType_Text":
                if (avatarType_index >= avatarTypeList.Count -1)
                    avatarType_index = -1;

                avatarTypeText.text = typeK[++avatarType_index];

                if (avatarType_index == 0)
                    skinColor_index = 0;
                else if(avatarType_index == 1)
                    skinColor_index = 4;
                else if (avatarType_index == 2)
                    skinColor_index = 7;
                else if (avatarType_index == 3)
                    skinColor_index = 11;

                //skinColorImage.color = new Color(ColorPalette.skinColors[skinColor_index].x, ColorPalette.skinColors[skinColor_index].y, ColorPalette.skinColors[skinColor_index].z);
                skinColorImage.color = SwitchSkinUIColor(skinColor_index);
                //skinColorText.text = skinColor[skinColor_index].ToString();

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

                //skinColorText.text = skinColor[++skinColor_index].ToString();
                skinColorImage.color = SwitchSkinUIColor(++skinColor_index);
                SetSkinColor(skinColor[skinColor_index]);
                break;
            case "EyeHairColor":
                if (eyeColor_index >= eyeColor.Count - 1)
                    eyeColor_index = -1;

                //eyeColorText.text = eyeColor[++eyeColor_index].ToString();
                eyeColorImage.color = SwitchEyeHairUIColor(++eyeColor_index)[0];
                hairColorImage.color = SwitchEyeHairUIColor(eyeColor_index)[1];
                SetEyeColor(eyeColor[eyeColor_index]);
                //머리색바꾸기
                break;
        }
    }

    public void PreButton(GameObject text)
    {
        SoundManager.instance.PlayUICilckSound();
        switch (text.name)
        {
            case "Major_Text":
                if (major_index <= 0)
                    major_index = majorList.Count;

                majorText.text = majorK[--major_index];
                SwitchingMajor(major_index);
                break;
            case "AvatarGender_Text":
                if (avatarGender_index <= 0)
                    avatarGender_index = avatarGenderList.Count;

                avatarGenderText.text = genderK[--avatarGender_index];
                SwitchingGender(avatarGender_index);
                break;
            case "AvatarType_Text":
                if (avatarType_index <= 0)
                    avatarType_index = avatarTypeList.Count;

                avatarTypeText.text = typeK[--avatarType_index];

                if (avatarType_index == 0)
                    skinColor_index = 0;
                else if (avatarType_index == 1)
                    skinColor_index = 4;
                else if (avatarType_index == 2)
                    skinColor_index = 7;
                else if (avatarType_index == 3)
                    skinColor_index = 11;

                //skinColorText.text = skinColor[skinColor_index].ToString();
                skinColorImage.color = SwitchSkinUIColor(skinColor_index);
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

                //skinColorText.text = skinColor[--skinColor_index].ToString();
                skinColorImage.color = SwitchSkinUIColor(--skinColor_index);
                SetSkinColor(skinColor[skinColor_index]);
                break;
            case "EyeHairColor":
                if (eyeColor_index <= 0)
                    eyeColor_index = eyeColor.Count;

                //eyeColorText.text = eyeColor[--eyeColor_index].ToString();
                eyeColorImage.color = SwitchEyeHairUIColor(--eyeColor_index)[0];
                hairColorImage.color = SwitchEyeHairUIColor(eyeColor_index)[1];
                SetEyeColor(eyeColor[eyeColor_index]);
                //머리색 바꾸기
                break;
        }
    }

    private Color SwitchSkinUIColor(int index)
    {
        Color pattle;
        pattle = new Color(ColorPalette.skinColors[index].x / 255f, ColorPalette.skinColors[index].y / 255f, ColorPalette.skinColors[index].z / 255f);

        return pattle;
    }

    private Color[] SwitchEyeHairUIColor(int index)
    {
        Color[] pattle = new Color[2];
        pattle[0] = new Color(ColorPalette.eyeColors[index].x / 255f, ColorPalette.eyeColors[index].y / 255f, ColorPalette.eyeColors[index].z / 255f);
        pattle[1] = new Color(ColorPalette.hairColors[index].x / 255f, ColorPalette.hairColors[index].y / 255f, ColorPalette.hairColors[index].z / 255f);

        return pattle;
    }

    private void SwitchingMajor(int index)
    {
        switch (index)
        {
            case 0:
                major[0].SetActive(true);
                major[1].SetActive(false);
                major[2].SetActive(false);
                //애니메이션 연출
                animationModel.SetBool("isKinght", true);
                animationModel.SetBool("isWizard", false);
                animationModel.SetBool("iscleric", false);
                SwitchingWeapon(index);
                break;
            case 1:
                major[0].SetActive(false);
                major[1].SetActive(true);
                major[2].SetActive(false);
                animationModel.SetBool("isKinght", false);
                animationModel.SetBool("isWizard", true);
                animationModel.SetBool("iscleric", false);
                SwitchingWeapon(index);
                break;
            case 2:
                major[0].SetActive(false);
                major[1].SetActive(false);
                major[2].SetActive(true);
                animationModel.SetBool("isKinght", false);
                animationModel.SetBool("isWizard", false);
                animationModel.SetBool("iscleric", true);
                SwitchingWeapon(index);
                break;
        }
    }

    private void SwitchingWeapon(int index)
    {
        switch (index)
        {
            case 0:
                weapon[0].SetActive(true);
                weapon[1].SetActive(false);
                weapon[2].SetActive(false);
                weapon[3].SetActive(false);
                break;
            case 1:
                weapon[0].SetActive(false);
                weapon[1].SetActive(true);
                weapon[2].SetActive(false);
                weapon[3].SetActive(false);
                break;
            case 2:
                weapon[0].SetActive(false);
                weapon[1].SetActive(false);
                weapon[2].SetActive(true);
                weapon[3].SetActive(true);
                break;
        }
    }
    
    private void SwitchingGender(int index)
    {
        switch (index)
        {
            case 0:
                for (int i = 0; i < genderAndType.Length; i++)
                {
                    genderAndType[i].SetBlendShapeWeight(0, 0);
                }
                break;
            case 1:
                for (int i = 0; i < genderAndType.Length; i++)
                {
                    genderAndType[i].SetBlendShapeWeight(0, 100);
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
                genderAndType[0].SetBlendShapeWeight(1, 0);
                genderAndType[0].SetBlendShapeWeight(2, 0);

                break;
            case 1:
                genderAndType[0].SetBlendShapeWeight(1, 100);
                genderAndType[0].SetBlendShapeWeight(2, 0);
                break;
            case 2:
                genderAndType[0].SetBlendShapeWeight(1, 100);
                genderAndType[0].SetBlendShapeWeight(2, 0);
                break;
            case 3:
                genderAndType[0].SetBlendShapeWeight(1, 0);
                genderAndType[0].SetBlendShapeWeight(2, 100);
                break;
        }
        SetSkinColor(skinColor[colorIndex]);
    }

    private void SetSkinColor(float offset)
    {
        genderAndType[0].materials[1].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        //for (int i = 0; i < gender_skinColor.Length; i++)
        //{
        //    gender_skinColor[i].materials[0].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        //}
    }

    private void SetEyeColor(float offset)
    {
        genderAndType[0].materials[2].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        //customEyeColor.materials[2].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        //for(int i = 0; i < customHairColor.Length; i++)
        //{
        //    customHairColor[i].materials[0].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        //}
    }

    public void SetType(int num)
    {
        switch(num)
        {
            case 0:
                manager.avatar_0.Add("1");
                manager.avatar_0.Add(avatarNickName_index);
                //manager.avatar_0.Add(majorText.text);
                manager.avatar_0.Add(majorList[major_index]);
                //manager.avatar_0.Add(avatarGenderText.text);
                manager.avatar_0.Add(avatarGenderList[avatarGender_index]);
                //manager.avatar_0.Add(avatarTypeText.text);
                manager.avatar_0.Add(avatarTypeList[avatarType_index]);
                manager.avatar_0.Add(skinColor[skinColor_index].ToString());
                manager.avatar_0.Add(eyeColor[eyeColor_index].ToString());
                break;

            case 1:
                manager.avatar_1.Add("2");
                manager.avatar_1.Add(avatarNickName_index);
                //manager.avatar_1.Add(majorText.text);
                manager.avatar_1.Add(majorList[major_index]);
                //manager.avatar_1.Add(avatarGenderText.text);
                manager.avatar_1.Add(avatarGenderList[avatarGender_index]);
                //manager.avatar_1.Add(avatarTypeText.text);
                manager.avatar_1.Add(avatarTypeList[avatarType_index]);
                manager.avatar_1.Add(skinColor[skinColor_index].ToString());
                manager.avatar_1.Add(eyeColor[eyeColor_index].ToString());
                break;

            case 2:
                manager.avatar_2.Add("3");
                manager.avatar_2.Add(avatarNickName_index);
                //manager.avatar_0.Add(majorText.text);
                manager.avatar_2.Add(majorList[major_index]);
                //manager.avatar_2.Add(avatarGenderText.text);
                manager.avatar_2.Add(avatarGenderList[avatarGender_index]);
                //manager.avatar_2.Add(avatarTypeText.text);
                manager.avatar_2.Add(avatarTypeList[avatarType_index]);
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


public static class ColorPalette
{
    public static Vector3[] skinColors = new[] 
    {
        new Vector3(254, 221, 214),
        new Vector3(247, 221, 202),
        new Vector3(235, 187, 151),
        new Vector3(121, 78, 69),
        new Vector3(254, 238, 231),
        new Vector3(252, 235, 220),
        new Vector3(254, 233, 231),
        new Vector3(183, 173, 225),
        new Vector3(139, 144, 194),
        new Vector3(165, 125, 180),
        new Vector3(101, 91, 96),
        new Vector3(164, 199, 125),
        new Vector3(199, 135, 125),
        new Vector3(199, 168, 125),
        new Vector3(120, 145, 74),
    };

    public static Vector3[] eyeColors = new[]
    {
        new Vector3(21, 9, 2),
        new Vector3(79, 39, 17),
        new Vector3(145, 98, 71),
        new Vector3(171, 127, 76),
        new Vector3(134, 144, 32),
        new Vector3(88, 169, 44),
        new Vector3(149, 193, 102),
        new Vector3(66, 179, 151),
        new Vector3(126, 193, 203),
        new Vector3(93, 125, 212),
        new Vector3(169, 172, 180),
        new Vector3(240, 125, 125),
        new Vector3(44, 44, 46)
    };

    public static Vector3[] hairColors = new[]
    {
        new Vector3(42, 42, 42),
        new Vector3(103, 70, 47),
        new Vector3(154, 115, 56),
        new Vector3(234, 184, 109),
        new Vector3(199, 90, 60),
        new Vector3(234, 184, 109),
        new Vector3(183, 174, 161),
        new Vector3(247, 202, 134),
        new Vector3(115, 83, 55),
        new Vector3(58, 54, 50),
        new Vector3(178, 177, 175),
        new Vector3(192, 188, 184),
        new Vector3(192, 188, 184)
    };
}