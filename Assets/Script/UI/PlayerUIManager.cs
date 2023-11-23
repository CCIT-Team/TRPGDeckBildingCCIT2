using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    private GameObject[] player;
    private void Awake()
    {
        int player = DataBase.instance.loadTypeData.Count;
        GameObject childs;
        for(int i = 0; i < player; i++)
        {
            childs = transform.GetChild(i).gameObject;
            childs.SetActive(true);
            childs.GetComponent<PlayerStatUI>().character = DataBase.instance.loadStatData[i];
            childs.GetComponent<PlayerStatUI>().character_Type = DataBase.instance.loadTypeData[i];
        }
    }

    public void SetPlayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        GameObject childUI;
        for (int i = 0; i < player.Length; i++)
        {
            childUI = transform.GetChild(i).gameObject;
            childUI.GetComponent<PlayerStatUI>().LinkingPlayer(player[i]);
        }
    }
}
