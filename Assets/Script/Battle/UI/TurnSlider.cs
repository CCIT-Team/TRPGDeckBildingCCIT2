using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSlider : MonoBehaviour
{
    public Slider slider;
    public Unit boundUnit;
    public RawImage renderImage;

    RectTransform rectTransform;

    public void BindingUnit(Unit unit,int maxValue)
    {
        rectTransform = GetComponent<RectTransform>();
        boundUnit = unit;
        name = unit + "_Icon";
        slider.maxValue = maxValue;   //아이콘 변경 후 제거 예정
    }

    public IEnumerator DisplayTurn()   //나중에 수정될 가능성 높음
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if(boundUnit.gameObject.activeSelf == false)
            {
                BattleUI.instance.AnnounceUnitDead(this);
                break;
            }

            rectTransform.sizeDelta = new Vector2(100 * (N_BattleManager.instance.units.Count + 1), 75);

            if (boundUnit != N_BattleManager.instance.currentUnit)
            {
                slider.value = 1 + N_BattleManager.instance.units.IndexOf(boundUnit);
                slider.handleRect.localScale = new Vector3(0.7f, 0.7f, 1);
            }
            else
            {
                slider.value = 0;
                slider.handleRect.localScale = new Vector3(1.3f,1.3f,1);
            }
        }
    }
}
