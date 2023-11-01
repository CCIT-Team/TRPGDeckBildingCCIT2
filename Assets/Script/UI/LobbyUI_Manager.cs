using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUI_Manager : MonoBehaviour
{
    public GameObject panel;
    public List<List<GameObject>> avatars_slot = new List<List<GameObject>>();

    [SerializeField]
    public List<GameObject> avatar2 = new List<GameObject>();

    [SerializeField]
    public List<GameObject> avatar3 = new List<GameObject>();

    public CharacterSlot_UI[] avatarSet;
    public TMP_InputField[] avatar_InputNames;

    private void Start()
    {
        avatars_slot.Add(avatar2);
        avatars_slot.Add(avatar3);
    }
    public void Avatar_Delete_button(int num)
    {
        avatars_slot[num][0].gameObject.SetActive(false);
        avatars_slot[num][1].gameObject.SetActive(false);
        avatars_slot[num][2].gameObject.SetActive(false);
        avatars_slot[num][3].gameObject.SetActive(true);
    }

    public void Avatar_Create_button(int num)
    {
        avatars_slot[num][0].gameObject.SetActive(true);
        avatars_slot[num][1].gameObject.SetActive(true);
        avatars_slot[num][2].gameObject.SetActive(true);
        avatars_slot[num][3].gameObject.SetActive(false);
    }

    public void PlayButton(string sceneName)
    {
        if(Input_Exception())
        {
            AvatarSetting();
            GameManager.instance.GetLobbyAvatar();
            panel.SetActive(true);

            GameManager.instance.LoadScenceName(sceneName);
        }
    }

    private bool Input_Exception()
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

    private void AvatarSetting()
    {
        for(int i = 0; i < avatarSet.Length; i++)
        {
            if(avatarSet[i].gameObject.activeSelf)
            {
                avatarSet[i].SetType(i);
                GameManager.instance.avatarCounter++;
            }
        }
    }
}
