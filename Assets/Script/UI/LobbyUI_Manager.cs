using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUI_Manager : MonoBehaviour
{
    public GameObject panel;

    public List<List<GameObject>> avatars_slot = new List<List<GameObject>>();   
    public List<GameObject> avatar2 = new List<GameObject>();   
    public List<GameObject> avatar3 = new List<GameObject>();

    public CharacterSlot_UI[] avatarSet;
    public TMP_InputField[] avatar_InputNames;

    public List<string> avatar_0 = new List<string>();
    public List<string> avatar_1 = new List<string>();
    public List<string> avatar_2 = new List<string>();

    private int avatarCounter = 0;
    private float initPos = 0.0f;

    private void Start()
    {
        avatars_slot.Add(avatar2);
        avatars_slot.Add(avatar3);
    }

    #region 버튼 함수
    public void Avatar_Delete_button(int num)
    {
        SoundManager.instance.PlayUICilckSound();
        avatars_slot[num][0].gameObject.SetActive(false);
        avatars_slot[num][1].gameObject.SetActive(false);
        avatars_slot[num][2].gameObject.SetActive(false);
        avatars_slot[num][3].gameObject.SetActive(true);
    }

    public void Avatar_Create_button(int num)
    {
        SoundManager.instance.PlayUICilckSound();
        avatars_slot[num][0].gameObject.SetActive(true);
        avatars_slot[num][1].gameObject.SetActive(true);
        avatars_slot[num][2].gameObject.SetActive(true);
        avatars_slot[num][3].gameObject.SetActive(false);
    }

    public void PlayButton(string sceneName)
    {
        SoundManager.instance.PlayUICilckSound();
        if (Input_Exception())
        {
            AvatarSetting();
            GetLobbyAvatar();

            GameManager.instance.LoadScenceName(sceneName);
            panel.SetActive(false);
        }
    }

    public void BackToTitleButton(string sceneName)
    {
        SoundManager.instance.PlayUICilckSound();
        GameManager.instance.LoadScenceName(sceneName);
    }

    #endregion

    private bool Input_Exception() //닉네임 빈칸 예외처리
    {
        for (int i = 0; i < avatar_InputNames.Length; i++)
        {
            if(avatar_InputNames[i].gameObject.transform.parent.gameObject.activeSelf)
            {
                if(string.IsNullOrEmpty(avatar_InputNames[i].text))
                {
                    return false;
                }
            }
        }
        return true;
    }

    #region 아바타 데이터 세팅

    private void AvatarSetting()
    {
        for(int i = 0; i < avatarSet.Length; i++)
        {
            if(avatarSet[i].gameObject.activeSelf)
            {
                avatarSet[i].SetType(i);
                avatarCounter++;
            }
        }
    }

    public void GetLobbyAvatar()
    {
        for (int i = 0; i < avatarCounter; i++)
        {
            CreateToLobbyAvatar(i);
        }
        DataBase.instance.LoadData();
    }

    private void CreateToLobbyAvatar(int index)
    {
        string insertQuery = null;
        switch (index)
        {
            case 0:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type, skinColor, eyeColor) VALUES ({avatar_0[0]}, '{avatar_0[1]}', '{avatar_0[2]}', '{avatar_0[3]}', '{avatar_0[4]}', '{avatar_0[5]}', '{avatar_0[6]}')";
                DataBase.instance.SaveDB(insertQuery);
                insertQuery = $"INSERT INTO Position (playerNum, positionX, positionY, positionZ) VALUES ({avatar_0[0]}, '{initPos.ToString()}', '{initPos.ToString()}', '{initPos.ToString()}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_0[0], avatar_0[2]));
                //DataBase.instance.SaveDB(AvatarCardSetting(avatar_0[0], avatar_0[2]));
                DataBase.instance.SaveDB(NewAvatarCardSetting(avatar_0[0], avatar_0[2]));
                break;
            case 1:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type, skinColor, eyeColor) VALUES ({avatar_1[0]}, '{avatar_1[1]}', '{avatar_1[2]}', '{avatar_1[3]}', '{avatar_1[4]}', '{avatar_1[5]}', '{avatar_1[6]}')";
                DataBase.instance.SaveDB(insertQuery);
                insertQuery = $"INSERT INTO Position (playerNum, positionX, positionY, positionZ) VALUES ({avatar_1[0]}, '{initPos.ToString()}', '{initPos.ToString()}', '{initPos.ToString()}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_1[0], avatar_1[2]));
                //DataBase.instance.SaveDB(AvatarCardSetting(avatar_1[0], avatar_1[2]));
                DataBase.instance.SaveDB(NewAvatarCardSetting(avatar_1[0], avatar_1[2]));
                break;
            case 2:
                insertQuery = $"INSERT INTO Type (playerNum, nickname, major, sex, type, skinColor, eyeColor) VALUES ({avatar_2[0]}, '{avatar_2[1]}', '{avatar_2[2]}', '{avatar_2[3]}', '{avatar_2[4]}', '{avatar_2[5]}', '{avatar_2[6]}')";
                DataBase.instance.SaveDB(insertQuery);
                insertQuery = $"INSERT INTO Position (playerNum, positionX, positionY, positionZ) VALUES ({avatar_2[0]}, '{initPos.ToString()}', '{initPos.ToString()}', '{initPos.ToString()}')";
                DataBase.instance.SaveDB(insertQuery);
                DataBase.instance.SaveDB(AvatarStatSetting(avatar_2[0], avatar_2[2]));
               //DataBase.instance.SaveDB(AvatarCardSetting(avatar_2[0], avatar_2[2]));
               DataBase.instance.SaveDB(NewAvatarCardSetting(avatar_2[0], avatar_2[2]));
                break;
        }
    }

    private string AvatarStatSetting(string playerNum, string major)
    {
        string insertQuery;
        for (int i = 0; i < DataBase.instance.defaultData.Count; i++)
        {
            if (major == DataBase.instance.defaultData[i].major.ToString())
            {
                return insertQuery = $"INSERT INTO Stat (playerNum, strength, intelligence, luck, speed, currentHp, hp, cost, level, exp, maxExp, gold, portionRegular, portionLarge, turn) VALUES " +
             $"({playerNum}, {DataBase.instance.defaultData[i].strength}, {DataBase.instance.defaultData[i].intelligence}, {DataBase.instance.defaultData[i].luck}, {DataBase.instance.defaultData[i].speed}, {DataBase.instance.defaultData[i].hp}, {DataBase.instance.defaultData[i].hp}, {DataBase.instance.defaultData[i].cost}, {DataBase.instance.defaultData[i].level}, {DataBase.instance.defaultData[i].exp}, {DataBase.instance.defaultData[i].maxExp}, {DataBase.instance.defaultData[i].gold}, {0}, {0}, {0})";

            }
        }
        return null;
    }

    private string NewAvatarCardSetting(string playerNum, string major)
    {
        int[] card = new int[40];
        for (int i = 0; i < 40; i++)
        {
            card[i] = 0;
        }
        
        switch(major)
        {
            case "Fighter":
                for(int i = 0; i < DataBase.instance.fighterCardData.Count; i++)
                {
                    card[i] = DataBase.instance.fighterCardData[i].no;
                }
                break;
            case "Wizard":
                for (int i = 0; i < DataBase.instance.wizardCardData.Count; i++)
                {
                    card[i] = DataBase.instance.wizardCardData[i].no;
                }
                break;
            case "Cleric":
                for (int i = 0; i < DataBase.instance.clericCardData.Count; i++)
                {
                    card[i] = DataBase.instance.clericCardData[i].no;
                }
                break;
        }

        string query = "INSERT INTO Deck (playerNum";
        for (int j = 1; j < 41; j++)
        {
            query += ", no" + j.ToString();
        }
        query += ") VALUES (" + playerNum;

        for (int j = 0; j < 40; j++)
        {
            query += ", " + card[j];
        }
        query += ")";
        return query;
    }

    private string AvatarCardSetting(string playerNum, string major)
    {
        int[] card = new int[40];
        for (int i = 0; i < 40; i++)
        {
            card[i] = 0;
        }
        int cardCount = 0;
        for (int i = 0; i < DataBase.instance.defaultData.Count; i++)
        {
            if (major == DataBase.instance.defaultData[i].major.ToString())
            {
                cardCount = DataBase.instance.defaultData[i].card1Count;
                for (int j = 0; j < cardCount; j++)
                {
                    card[j] = DataBase.instance.defaultData[i].card1;
                }
                for (int j = cardCount; j < cardCount + DataBase.instance.defaultData[i].card2Count; j++)
                {
                    card[j] = DataBase.instance.defaultData[i].card2;
                }
                cardCount += DataBase.instance.defaultData[i].card2Count;
                for (int j = cardCount; j < cardCount + DataBase.instance.defaultData[i].card3Count; j++)
                {
                    card[j] = DataBase.instance.defaultData[i].card3;
                }

                string query = "INSERT INTO Deck (playerNum";
                for (int j = 1; j < 41; j++)
                {
                    query += ", no" + j.ToString();
                }
                query += ") VALUES (" + playerNum;

                for (int j = 0; j < 40; j++)
                {
                    query += ", " + card[j];
                }
                query += ")";
                return query;
            }
        }
        return null;
    }

    #endregion

}
