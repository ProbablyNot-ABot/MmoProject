using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuildInfo : MonoBehaviour
{
    public Text guildName;
    public Text guildID;
    public Text leader;
    public Text notice;
    public Text memberNumber;

    private NGuildInfo info;
    public NGuildInfo Info
    {
        get { return this.info; }
        set
        {
            this.info = value;
            this.UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (this.info == null)
        {
            this.guildName.text = "��";
            this.guildID.text = "ID:0";
            this.leader.text = "�᳤: ��";
            this.notice.text = "";
            this.memberNumber.text = string.Format("��Ա����: 0/{0}", 50);//GameDefine.GuildMaxMemberCount);
        }
        else
        {
            this.guildName.text = this.info.GuildName;
            this.guildID.text = "ID:" + this.info.Id;
            this.leader.text = "�᳤: " + this.Info.leaderName;
            this.notice.text = this.info.Notice;
            this.memberNumber.text = string.Format("��Ա����: {0}/{1}", this.info.memberCount, 50);// GameDefine.GuildMaxMemberCount);
        }
    }
}
