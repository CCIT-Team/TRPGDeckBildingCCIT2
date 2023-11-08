using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("TurnDisplay")]

    [SerializeField]
    GameObject turnDisplay;

    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();


    List<Unit> boundUnits = new List<Unit>();

    [Header("Player")]
    public PlayerBattleUI[] playerUI = new PlayerBattleUI[3];


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BindPlayer(GameObject[] playerarray)
    {
        for(int i = 0; i < playerarray.Length; i++)
        {
            playerUI[i].BindCharacter(playerarray[i].GetComponent<Character>());
        }
    }

    #region 턴 슬라이더
    public void SetTurnSlider(List<Unit> units)
    {
        for(int i = 0; i < units.Count; i++)
        {
            boundUnits.Add(units[i]);
            TurnSlider icon = Instantiate(slider, turnDisplay.transform).GetComponent<TurnSlider>();
            turnSlider.Add(icon.GetComponent<Slider>());
            icon.BindingUnit(units[i], units.Count - 1);
            icon.StartCoroutine(icon.DisplayTurn());
        }
    }
    #endregion
}
