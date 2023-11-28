using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTexturePlayer : MonoBehaviour
{
    public int playerNum;
    public string nickname;
    public PlayerType.Major major;
    [SerializeField] private PlayerType.Gender gender;
    [SerializeField] private PlayerType.AvatarType avatarType;
    private float skinColor;
    private float eyeColor;

    public GameObject[] majorObject;
    public GameObject[] genderObject;
    public GameObject[] typeObject;
    public SkinnedMeshRenderer[] gender_mesh;
    public SkinnedMeshRenderer[] gender_skinColor;
    public SkinnedMeshRenderer customEyeColor;
    public SkinnedMeshRenderer[] customHairColor;



    public void SetUnitType(PlayerType type)
    {
        playerNum = type.playerNum;
        nickname = type.nickname;
        major = type.major;
        gender = type.gender;
        avatarType = type.type;
        skinColor = type.skinColor;
        eyeColor = type.eyeColor;

        SwitchingMajor(major);
        SwitchingGender(gender);
        SwitchingType(avatarType);
        SetSkinColor(skinColor);
        SetEyeColor(eyeColor);
    }

    public void SetUnitPosition(Vector3 position, Vector3 rotation)
    {
        gameObject.transform.localPosition = position;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    private void SwitchingMajor(PlayerType.Major major)
    {
        switch (major)
        {
            case PlayerType.Major.Fighter:
                majorObject[0].SetActive(true);
                majorObject[1].SetActive(false);
                majorObject[2].SetActive(false);
                break;
            case PlayerType.Major.Wizard:
                majorObject[0].SetActive(false);
                majorObject[1].SetActive(true);
                majorObject[2].SetActive(false);
                break;
            case PlayerType.Major.Cleric:
                majorObject[0].SetActive(false);
                majorObject[1].SetActive(false);
                majorObject[2].SetActive(true);
                break;
        }
    }

    private void SwitchingGender(PlayerType.Gender gender)
    {
        switch (gender)
        {
            case PlayerType.Gender.Male:
                genderObject[0].SetActive(true);
                genderObject[1].SetActive(false);

                for (int i = 0; i < gender_mesh.Length; i++)
                {
                    gender_mesh[i].SetBlendShapeWeight(0, 0);
                }

                break;
            case PlayerType.Gender.Female:
                genderObject[0].SetActive(false);
                genderObject[1].SetActive(true);
                for (int i = 0; i < gender_mesh.Length; i++)
                {
                    gender_mesh[i].SetBlendShapeWeight(0, 100);
                }
                break;
        }
    }
    private void SwitchingType(PlayerType.AvatarType type)
    {
        switch (type)
        {
            case PlayerType.AvatarType.Human:
                typeObject[0].SetActive(true);
                typeObject[1].SetActive(false);
                typeObject[2].SetActive(false);
                break;
            case PlayerType.AvatarType.Elf:
                typeObject[0].SetActive(false);
                typeObject[1].SetActive(true);
                typeObject[2].SetActive(false);
                break;
            case PlayerType.AvatarType.DarkElf:
                typeObject[0].SetActive(false);
                typeObject[1].SetActive(true);
                typeObject[2].SetActive(false);
                break;
            case PlayerType.AvatarType.HalfOrc:
                typeObject[0].SetActive(false);
                typeObject[1].SetActive(false);
                typeObject[2].SetActive(true);
                break;
        }
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
        for (int i = 0; i < customHairColor.Length; i++)
        {
            customHairColor[i].materials[0].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        }
    }
}
