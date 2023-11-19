using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WolrdTurn : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public Character currentPlayer;
    public int turnNum = 0;
    public GameObject turnNicknameObejct;
    public TMP_Text turnNickName;//누구의 턴인지 알려주는 UI
    void Start()
    {
        players = Map.instance.players;
        turnNicknameObejct.SetActive(false);
        StartCoroutine(PlayTurn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            turnNum += 1;
            Debug.Log(turnNum);
        }
    }
    IEnumerator PlayTurn()
    {
        //currentPlayer = players[0].GetComponent<Character>();
        if (currentPlayer == null) { currentPlayer = players[0].GetComponent<Character>(); }
        else { currentPlayer = players[0].GetComponent<Character>(); }
        currentPlayer.GetComponent<Character>().isMyturn = true;
        StartCoroutine(OnTurnNicknameUI());
        yield return new WaitUntil(() => !currentPlayer.GetComponent<Character>().isMyturn);
        yield return new WaitUntil(() => !Map.instance.isOutofUI);

        players.Remove(currentPlayer.gameObject);
        players.Add(currentPlayer.gameObject);
        currentPlayer = null;
        turnNum += 1;
        StartCoroutine(PlayTurn());
    }

    public IEnumerator OnTurnNicknameUI()
    {
        turnNicknameObejct.SetActive(true);
        turnNickName.text = currentPlayer.name + "의 턴!";
        yield return new WaitForSeconds(2f);
        turnNicknameObejct.SetActive(false);
        StopCoroutine(OnTurnNicknameUI());
    }
}
