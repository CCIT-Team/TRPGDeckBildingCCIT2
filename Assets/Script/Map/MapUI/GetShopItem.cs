using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetShopItem : MonoBehaviour
{
    public Shop shop;
    public ShopItem item;
    public Button button;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemPrise;

    private void Start()
    {
        shop = Map.instance.tileUI.shop;
    }

    public void UpdateItemInfo()
    {
        itemImage.sprite = item._itemsprite;
        itemName.text = item._itemname;
        itemPrise.text = item._itemprise.ToString();
    }

    public void BuyItem()
    {
        if(Map.instance.wolrdTurn.currentPlayer.gold >= item._itemprise)
        {
            Map.instance.wolrdTurn.currentPlayer.gold -= item._itemprise;
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character_type>().invenUI.SetInvenItem(item._itemcode,1);
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character_type>().invenUI.UpdateMoneyUI();
            shop.currentHaveGold.text = Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold.ToString();
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(shop.Comebackwiththemoney());
        }
    }
}
