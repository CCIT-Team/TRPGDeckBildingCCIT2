using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();
    List<Unit> uiUnits = new List<Unit>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetTurnSlider(List<Unit> units)
    {
        for(int i = 0; i < units.Count; i++)
        {
            uiUnits.Add(units[i]); 
            Slider icon = Instantiate(slider, this.transform);
            icon.name = "Unit" + (i + 1);
            icon.maxValue = units.Count - 1;
            icon.handleRect.gameObject.GetComponent<Image>().color = new Color(1 * (float)i / units.Count, 1 * (float)i / units.Count, 1 * (float)i / units.Count);
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
                if (uiUnits[i] == N_BattleManager.instance.currentUnit)
                    turnSlider[i].value = 0;
                else
                    turnSlider[i].value = 1 + N_BattleManager.instance.units.IndexOf(uiUnits[i]);
            }
        }
    }
}
