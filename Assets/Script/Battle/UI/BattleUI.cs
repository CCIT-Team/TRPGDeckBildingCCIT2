using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public GameObject inputBlocker;

    [Header("TurnDisplay")]

    [SerializeField]
    GameObject turnDisplay;

    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();

    public GameObject TurnAnnounce;
    public TMP_Text announceText;


    [Header("Player")]
    public PlayerBattleUI[] playerUI = new PlayerBattleUI[3];
    List<Unit> boundUnits = new List<Unit>();

    [Header("Token")]
    public Image[] tokens;

    public void BindPlayer(GameObject[] playerarray)
    {
        for(int i = 0; i < playerarray.Length; i++)
        {
            playerUI[i].BindCharacter(playerarray[i].GetComponent<Character>());
        }
    }

    int RollToken(int tokenAmount)
    {
        int rollResult = tokenAmount;
        for (int i = 0; i < tokenAmount; i++)
        {
            int x = UnityEngine.Random.Range(0, 100);
            if (x <= 100)
            {
                Debug.Log("����");
            }
            else
            {
                Debug.Log("����");
                rollResult--;
            }
        }
        return rollResult;
    }

    #region ��
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
        //�����̺��� ������ ���� �� ���� �� ����
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
