using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Portion_", menuName = "Inventory System/Item Data/Portion", order = 3)]
public class PortionItemData : CountableItemData
{
    public string Effect => _effect;
    [Multiline]
    [SerializeField] private string _effect;
    public float UseCost => _useCost;
    [SerializeField] private float _useCost;

    public override Item CreateItem()
    {
        return new PortionItem(this);
    }
}
