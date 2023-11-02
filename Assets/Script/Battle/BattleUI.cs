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
            Slider icon = Instantiate(slider, turnDisplay.transform);
            icon.name = (units[i]+" Icon");
            icon.maxValue = units.Count - 1;
            icon.handleRect.gameObject.GetComponent<Image>().color = new Color(1 * (float)i / units.Count, 1 * (float)i / units.Count, 1 * (float)i / units.Count); //아이콘으로 변경 예정
            icon.handleRect.transform.GetComponentInChildren<Text>().text = units[i].gameObject.name;   //아이콘 변경 후 제거 예정
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
                if (boundUnits[i] == N_BattleManager.instance.currentUnit)
                    turnSlider[i].value = 0;
                else
                    turnSlider[i].value = 1 + N_BattleManager.instance.units.IndexOf(boundUnits[i]);
            }
        }
    }
    #endregion
}
