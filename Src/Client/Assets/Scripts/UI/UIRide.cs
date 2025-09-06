using Models;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRide : UIWindow
{
    public Text descript;
    public GameObject itemPrefab;
    public ListView listMain;
    private UIRideItem selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
        this.listMain.onItemSelected += this.OnItemSelected;
    }

    private void OnDestroy()
    {
        //EquipManager.Instance.onEquipChanged -= this.OnEquipChanged;
    }

    public void OnItemSelected(ListView.ListViewItem item)
    {
        this.selectedItem = item as UIRideItem;
        this.descript.text = this.selectedItem.item.Define.Description;
    }

    void RefreshUI()
    {
        ClearItems();
        InitItems();
    }

    private void InitItems()
    {
        foreach (var kv in ItemManager.Instance.Items)
        {
            if (kv.Value.Define.type == ItemType.Ride && (kv.Value.Define.LimitClass == CharacterClass.None || kv.Value.Define.LimitClass == User.Instance.CurrentCharacter.Class))
            {
                if (EquipManager.Instance.Contains(kv.Key))
                    continue;
                GameObject go = Instantiate(itemPrefab, this.listMain.transform);
                UIRideItem ui = go.GetComponent<UIRideItem>();
                ui.SetEquipItem(kv.Value, this, false);
                this.listMain.AddItem(ui);
            }
        }
    }

    private void ClearItems()
    {
        this.listMain.RemoveAll();
    }

    public void DoRide()
    {
        if (this.selectedItem == null)
        {
            MessageBox.Show("请选择要召唤的坐骑","提示");
            return;
        }
        User.Instance.Ride(this.selectedItem.item.Id);
    }
}
