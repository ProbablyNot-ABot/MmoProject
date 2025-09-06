using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoSingleton<UIMain>
{
    public Text avatarName;
    public Text avatarLevel;

    public UITeam TeamWindow;
    public UINpcInteractive npcInteractive;
    // Start is called before the first frame update
    protected override void OnStart()
    {
        UpdateAvatar();
    }

    void UpdateAvatar()
    {
        avatarName.text = string.Format("{0}[{1}]",User.Instance.CurrentCharacter.Name, User.Instance.CurrentCharacter.Id);
        avatarLevel.text = User.Instance.CurrentCharacter.Level.ToString();
    }

    //public void BackToCharSelect()
    //{
    //    SceneManager.Instance.LoadScene("CharSelect");
    //    Services.UserService.Instance.SendGameLeave();
    //}

    public void OnClickTest()
    {
        UITest test = UIManager.Instance.Show<UITest>();
        test.SetTitle("这是一个测试UI");
        test.OnClose += Test_OnClose;
    }

    private void Test_OnClose(UIWindow sender, UIWindow.WindowResult result)
    {

        MessageBox.Show("点击了对话框的：" + result,"对话框响应结果",MessageBoxType.Information);
    }

    public void OnClickBag()
    {
        UIManager.Instance.Show<UIBag>();
    }

    public void OnClickCharEquip()
    {
        UIManager.Instance.Show<UICharEquip>();
    }

    public void OnClickQuest()
    {
        UIManager.Instance.Show<UIQuestSystem>();
    }

    public void OnClickFriend()
    {
        UIManager.Instance.Show<UIFriends>();
    }

    public void OnClickGuild()
    {
        GuildManager.Instance.ShowGuild();
    }

    public void OnClickRide()
    {
        UIManager.Instance.Show<UIRide>();
    }

    public void OnClickSetting()
    {
        UIManager.Instance.Show<UISetting>();
    }

    public void OnShowNpcInteractive(string npcName)
    {
        npcInteractive.gameObject.SetActive(true);
        npcInteractive.OnShow(npcName);
    }
    public void OnHidenNpcInteractive()
    {
        npcInteractive.gameObject.SetActive(false);
    }

    public void OnClickSkill()
    {

    }

    public void ShowTemaUI(bool show)
    {
        TeamWindow.ShowTeam(show);
    }
}
