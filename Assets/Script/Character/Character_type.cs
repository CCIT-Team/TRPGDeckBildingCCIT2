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
    private Color skinColor;

    private string insertQuery;

    public void SetUnitType(int _playerNum, string _nickname, PlayerType.Major _major, PlayerType.Gender _gender, PlayerType.AvatarType _avatarType)
    {
        playerNum = _playerNum;
        nickname = _nickname;
        major = _major;
        gender = _gender;
        avatarType = _avatarType;
        //피부색
    }
    public void SetUnitType(PlayerType type)
    {
        playerNum = type.playerNum;
        nickname = type.nickname;
        major = type.major;
        gender = type.gender;
        avatarType = type.type;
        //피부색
    }

    public string GetTypeDBQuery()
    {
        insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({playerNum}, '{nickname}', '{major.ToString()}', '{gender.ToString()}', '{avatarType.ToString()}')";
        return insertQuery;
    }
}
