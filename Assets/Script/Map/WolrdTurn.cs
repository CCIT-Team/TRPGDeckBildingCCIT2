using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdTurn : MonoBehaviour
{
    List<GameObject> characterTurnUIs;
    public List<GameObject> players = new List<GameObject>();
    public Character currentPlayer;
    void Start()
    {
        players = Map.instance.players;
        StartCoroutine(PlayTurn());
    }

    void Update()
    {

    }

    IEnumerator PlayTurn()
    {
        //currentPlayer = players[0].GetComponent<Character>();
        if(currentPlayer == null) { currentPlayer = players[0].GetComponent<Character>(); }
        else { currentPlayer = players[0].GetComponent<Character>(); }
        currentPlayer.GetComponent<Character>().isMyturn = true;
        yield return new WaitUntil(() => !currentPlayer.GetComponent<Character>().isMyturn);

        players.Remove(currentPlayer.gameObject);
        players.Add(currentPlayer.gameObject);
        currentPlayer = null;
        StartCoroutine(PlayTurn());
    }
}
