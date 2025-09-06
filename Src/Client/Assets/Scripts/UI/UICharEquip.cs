using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillBridge.Message;

public class UICharEquip : UIWindow
{
    public Text title;
    public Text money;

    public GameObject itemPrefab;
    public GameObject itemEquipedPrefab;

    public Transform itemListRoot;

    public List<Transform> slots;

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
        EquipManager.Instance.OnEquipChange += RefreshUI;
    }

    void OnDestroy()
    {
        EquipManager.Instance.OnEquipChange -= RefreshUI;
    }

    void RefreshUI()
    {
        ClearAllEquipList();
        InitAllEquipItems();
        ClearEquipdList();
        InitEquipedItems();
        this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }

    private void InitAllEquipItems()
    {
        foreach (var kv in ItemManager.Instance.Items)
        {
            if(kv.Value.Define.type == ItemType.Equip)
            {
                //�Ѿ�װ���˾Ͳ���ʾ��
                if (EquipManager.Instance.Contains(kv.Key) && kv.Value.Define.LimitClass == User.Instance.CurrentCharacter.Class)
                    continue;
                GameObject go = Instantiate(itemPrefab, itemListRoot);
                UIEquipItem ui = go.GetComponent<UIEquipItem>();
                ui.SetEquipItem(kv.Key,kv.Value,this,false);
            }
        }
    }

    private void ClearAllEquipList()
    {
        foreach(var item in itemListRoot.GetComponentsInChildren<UIEquipItem>())
        {
            Destroy(item.gameObject);
        }
    }

    private void ClearEquipdList()
    {
        foreach(var item in slots)
        {
            if(item.childCount > 0)
            {
                Destroy(item.GetChild(0).gameObject);
            }
        }
    }

    private void InitEquipedItems()
    {
        for(int i = 0; i < (int)EquipSlot.SlotMax; i++)
        {
            var item = EquipManager.Instance.Equips[i];
            if(item != null)
            {
                GameObject go = Instantiate(itemEquipedPrefab, slots[i]);
                UIEquipItem ui = go.GetComponent<UIEquipItem>();
                ui.SetEquipItem(i, item, this, true);
            }
        }
    }

    public void DoEquip(Item item)
    {
        EquipManager.Instance.EquipItem(item);
    }
    public void UnEquip(Item item)
    {
        EquipManager.Instance.UnEquipItem(item);
    }

}
