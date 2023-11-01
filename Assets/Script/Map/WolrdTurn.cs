using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdTurn : MonoBehaviour
{
    List<GameObject> characterTurnUIs;
    public List<GameObject> players;
    public GameObject currentPlayer;
    void Start()
    {
        players = Map.instance.players;
    }

    void Update()
    {

    }

    IEnumerator PlayTurn()
    {
        currentPlayer = players[0];
        players.Remove(currentPlayer);
        currentPlayer.GetComponent<Character>().isMyturn = true;
        yield return new WaitUntil(() => !currentPlayer.GetComponent<Character>().isMyturn);

        players.Add(currentPlayer);
        currentPlayer = null;
        StartCoroutine(PlayTurn());
    }
}
