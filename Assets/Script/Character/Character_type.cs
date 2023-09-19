using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_type : MonoBehaviour
{
    [SerializeField] private string nickname;
    [SerializeField] private PlayerType.Major major;
    [SerializeField] private PlayerType.Sex sex;
    [SerializeField] private PlayerType.AvatarType avatarType;
    private Color skinColor;

    public void SetUnitType(string _nickname, PlayerType.Major _major, PlayerType.Sex _sex, PlayerType.AvatarType _avatarType)
    {
        nickname = _nickname;
        major = _major;
        sex = _sex;
        avatarType = _avatarType;
        //ÇÇºÎ»ö
    }
}
