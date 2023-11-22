using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IusableItem
{
    bool Use();
}
public abstract class ItemData_ : ScriptableObject
{
    public int Id => _id;
    public string Name => _name;
    public int BuyGold => _buyGold;
    public int SellGold => _sellGold;
    public Sprite IconSprite => _iconSprite;

    [SerializeField]private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _buyGold;
    [SerializeField] private int _sellGold;
    [SerializeField] private Sprite _iconSprite;

    public abstract Item CreateItem();
}
