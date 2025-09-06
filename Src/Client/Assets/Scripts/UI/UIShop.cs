using Common.Data;
using Managers;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIWindow
{
    public Text title;
    public Text money;

    public GameObject shopItem;
    ShopDefine shop;
    public Transform[] itemRoot;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitItems());
    }

    IEnumerator InitItems()
    {
        int count = 0;
        int page = 0;
        foreach (var kv in DataManager.Instance.ShopItems[shop.ID])
        {
            if (kv.Value.Status > 0)
            {
                GameObject go = Instantiate(shopItem, itemRoot[page]);
                UIShopItem ui = go.GetComponent<UIShopItem>();
                ui.SetShopItem(kv.Key, kv.Value, this);
                count++;
                if(count >= 10)
                {
                    count = 0;
                    page++;
                    itemRoot[page].gameObject.SetActive(true);
                }
            }
        }
        yield return null;
    }

    public void SetShop(ShopDefine shop)
    {
        this.shop = shop;
        this.title.text = shop.Name;
        this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }

    private UIShopItem selectedItem;
    internal void SelectShopItem(UIShopItem item)
    {
        if(selectedItem != null)
        {
            selectedItem.Selected = false;
        }
        selectedItem = item;
    }

    public void OnClickBuy()
    {
        if (this.selectedItem == null)
        {
            MessageBox.Show("��ѡ��Ҫ����ĵ���", "������ʾ");
            return;
        }
        if(!ShopManager.Instance.BuyItem(shop.ID, selectedItem.ShopItemID))
        {
            
        }
    }
}
