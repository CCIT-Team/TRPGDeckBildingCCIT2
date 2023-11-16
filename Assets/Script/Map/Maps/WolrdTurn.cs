using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdTurn : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public Character currentPlayer;
    public int turnNum = 0;
    void Start()
    {
        players = Map.instance.players;
        StartCoroutine(PlayTurn());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            turnNum += 1;
            Debug.Log(turnNum);
        }
    }
    IEnumerator PlayTurn()
    {
        //currentPlayer = players[0].GetComponent<Character>();
        if(currentPlayer == null) { currentPlayer = players[0].GetComponent<Character>(); }
        else { currentPlayer = players[0].GetComponent<Character>(); }
        currentPlayer.GetComponent<Character>().isMyturn = true;
        yield return new WaitUntil(() => !currentPlayer.GetComponent<Character>().isMyturn);
        yield return new WaitUntil(() => !Map.instance.isOutofUI);

        players.Remove(currentPlayer.gameObject);
        players.Add(currentPlayer.gameObject);
        currentPlayer = null;
        turnNum += 1;
        StartCoroutine(PlayTurn());
    }
}
