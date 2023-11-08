using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSlider : MonoBehaviour
{
    public Slider slider;
    Unit boundUnit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BindingUnit(Unit unit,int maxValue)
    {
        boundUnit = unit;
        name = unit + "_Icon";
        slider.maxValue = maxValue;
        slider.handleRect.gameObject.GetComponent<Image>().color = new Color(1 * (float)N_BattleManager.instance.units.IndexOf(unit) / (maxValue + 1), 1 * (float)N_BattleManager.instance.units.IndexOf(unit) / (maxValue+1), 1 * (float)N_BattleManager.instance.units.IndexOf(unit) / (maxValue + 1)); //���������� ���� ����
        slider.handleRect.transform.GetComponentInChildren<Text>().text = unit.name;   //������ ���� �� ���� ����
    }

    public IEnumerator DisplayTurn()   //���߿� ������ ���ɼ� ����
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

                if (boundUnit != N_BattleManager.instance.currentUnit)
                    slider.value = 1 + N_BattleManager.instance.units.IndexOf(boundUnit);
                else
                    slider.value = 0;
        }
    }
}
