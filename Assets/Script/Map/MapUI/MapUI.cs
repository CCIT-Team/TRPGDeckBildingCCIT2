using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MapUI : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();
    List<GameObject> uiUnits = new List<GameObject>();
    [SerializeField] WolrdTurn wolrdTurn;

    public TMP_Text climateName;
    void Start()
    {
        //Map.instance.mapUI = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { TurnEnd(); }
    }

    public void TurnEnd()
    {
        if(!Map.instance.isPlayerMoving)
        {
            wolrdTurn.currentPlayer.isMyturn = false;
            wolrdTurn.currentPlayer.GetComponent<Character>().cost = wolrdTurn.currentPlayer.GetComponent<Character>().maxCost;
        }
    }

    public void SetTurnSlider(List<GameObject> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            uiUnits.Add(players[i]);
            Slider icon = Instantiate(slider, this.transform);
            icon.handleRect.GetComponentInChildren<TMP_Text>().text = players[i].name;
            icon.name = "Unit" + (i + 1);
            icon.maxValue = players.Count - 1;
            turnSlider.Add(icon);
            icon.gameObject.SetActive(true);
        }
        StartCoroutine(DisplayTurn());
    }

    IEnumerator DisplayTurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < turnSlider.Count; i++)
            {
                if (uiUnits[i] == wolrdTurn.currentPlayer)
                    turnSlider[i].value = 0;
                else
                    turnSlider[i].value = wolrdTurn.players.IndexOf(uiUnits[i]);
            }
        }
    }
}
