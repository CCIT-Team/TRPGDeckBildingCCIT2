using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_type : MonoBehaviour
{
    public int playerNum;
    public string nickname;
    public PlayerType.Major major;
    [SerializeField] private PlayerType.Gender gender;
    [SerializeField] private PlayerType.AvatarType avatarType;
    private float skinColor;
    private float eyeColor;
    private GameObject weapon;

    public Transform[] weaponPosition;
    public GameObject[] majorObject;
    public GameObject[] genderObject;
    public GameObject[] typeObject;
    public SkinnedMeshRenderer[] gender_mesh;
    public SkinnedMeshRenderer[] gender_skinColor;
    public SkinnedMeshRenderer customEyeColor;
    public SkinnedMeshRenderer[] customHairColor;

    private float positionx;
    private float positiony;
    private float positionz;

    public Vector3 pos;

    private string insertQuery;

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

    public void SetUnitPosition(PlayerPosition position)
    {
        playerNum = position.playerNum;
        positionx = position.positionX;
        positiony = position.positionY;
        positionz = position.positionZ;

        pos.Set(positionx, positiony, positionz);
    }

    private void SwitchingMajor(PlayerType.Major major)
    {
        switch (major)
        {
            case PlayerType.Major.Fighter:
                majorObject[0].SetActive(true);
                majorObject[1].SetActive(false);
                majorObject[2].SetActive(false);
                EquipWeapon(1, "TwohandSword_claymore");
                break;
            case PlayerType.Major.Wizard:
                majorObject[0].SetActive(false);
                majorObject[1].SetActive(true);
                majorObject[2].SetActive(false);
                EquipWeapon(0, "Staff_scholarship");
                break;
            case PlayerType.Major.Cleric:
                majorObject[0].SetActive(false);
                majorObject[1].SetActive(false);
                majorObject[2].SetActive(true);
                EquipWeapon(0, "Sheild_steel");
                EquipWeapon(1, "Mace_mace");
                break;
        }
    }

    private void EquipWeapon(int posHand, string weaponName)
    {
        weapon = Instantiate(Resources.Load("Prefabs/Character/Weapon/" + weaponName, typeof(GameObject))) as GameObject;
        weapon.transform.SetParent(weaponPosition[posHand]);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
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
        for(int i = 0; i < customHairColor.Length; i++)
        {
            customHairColor[i].materials[0].SetTextureOffset("_BaseMap", new Vector2(0, offset));
        }
    }

    public string GetTypeDBQuery()
    {
        insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type, skinColor, eyeColor) VALUES ({playerNum}, '{nickname}', '{major.ToString()}', '{gender.ToString()}', '{avatarType.ToString()}', '{skinColor.ToString()}', '{eyeColor.ToString()}')";
        return insertQuery;
    }

    public string GetPositionDBQuery()
    {
        insertQuery = $"INSERT INTO Position (playerNum, positionX, PositionY, positionZ) VALUES ({playerNum}, '{transform.position.x.ToString()}', '{transform.position.y.ToString()}', '{transform.position.z.ToString()}')";
        return insertQuery;
    }

    public string GetWorldPositionDBQuery()
    {
        insertQuery = $"INSERT INTO Position (playerNum, positionX, PositionY, positionZ) VALUES ({playerNum}, '{pos.x.ToString()}', '{pos.y.ToString()}', '{pos.z.ToString()}')";
        return insertQuery;
    }
}
