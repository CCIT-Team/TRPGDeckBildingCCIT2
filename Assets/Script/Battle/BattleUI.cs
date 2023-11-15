using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject inputBlocker;

    [Header("TurnDisplay")]

    [SerializeField]
    GameObject turnDisplay;

    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();


    [Header("Player")]
    public PlayerBattleUI[] playerUI = new PlayerBattleUI[3];
    List<Unit> boundUnits = new List<Unit>();

    public void BindPlayer(GameObject[] playerarray)
    {
        for(int i = 0; i < playerarray.Length; i++)
        {
            playerUI[i].BindCharacter(playerarray[i].GetComponent<Character>());
        }
    }

    #region 턴
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

    public void AnnounceUnitDead(TurnSlider unitIcon)
    {
        //턴테이블에서 아이콘 제거 후 남은 턴 조정
        turnSlider.Remove(unitIcon.slider);
        unitIcon.gameObject.SetActive(false);
        foreach(Slider slider in turnSlider)
        {
            slider.maxValue = turnSlider.Count - 1;
        }

        foreach(PlayerBattleUI PUI in playerUI)
        {
            if(PUI.boundCharacter == unitIcon.boundUnit)
            {
                PUI.UnBindCharacter();
                PUI.gameObject.SetActive(false);
            }
        }
    }

    #endregion
}
