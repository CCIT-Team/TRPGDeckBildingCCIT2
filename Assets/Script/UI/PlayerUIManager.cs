using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    private GameObject[] player;

    private void Awake()
    {
        GameManager.instance.playerUI = gameObject;
    }

    public void SetPlayer(GameObject[] player)
    {
        //player = GameObject.FindGameObjectsWithTag("Player");
        GameObject childUI;
        for (int i = 0; i < player.Length; i++)
        {
            childUI = transform.GetChild(i + 3).gameObject;
            childUI.SetActive(true);
            childUI.GetComponent<PlayerStatUI>().LinkingPlayer(player[i]);
            childUI.GetComponent<PlayerStatUI>().InitUI();
        }
    }
}
