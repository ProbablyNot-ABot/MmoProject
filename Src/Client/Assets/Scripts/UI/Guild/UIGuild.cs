using Managers;
using Services;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuild : UIWindow
{
    public GameObject itemPrefab;
    public ListView listMain;
    public Transform itemRoot;
    public UIGuildInfo uiInfo;
    //public UIGuildItem selectItem;
    public UIGuildMemberItem selectItem;

    public GameObject panelAdmin;
    public GameObject panelLeader;
    // Start is called before the first frame update
    void Start()
    {
        GuildService.Instance.OnGuildUpdate = UpdateUI;
        this.listMain.onItemSelected += this.OnGuildMemberSelected;
        this.UpdateUI();
    }

    private void OnDestroy()
    {
        GuildService.Instance.OnGuildUpdate -= UpdateUI;
    }

    void UpdateUI()
    {
        this.uiInfo.Info = GuildManager.Instance.guildInfo;

        ClearList();
        InitItems();

        this.panelAdmin.SetActive(GuildManager.Instance.myMemberInfo.Title > GuildTitle.None);
        this.panelLeader.SetActive(GuildManager.Instance.myMemberInfo.Title == GuildTitle.President);
    }

    public void OnGuildMemberSelected(ListView.ListViewItem item)
    {
        this.selectItem = item as UIGuildMemberItem;
    }

    void InitItems()
    {
        foreach (var item in GuildManager.Instance.guildInfo.Members)
        {
            GameObject go = Instantiate(itemPrefab, this.listMain.transform);
            UIGuildMemberItem ui = go.GetComponent<UIGuildMemberItem>();
            ui.SetGuildMemberInfo(item);
            this.listMain.AddItem(ui);
        }
    }

    void ClearList()
    {
        this.listMain.RemoveAll();
    }

    public void OnClickAppliesList()
    {
        UIManager.Instance.Show<UIGuildApplyList>();
    }

    public void OnClickLeace()
    {
        MessageBox.Show("暂未开放");
    }

    public void OnClickKickOut()
    {
        if (this.selectItem == null)
        {
            MessageBox.Show("请选择要踢出的成员", "提示");
            return;
        }
        MessageBox.Show(string.Format("确定要将{0}踢出公会吗？", this.selectItem.Info.Info.Name), "踢出成员", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Kickout, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickPromote()
    {
        if (selectItem == null)
        {
            MessageBox.Show("请选择要提升的成员", "提示");
            return;
        }
        if (selectItem.Info.Title != GuildTitle.None)
        {
            MessageBox.Show("对方已身份尊贵", "提示");
            return;
        }
        MessageBox.Show(string.Format("要晋升【{0}】为副会长吗？", this.selectItem.Info.Info.Name), "晋升", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Promote, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickDepose()
    {
        if (selectItem == null)
        {
            MessageBox.Show("请选择要罢免的成员", "提示");
            return;
        }
        if (selectItem.Info.Title == GuildTitle.None)
        {
            MessageBox.Show("对方貌似无职可免", "提示");
            return;
        }
        if(selectItem.Info.Title == GuildTitle.President)
        {
            MessageBox.Show("会长不是你能动的", "提示");
            return;
        }
        MessageBox.Show(string.Format("要罢免【{0}】的职务吗？", this.selectItem.Info.Info.Name), "罢免", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Depost, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickTransfer()
    {
        if(selectItem == null)
        {
            MessageBox.Show("请选择要把会长转让给的成员", "提示");
            return;
        }
        MessageBox.Show(string.Format("要把会长转让给【{0}】吗？", this.selectItem.Info.Info.Name), "转让会长", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Transfer, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickSetNotice()
    {
        MessageBox.Show("暂未开放");
    }
}
