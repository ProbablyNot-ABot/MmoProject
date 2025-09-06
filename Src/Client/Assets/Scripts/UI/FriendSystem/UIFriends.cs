using Managers;
using Models;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFriends : UIWindow
{
    public GameObject itemPrefab;
    public ListView listMain;
    public Transform itemRoot;
    public UIFriendItem selectedItem;

    void Start()
    {
        FriendService.Instance.OnFriendUpdate = RefreshUI;    
        this.listMain.onItemSelected += this.OnFriendSelected;
        RefreshUI();
    }

    public void OnFriendSelected(ListView.ListViewItem item)
    {
        this.selectedItem = item as UIFriendItem;
    }

    public void OnClickFriendAdd()
    {
        InputBox.Show("����Ҫ��ӵĺ������ƻ�ID", "��Ӻ���").OnSubmit += OnFriendAddSubmit;
    }

    private bool OnFriendAddSubmit(string input, out string tips)
    {
        tips = "";
        int friendId = 0;
        string firendName = "";
        if (!int.TryParse(input, out friendId))
            firendName = input;
        if (friendId == User.Instance.CurrentCharacter.Id || firendName == User.Instance.CurrentCharacter.Name)
        {
            tips = "��������Լ�Ϊ����Ŷ";
            return false;
        }
        FriendService.Instance.SendFriendAddRequest(friendId, firendName);
        return true;
    }

    public void OnClickFriendChat()
    {
        MessageBox.Show("��δ����");
    }

    public void OnClickFriendTeamInvite()
    {
        if(selectedItem == null)
        {
            MessageBox.Show("��ѡ��Ҫ����ĺ���");
            return;
        }
        if(selectedItem.Info.Status == 0)
        {
            MessageBox.Show("��ѡ�����ߵĶ���");
            return;
        }
        MessageBox.Show(string.Format("ȷ��Ҫ����ĺ��ѡ�{0}�����������", selectedItem.Info.friendInfo.Name), "����������", MessageBoxType.Confirm, "����", "ȡ��").OnYes = () =>
        {
            TeamService.Instance.SendTeamInviteRequest(this.selectedItem.Info.friendInfo.Id, this.selectedItem.Info.friendInfo.Name);
        };
    }

    public void OnClickFriendRemove()
    {
        if(selectedItem == null)
        {
            MessageBox.Show("��ѡ��Ҫɾ���ĺ���");
            return;
        }
        MessageBox.Show(string.Format("ȷ��Ҫɾ�����ѡ�{0}����", selectedItem.Info.friendInfo.Name), "ɾ������", MessageBoxType.Confirm, "ɾ��", "ȡ��").OnYes = () =>
        {
            FriendService.Instance.SendFriendRemoveRequest(this.selectedItem.Info.Id, this.selectedItem.Info.friendInfo.Id);
        };
    }

    void RefreshUI()
    {
        ClearFriendList();
        InitFriendItems();
    }

    void InitFriendItems()
    {
        foreach(var item in FriendManager.Instance.allFriends)
        {
            GameObject go = Instantiate(itemPrefab, this.listMain.transform);
            UIFriendItem ui = go.GetComponent<UIFriendItem>();
            ui.SetFriendInfo(item);
            this.listMain.AddItem(ui);
        }
    }

    void ClearFriendList()
    {
        this.listMain.RemoveAll();
    }
}
