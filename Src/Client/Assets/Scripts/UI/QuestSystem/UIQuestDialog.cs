using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class UIQuestDialog : UIWindow
//{
//    public UIQuestInfo questInfo;

//    public Quest quest;

//    public GameObject openButtons;
//    public GameObject submitButtons;

//    public void SetQuest(Quest quest)
//    {
//        this.quest = quest;
//        this.UpdateQuest();
//        if(this.quest.Info == null)
//        {
//            openButtons.SetActive(true);
//            submitButtons.SetActive(false);
//        }
//        else
//        {
//            if(this.quest.Info.Status == SkillBridge.Message.QuestStatus.Complated)
//            {
//                openButtons.SetActive(false);
//                submitButtons.SetActive(true);
//            }
//            else
//            {
//                openButtons.SetActive(false);
//                submitButtons.SetActive(false);
//            }
//        }
//    }

//    void UpdateQuest()
//    {
//        if(this.quest != null)
//        {
//            if(this.questInfo != null)
//            {
//                this.questInfo.SetQuestInfo(quest);
//            }
//        }
//    }
//}

public class UIQuestDialog : UIWindow
{
    public UIQuestInfo questInfo;
    public GameObject openButtons;
    public GameObject submitButtons;

    public Quest quest;

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        UpdateQuest();
        if (quest.Info == null)
        {
            submitButtons.SetActive(false);
            openButtons.SetActive(true);
        }
        else
        {
            if(quest.Info.Status == SkillBridge.Message.QuestStatus.Complated)
            {
                submitButtons.SetActive(true);
                openButtons.SetActive(false);
            }
            else
            {
                submitButtons.SetActive(false);
                openButtons.SetActive(false);
            }
        }
    }

    void UpdateQuest()
    {
        if (this.quest != null && this.questInfo != null)
        {
            this.questInfo.SetQuestInfo(quest);
        }
    }
}