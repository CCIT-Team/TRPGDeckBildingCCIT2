using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public int posionRegulerPrice = 18;
    public int posionLargePrice = 35;
    public int healPrise = 20;
    public int deleteCurse = 40;

   public  List<ShopItem> itemList = new List<ShopItem>();

    public GameObject item;
    public Transform itemListTransform;
    public GameObject noMoney;
    public TMP_Text currentHaveGold;


    private void Start()
    {
        ShopSellItems();
    }
    #region 상점

    public void ShopSellItems()
    {
        GameObject posionR = Instantiate(item, itemListTransform);
        posionR.GetComponent<GetShopItem>().item = itemList[0];
        posionR.GetComponent<GetShopItem>().UpdateItemInfo();
        GameObject posionL = Instantiate(item, itemListTransform);
        posionL.GetComponent<GetShopItem>().item = itemList[1];
        posionL.GetComponent<GetShopItem>().UpdateItemInfo();
        GameObject sword = Instantiate(item, itemListTransform);
        sword.GetComponent<GetShopItem>().item = itemList[2];
        sword.GetComponent<GetShopItem>().UpdateItemInfo();
        GameObject armor = Instantiate(item, itemListTransform);
        armor.GetComponent<GetShopItem>().item = itemList[Random.Range(3, 7)];
        armor.GetComponent<GetShopItem>().UpdateItemInfo();
        //switch (Map.instance.wolrdTurn.currentPlayer.level)
        //{
        //    case 1:
        //        GameObject posionR = Instantiate(item,itemListTransform);
        //        posionR.GetComponent<GetShopItem>().item = itemList[0];
        //        posionR.GetComponent<GetShopItem>().UpdateItemInfo();
        //        GameObject posionL = Instantiate(item, itemListTransform);
        //        posionR.GetComponent<GetShopItem>().item = itemList[1];
        //        posionR.GetComponent<GetShopItem>().UpdateItemInfo();
        //        GameObject sword = Instantiate(item, itemListTransform);
        //        posionR.GetComponent<GetShopItem>().item = itemList[2];
        //        posionR.GetComponent<GetShopItem>().UpdateItemInfo();
        //        GameObject armor = Instantiate(item, itemListTransform);
        //        posionR.GetComponent<GetShopItem>().item = itemList[Random.Range(3,7)];
        //        posionR.GetComponent<GetShopItem>().UpdateItemInfo();
        //        break;
        //    case 3:

        //        break;
        //    case 5:

        //        break;
        //    case 7:

        //        break;
        //    case 9:

        //        break;
        //    case 10:

        //        break;
        //    case 11:

        //        break;
        //}

    }

    public IEnumerator Comebackwiththemoney()
    {
        noMoney.SetActive(true);
        yield return new WaitForSeconds(1);
        noMoney.SetActive(false);
    }

    #endregion
    #region 치료소
    public void BuyingHeal()
    {
        Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().hp =
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().maxHp;
        Debug.Log("풀피");
    }
    public void BuyingDeleteCurse()
    {
        
    }
    #endregion
}
