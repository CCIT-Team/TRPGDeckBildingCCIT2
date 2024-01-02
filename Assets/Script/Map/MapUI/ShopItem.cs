using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Object/ShopItem", order = int.MaxValue)]
public class ShopItem : ScriptableObject
{
    [SerializeField]
    int itemCode;
    public int _itemcode { get { return itemCode; }}
    [SerializeField]
    string itemName;
    public string _itemname { get { return itemName; } }
    [SerializeField]
    int itemPrise;
    public int _itemprise { get { return itemPrise; } }
    [SerializeField]
    Sprite itemSprite;
    public Sprite _itemsprite { get { return itemSprite; } }


}
