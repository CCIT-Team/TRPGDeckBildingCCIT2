using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.playerUI = gameObject;
    }

    public void SetPlayer(GameObject[] player)
    {
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
