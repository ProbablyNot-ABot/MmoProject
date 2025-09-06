using Common.Utils;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuildItem : ListView.ListViewItem
{
    public Text guildID;
    public Text guildName;
    public Text guildNum;
    public Text guildLeader;

    public Image background;
    public Sprite normalBg;
    public Sprite selectedBg;

    public NGuildInfo Info;

    public override void onSelected(bool selected)
    {
        this.background.overrideSprite = selected ? selectedBg : normalBg;
    }

    public void SetGuildInfo(NGuildInfo info)
    {
        this.Info = info;
        if (this.guildID != null) this.guildID.text = this.Info.Id.ToString();
        if (this.guildName != null) this.guildName.text = this.Info.GuildName;
        if (this.guildNum != null) this.guildNum.text = this.Info.memberCount.ToString();
        if (this.guildLeader != null) this.guildLeader.text = this.Info.leaderName.ToString();
    }
}
