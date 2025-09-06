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
        MessageBox.Show("��δ����");
    }

    public void OnClickKickOut()
    {
        if (this.selectItem == null)
        {
            MessageBox.Show("��ѡ��Ҫ�߳��ĳ�Ա", "��ʾ");
            return;
        }
        MessageBox.Show(string.Format("ȷ��Ҫ��{0}�߳�������", this.selectItem.Info.Info.Name), "�߳���Ա", MessageBoxType.Confirm, "ȷ��", "ȡ��").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Kickout, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickPromote()
    {
        if (selectItem == null)
        {
            MessageBox.Show("��ѡ��Ҫ�����ĳ�Ա", "��ʾ");
            return;
        }
        if (selectItem.Info.Title != GuildTitle.None)
        {
            MessageBox.Show("�Է���������", "��ʾ");
            return;
        }
        MessageBox.Show(string.Format("Ҫ������{0}��Ϊ���᳤��", this.selectItem.Info.Info.Name), "����", MessageBoxType.Confirm, "ȷ��", "ȡ��").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Promote, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickDepose()
    {
        if (selectItem == null)
        {
            MessageBox.Show("��ѡ��Ҫ����ĳ�Ա", "��ʾ");
            return;
        }
        if (selectItem.Info.Title == GuildTitle.None)
        {
            MessageBox.Show("�Է�ò����ְ����", "��ʾ");
            return;
        }
        if(selectItem.Info.Title == GuildTitle.President)
        {
            MessageBox.Show("�᳤�������ܶ���", "��ʾ");
            return;
        }
        MessageBox.Show(string.Format("Ҫ���⡾{0}����ְ����", this.selectItem.Info.Info.Name), "����", MessageBoxType.Confirm, "ȷ��", "ȡ��").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Depost, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickTransfer()
    {
        if(selectItem == null)
        {
            MessageBox.Show("��ѡ��Ҫ�ѻ᳤ת�ø��ĳ�Ա", "��ʾ");
            return;
        }
        MessageBox.Show(string.Format("Ҫ�ѻ᳤ת�ø���{0}����", this.selectItem.Info.Info.Name), "ת�û᳤", MessageBoxType.Confirm, "ȷ��", "ȡ��").OnYes = () =>
        {
            GuildService.Instance.SendAdminCommand(GuildAdminCommand.Transfer, this.selectItem.Info.Info.Id);
        };
    }

    public void OnClickSetNotice()
    {
        MessageBox.Show("��δ����");
    }
}
