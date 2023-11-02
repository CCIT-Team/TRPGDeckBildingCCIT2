using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_type : MonoBehaviour
{
    [SerializeField] public int playerNum;
    [SerializeField] public string nickname;
    [SerializeField] public PlayerType.Major major;
    [SerializeField] private PlayerType.Sex sex;
    [SerializeField] private PlayerType.AvatarType avatarType;
    private Color skinColor;

    private string insertQuery;

    public void SetUnitType(int _playerNum, string _nickname, PlayerType.Major _major, PlayerType.Sex _sex, PlayerType.AvatarType _avatarType)
    {
        playerNum = _playerNum;
        nickname = _nickname;
        major = _major;
        sex = _sex;
        avatarType = _avatarType;
        //피부색
    }
    public void SetUnitType(PlayerType type)
    {
        playerNum = type.playerNum;
        nickname = type.nickname;
        major = type.major;
        sex = type.sex;
        avatarType = type.type;
        //피부색
    }


    public string GetTypeDBQuery()
    {
        insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type) VALUES ({playerNum}, '{nickname}', '{major.ToString()}', '{sex.ToString()}', '{avatarType.ToString()}')";
        return insertQuery;
    }
}
