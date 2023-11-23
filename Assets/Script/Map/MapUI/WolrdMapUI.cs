using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdMapUI : MonoBehaviour
{
    public PlayerStatUI playerStatUI;

    public Transform[] player_UI_pos = new Transform[3];

    public GameObject playerUI;

    void Start()
    {
        for(int i = 0; i < Map.instance.players.Count; i++)
        {
            GameObject playerUIs = Instantiate(playerUI, player_UI_pos[i]);
            //playerStatUI = playerUIs.GetComponent<PlayerStatUI>();
            //playerStatUI.character = Map.instance.players[i].GetComponent<Character>();
            //playerStatUI.character_Type = Map.instance.players[i].GetComponent<Character_type>();
        }
    }

    
    void Update()
    {
        
    }
}
