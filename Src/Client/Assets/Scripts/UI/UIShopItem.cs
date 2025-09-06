using Common.Data;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour,ISelectHandler
{
    public Image icon;
    public Text title;
    public Text price;
    public Text limitClass;
    public Text count;

    public Image background;
    public Sprite normalBag;
    public Sprite selectedBag;

    private bool selected;
    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            background.overrideSprite = selected ? selectedBag : normalBag;
        }
    }


    public int ShopItemID { get; set; }
    private UIShop shop;

    private ItemDefine item;
    private ShopItemDefine ShopItem { get; set; }
    public void SetShopItem(int id, ShopItemDefine shopItem, UIShop owner)
    {
        this.shop = owner;
        this.ShopItemID = id;
        this.ShopItem = shopItem;
        this.item = DataManager.Instance.Items[shopItem.ItemID];

        this.title.text = this.item.Name;
        this.count.text = "x" + shopItem.Count.ToString();
        this.limitClass.text = this.item.LimitClass.ToString();
        this.price.text = shopItem.Price.ToString();
        this.icon.overrideSprite = Resloader.Load<Sprite>(item.Icon);
    }

    public void OnSelect(BaseEventData eventData)
    {
        this.Selected = true;
        this.shop.SelectShopItem(this);
    }

}
