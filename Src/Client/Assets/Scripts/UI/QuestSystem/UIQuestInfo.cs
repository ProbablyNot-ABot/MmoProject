using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class UIQuestInfo : MonoBehaviour
//{
//    public Text title;
//    public Text[] targets;
//    public Text description;
//    public UIIconItem rewardItems;
//    public Text rewardMoney;
//    public Text rewardExp;
//    public Text overview;

//    public Button navButton;
//    private int npc = 0;
//    public void SetQuestInfo(Quest quest)
//    {
//        this.title.text = string.Format("[{0}]{1}",quest.Define.Type,quest.Define.Name);
//        if(this.overview != null) this.overview.text = quest.Define.Overview;
//        if(this.description != null)
//        {
//            if (quest.Info == null) //还没有接取任务
//            {
//                this.description.text = quest.Define.Dialog;
//            }
//            else //任务已接取
//            {
//                if (quest.Info.Status == SkillBridge.Message.QuestStatus.Complated) //任务已完成
//                {
//                    this.description.text = quest.Define.DialogFinish;
//                }
//            }
//        }
//        if(this.gameObject.name != "Panel")
//        {
//            this.rewardMoney.text = "金币：" + quest.Define.RewardGold.ToString();
//            this.rewardExp.text = "经验：" + quest.Define.RewardExp.ToString();
//        }
//        else
//        {
//            this.rewardMoney.text = "金币        " + quest.Define.RewardGold.ToString();
//            this.rewardExp.text = "经验        " + quest.Define.RewardExp.ToString();
//        }

//        if(quest.Info == null)
//        {
//            this.npc = quest.Define.AcceptNPC;
//        }
//        else if(quest.Info.Status == SkillBridge.Message.QuestStatus.Complated)
//        {
//            this.npc = quest.Define.SubmitNPC;
//        }
//        this.navButton.gameObject.SetActive(this.npc > 0);
//        foreach (var fitter in this.GetComponentsInChildren<ContentSizeFitter>())
//        {
//            fitter.SetLayoutVertical();
//        }
//    }

//    public void OnClickAbandon()
//    {

//    }

//    public void OnClickNav()
//    {
//        Vector3 pos = NPCManager.Instance.GetNpcPosition(this.npc);
//        User.Instance.CurrentCharacterObject.StartNav(pos);
//        UIManager.Instance.Close<UIQuestSystem>();
//    }
//}

public class UIQuestInfo : MonoBehaviour
{
    public Text questName;
    public Text questDescription;
    public Text questTarget;
    //public UIIconItem[] rewardItems;
    //public Text questReward;
    public Text questExp;
    public Text questGold;
    public Text overview;
    private Quest thisQuest;
    public UIQuestSystem owner;

    public void SetQuestInfo(Quest quest)
    {
        this.thisQuest = quest;
        if(this.questName != null) this.questName.text = string.Format("[{0}]{1}", quest.Define.Type == Common.Data.QuestType.Main ? "主线" : "支线", quest.Define.Name);
        if (this.overview != null) this.overview.text = quest.Define.Overview;
        
        if (quest.Info == null) //任务还未接取
        {
            if (questDescription != null) this.questDescription.text = User.Instance.CurrentCharacter.Name + "，" + quest.Define.Dialog;
        }
        else //任务已接取或完成
        {
            if(quest.Info.Status == SkillBridge.Message.QuestStatus.Complated) //任务已完成
            {
                if (questDescription != null) this.questDescription.text = quest.Define.DialogFinish;
            }
            else if(quest.Info.Status == SkillBridge.Message.QuestStatus.InProgress) //任务进行中
            {
                //MessageBox.Show("任务还在进行中", quest.Define.DialogInProgress, MessageBoxType.Information);
            }
        }

        if(this.questTarget != null)
        {
            if(quest.Define.Target1 != Common.Data.QuestTarget.None)
            {
                
            }
            else
            {
                //this.questTarget.text = quest.Define.Name;
            }
        }

        if (this.gameObject.name != "QuestInfoDia")
        {
            if (this.questGold != null) this.questGold.text = "金币：" + quest.Define.RewardGold.ToString();
            if (this.questExp != null) this.questExp.text = "经验：" + quest.Define.RewardExp.ToString();
        }
        else
        {
            if (this.questGold != null) this.questGold.text = "金币        " + quest.Define.RewardGold.ToString();
            if (this.questExp != null) this.questGold.text = "经验        " + quest.Define.RewardExp.ToString();
        }

        foreach (var fitter in this.GetComponentsInChildren<ContentSizeFitter>())
        {
            fitter.SetLayoutVertical();
        }
    }

    public void FollowQuest()
    {
        if (this.thisQuest != null)
        {
            QuestManager.Instance.FollowQuest(thisQuest, true);
            if(owner != null)
                owner.Close();
        }
        else
            MessageBox.Show("请选择一个任务");
    }

    public void AbandonFollowQuest()
    {
        if (this.thisQuest != null)
        {
            QuestManager.Instance.FollowQuest(thisQuest, false);
            //if (owner != null)
            //    owner.Close();
        }    
        else
            MessageBox.Show("请选择一个任务");
    }
}