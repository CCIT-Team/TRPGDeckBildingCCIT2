using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance;
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
    public GameObject tokenPosition;
    public Token tokenPrefab;
    public List<Token> tokens;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(this);
    }

    public void BindPlayer(GameObject[] playerarray)
    {
        for(int i = 0; i < playerarray.Length; i++)
        {
            playerUI[i].BindCharacter(playerarray[i].GetComponent<Character>());
        }
    }

    public int RollToken(int mainStatus,int tokenAmount)
    {
        for(int i =0; i< tokenAmount; i++)
            tokens.Add(Instantiate(tokenPrefab,tokenPosition.transform));

        for (int i = 0; i < tokenAmount; i++)
        {
            tokens[i].transform.localPosition = new Vector2((-tokenAmount / 2 + i + (tokenAmount + 1) % 2 / 2f) * 160, 0);
        }

        int rollResult = tokenAmount;
        for (int i = 0; i < tokenAmount; i++)
        {
            int x = Random.Range(0, 100);
            if (x <= mainStatus)
            {
                tokens[i].CheckToken(StatusType.Intelligence, true);
            }
            else
            {
                tokens[i].CheckToken(StatusType.Intelligence, false);
                rollResult--;
            }
            Destroy(tokens[i].gameObject, 0.5f);
        }
        tokens.Clear();
        return rollResult;
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
