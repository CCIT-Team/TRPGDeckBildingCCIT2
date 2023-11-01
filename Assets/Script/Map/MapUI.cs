using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();
    List<GameObject> uiUnits = new List<GameObject>();
    [SerializeField] WolrdTurn wolrdTurn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetTurnSlider(List<GameObject> players)
    {
        for(int i = 0; i < players.Count; i++)
        {
            uiUnits.Add(players[i]); 
            Slider icon = Instantiate(slider, this.transform);
            icon.name = "Unit" + (i + 1);
            icon.maxValue = players.Count - 1;
            icon.handleRect.gameObject.GetComponent<Image>().color = new Color(1 * (float)i / players.Count, 1 * (float)i / players.Count, 1 * (float)i / players.Count);
            turnSlider.Add(icon);
            icon.gameObject.SetActive(true);
        }
        StartCoroutine(DisplayTurn());
    }

    IEnumerator DisplayTurn()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < turnSlider.Count; i++)
            {
                if (uiUnits[i] == wolrdTurn.currentPlayer)
                    turnSlider[i].value = 0;
                else
                    turnSlider[i].value = 1 + wolrdTurn.players.IndexOf(uiUnits[i]);
            }
        }
    }
}
