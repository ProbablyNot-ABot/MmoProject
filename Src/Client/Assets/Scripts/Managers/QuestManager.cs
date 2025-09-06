using Models;
using Services;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public enum NpcQuestStatus
    {
        None = 0,//无任务
        Complete,//拥有已完成可提交的任务
        Available,//拥有可接受任务
        Incompleter,//拥有未完成任务
    }

    //public class QuestManager : Singleton<QuestManager>
    //{
    //    //所有有效任务
    //    public List<NQuestInfo> questInfos;
    //    public Dictionary<int, Quest> allQuests = new Dictionary<int, Quest>();

    //    public Dictionary<int, Dictionary<NpcQuestStatus, List<Quest>>> npcQuests = new Dictionary<int, Dictionary<NpcQuestStatus, List<Quest>>>();

    //    public UnityAction<Quest> onQuestStatusChanged;

    //    public void Init(List<NQuestInfo> quests)
    //    {
    //        this.questInfos = quests;
    //        allQuests.Clear();
    //        this.npcQuests.Clear();
    //        InitQuests();
    //    }

    //    void InitQuests()
    //    {
    //        //初始化已有任务
    //        foreach(var info in this.questInfos)
    //        {
    //            Quest quest = new Quest(info);
    //            this.allQuests[quest.Info.QuestId] = quest;
    //        }

    //        this.CheckAvailableQuests();

    //        foreach(var kv in this.allQuests)
    //        {
    //            this.AddNpcQuest(kv.Value.Define.AcceptNPC,kv.Value);
    //            this.AddNpcQuest(kv.Value.Define.SubmitNPC,kv.Value);
    //        }
    //    }

    //    void CheckAvailableQuests()
    //    {
    //        //初始化可用任务
    //        foreach (var kv in DataManager.Instance.Quests)
    //        {
    //            if (kv.Value.LimitClass != CharacterClass.None && kv.Value.LimitClass != User.Instance.CurrentCharacter.Class)
    //                continue;//不符合职业

    //            if (kv.Value.LimitLevel > User.Instance.CurrentCharacter.Level)
    //                continue;//不符合等级

    //            if (this.allQuests.ContainsKey(kv.Key))
    //                continue;//任务已存在

    //            if (kv.Value.PreQuest > 0)//表示还有前置任务
    //            {
    //                Quest preQuest;
    //                if (this.allQuests.TryGetValue(kv.Value.PreQuest, out preQuest))//获取前置任务
    //                {
    //                    if (preQuest.Info == null)
    //                        continue;//前置任务未接取
    //                    if (preQuest.Info.Status != QuestStatus.Finished)
    //                        continue;//前置任务未完成
    //                }
    //                else
    //                {
    //                    continue;
    //                }
    //            }
    //            Quest quest = new Quest(kv.Value);
    //            this.allQuests[quest.Define.ID] = quest;
    //        }
    //    }

    //    void AddNpcQuest(int npcId,Quest quest)
    //    {
    //        if (!this.npcQuests.ContainsKey(npcId))
    //            this.npcQuests[npcId] = new Dictionary<NpcQuestStatus, List<Quest>>();

    //        List<Quest> availables;
    //        List<Quest> completes;
    //        List<Quest> incompletes;

    //        if (!this.npcQuests[npcId].TryGetValue(NpcQuestStatus.Available,out availables))
    //        {
    //            availables = new List<Quest>();
    //            this.npcQuests[npcId][NpcQuestStatus.Available] = availables;
    //        }
    //        if (!this.npcQuests[npcId].TryGetValue(NpcQuestStatus.Complete, out completes))
    //        {
    //            completes = new List<Quest>();
    //            this.npcQuests[npcId][NpcQuestStatus.Complete] = completes;
    //        }
    //        if (!this.npcQuests[npcId].TryGetValue(NpcQuestStatus.Incompleter, out incompletes))
    //        {
    //            incompletes = new List<Quest>();
    //            this.npcQuests[npcId][NpcQuestStatus.Incompleter] = incompletes;
    //        }

    //        if(quest.Info == null)
    //        {
    //            if(npcId == quest.Define.AcceptNPC && !this.npcQuests[npcId][NpcQuestStatus.Available].Contains(quest))
    //            {
    //                this.npcQuests[npcId][NpcQuestStatus.Available].Add(quest);
    //            }
    //        }
    //        else
    //        {
    //            if(quest.Define.SubmitNPC == npcId && quest.Info.Status == QuestStatus.Complated)
    //            {
    //                if (!this.npcQuests[npcId][NpcQuestStatus.Complete].Contains(quest))
    //                {
    //                    this.npcQuests[npcId][NpcQuestStatus.Complete].Add(quest);
    //                }
    //            }
    //            if (quest.Define.SubmitNPC == npcId && quest.Info.Status == QuestStatus.InProgress)
    //            {
    //                if (!this.npcQuests[npcId][NpcQuestStatus.Incompleter].Contains(quest))
    //                {
    //                    this.npcQuests[npcId][NpcQuestStatus.Incompleter].Add(quest);
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 获取NPC任务状态
    //    /// </summary>
    //    /// <param name="npcId"></param>
    //    /// <returns></returns>
    //    public NpcQuestStatus GetQuestStatusByNpc(int npcId)
    //    {
    //        Dictionary<NpcQuestStatus,List<Quest>> status = new Dictionary<NpcQuestStatus,List<Quest>>();
    //        if(this.npcQuests.TryGetValue(npcId,out status))//获取NPC任务
    //        {
    //            if (status[NpcQuestStatus.Complete].Count > 0)
    //                return NpcQuestStatus.Complete;
    //            if (status[NpcQuestStatus.Available].Count > 0)
    //                return NpcQuestStatus.Available;
    //            if (status[NpcQuestStatus.Incompleter].Count > 0)
    //                return NpcQuestStatus.Incompleter;
    //        }
    //        return NpcQuestStatus.None;
    //    }

    //    public bool OpenNpcQuest(int npcId)
    //    {
    //        Dictionary<NpcQuestStatus, List<Quest>> status = new Dictionary<NpcQuestStatus, List<Quest>>();
    //        if(this.npcQuests.TryGetValue(npcId,out status))
    //        {
    //            if (status[NpcQuestStatus.Complete].Count > 0)
    //                return ShowQuestDialog(status[NpcQuestStatus.Complete].First());
    //            if (status[NpcQuestStatus.Available].Count > 0)
    //                return ShowQuestDialog(status[NpcQuestStatus.Available].First());
    //            if (status[NpcQuestStatus.Incompleter].Count > 0)
    //                return ShowQuestDialog(status[NpcQuestStatus.Incompleter].First());
    //        }
    //        return false;
    //    }

    //    bool ShowQuestDialog(Quest quest)
    //    {
    //        if (quest.Info == null || quest.Info.Status == QuestStatus.Complated)
    //        {
    //            UIQuestDialog dlg = UIManager.Instance.Show<UIQuestDialog>();
    //            dlg.SetQuest(quest);
    //            dlg.OnClose += OnQuestDialogClose;
    //            return true;
    //        }
    //        if(quest.Info != null || quest.Info.Status == QuestStatus.Complated)
    //        {
    //            if (!string.IsNullOrEmpty(quest.Define.DialogIncomplete))
    //                MessageBox.Show(quest.Define.DialogIncomplete);
    //        }
    //        return true;
    //    }

    //    private void OnQuestDialogClose(UIWindow sender, UIWindow.WindowResult result)
    //    {
    //        UIQuestDialog dlg = (UIQuestDialog)sender;
    //        if (result == UIWindow.WindowResult.Yes)
    //        {
    //            if(dlg.quest.Info == null)
    //                QuestService.Instance.SendQuestAccept(dlg.quest);
    //            else if(dlg.quest.Info.Status == QuestStatus.Complated)
    //                QuestService.Instance.SendQuestSubmit(dlg.quest);
    //        }
    //        else if(result == UIWindow.WindowResult.No)
    //        {
    //            MessageBox.Show(dlg.quest.Define.DialogDeny);
    //        }
    //    }

    //    Quest RefreshQuestStatus(NQuestInfo quest)
    //    {
    //        this.npcQuests.Clear();
    //        Quest result;
    //        if (this.allQuests.ContainsKey(quest.QuestId))
    //        {
    //            //更新新的任务状态
    //            this.allQuests[quest.QuestId].Info = quest;
    //            result = this.allQuests[quest.QuestId];
    //        }
    //        else
    //        {
    //            result = new Quest(quest);
    //            this.allQuests[quest.QuestId] = result;
    //        }

    //        CheckAvailableQuests();

    //        foreach (var kv in this.allQuests)
    //        {
    //            this.AddNpcQuest(kv.Value.Define.AcceptNPC, kv.Value);
    //            this.AddNpcQuest(kv.Value.Define.SubmitNPC, kv.Value);
    //        }

    //        if (onQuestStatusChanged != null)
    //            onQuestStatusChanged(result);
    //        return result;
    //    }

    //    public void OnQuestAccepted(NQuestInfo info)
    //    {
    //        var quest = this.RefreshQuestStatus(info);
    //        MessageBox.Show(quest.Define.DialogAccept);
    //    }

    //    internal void OnQuestSubmit(NQuestInfo info)
    //    {
    //        var quest = this.RefreshQuestStatus(info);
    //        MessageBox.Show(quest.Define.DialogFinish);
    //    }
    //}

    public class QuestManager : Singleton<QuestManager>
    {
        public List<NQuestInfo> questInfos = new List<NQuestInfo>(); //所有有效任务信息
        public Dictionary<int, Quest> allQuests = new Dictionary<int, Quest>();//所有任务字典，key为任务ID，value为任务实例
        //NPC任务字典，key为NPC ID，value为该NPC的任务状态字典，NPC任务状态字典的key为任务状态，value为该状态下的任务列表
        public Dictionary<int,Dictionary<NpcQuestStatus, List<Quest>>> npcQuests = new Dictionary<int, Dictionary<NpcQuestStatus, List<Quest>>>();
        public UnityAction<Quest> onQuestStatusChanged;
        public Quest doingQuets;

        public void Init(List<NQuestInfo> quests)//传入任务信息列表
        {
            this.questInfos = quests;
            allQuests.Clear();
            InitQuests();
        }

        private void InitQuests()
        {
            foreach(var info in this.questInfos)
            {
                Quest quest = new Quest(info);
                allQuests[quest.Info.QuestId] = quest;
            }

            this.CheckAvailableQuests();

            //初始化NPC任务
            //foreach(var kv in this.allQuests)
            //{
            //    this.AddNpcQuest(kv.Value.Define.AcceptNPC, kv.Value);
            //    this.AddNpcQuest(kv.Value.Define.SubmitNPC, kv.Value);
            //}
        }

        private void CheckAvailableQuests()
        {
            foreach(var kv in DataManager.Instance.Quests)
            {
                if (kv.Value.LimitClass != CharacterClass.None && kv.Value.LimitClass != User.Instance.CurrentCharacter.Class)//当前任务的限制职业不为空(当前非不限制职业的任务) && 与玩家当前职业不相符，不显示该任务
                    continue;
                if (kv.Value.LimitLevel > User.Instance.CurrentCharacter.Level)//如果玩家等级低于该任务的要求等级，不显示该任务
                    continue;
                if (allQuests.ContainsKey(kv.Key))//任务已存在，不再添加
                    continue;
                if (kv.Value.PreQuest > 0)//如果任务有前置任务
                {
                    Quest preQuest = new Quest();
                    if (this.allQuests.TryGetValue(kv.Value.PreQuest, out preQuest))//获取前置任务
                    {
                        if (preQuest.Info == null) continue; //没有接取前置任务
                        if (preQuest.Info.Status != QuestStatus.Finished) continue;//前置任务还没有完成
                    }
                    else//前置任务不存在，跳过此条任务
                    {
                        continue;
                    }
                }
                Quest quest = new Quest(kv.Value);
                this.allQuests[quest.Define.ID] = quest;
            }
        }

        /// <summary>
        /// 添加NPC任务状态到字典中
        /// </summary>
        /// <param name="npcID">npcID</param>
        /// <param name="quest">要给npc添加的任务</param>
        void AddNpcQuest(int npcID,Quest quest)
        {
            if (!this.npcQuests.ContainsKey(npcID)) //如果NPC任务字典中没有该NPC的任务状态字典，则创建一个新的
                this.npcQuests[npcID] = new Dictionary<NpcQuestStatus, List<Quest>>();

            List<Quest> availables;
            List<Quest> completes;
            List<Quest> incompletes;

            if (!this.npcQuests[npcID].TryGetValue(NpcQuestStatus.Available,out availables))
            {
                availables = new List<Quest>();
                this.npcQuests[npcID][NpcQuestStatus.Available] = availables;
            }
            if (!this.npcQuests[npcID].TryGetValue(NpcQuestStatus.Complete,out completes))
            {
                completes = new List<Quest>();
                this.npcQuests[npcID][NpcQuestStatus.Complete] = completes;
            }
            if (!this.npcQuests[npcID].TryGetValue(NpcQuestStatus.Incompleter,out incompletes))
            {
                incompletes = new List<Quest>();
                this.npcQuests[npcID][NpcQuestStatus.Incompleter] = incompletes;
            }

            if(quest.Info == null) //如果任务信息为空，表示是可接取任务
            {
                if (quest.Define.AcceptNPC == npcID && !npcQuests[npcID][NpcQuestStatus.Available].Contains(quest))
                {
                    npcQuests[npcID][NpcQuestStatus.Available].Add(quest);
                }
            }
            else //如果任务信息不为空，表示是已接取的任务
            {
                if (quest.Define.SubmitNPC == npcID && quest.Info.Status == QuestStatus.Complated)
                {
                    if (!npcQuests[npcID][NpcQuestStatus.Complete].Contains(quest))
                    {
                        npcQuests[npcID][NpcQuestStatus.Complete].Add(quest);
                    }
                }
                if(quest.Define.SubmitNPC == npcID && quest.Info.Status == QuestStatus.InProgress)
                {
                    if (!npcQuests[npcID][NpcQuestStatus.Incompleter].Contains(quest))
                    {
                        npcQuests[npcID][NpcQuestStatus.Incompleter].Add(quest);
                    }
                }
            }
        }

        public NpcQuestStatus GetQuestStatusByNpc(int npcID,Quest quest = null)
        {
            if(quest == null)
            {
                if (doingQuets != null)
                    quest = doingQuets;
                else
                    return NpcQuestStatus.None;
            }
            //检查自己是否有与任务有关的状态(自己是AcceptNPC还是SubmitNpc)
            if (quest.Define.AcceptNPC == npcID)//如果自己是任务接取NPC
            {
                if (quest.Info == null) //如果任务信息为空，表示是可接取任务
                {
                    return NpcQuestStatus.Available; //返回可接取任务状态
                }
            }
            else if (quest.Define.SubmitNPC == npcID) //如果自己是任务提交NPC
            {
                if (quest.Info != null)
                {
                    if (quest.Info.Status == QuestStatus.Complated)
                    {
                        return NpcQuestStatus.Complete; //返回已完成任务状态
                    }
                    else if (quest.Info.Status == QuestStatus.InProgress)
                    {
                        return NpcQuestStatus.Incompleter; //返回未完成任务状态
                    }
                }
            }
            return NpcQuestStatus.None;
        }

        /// <summary>
        /// 根据任务判断该任务当前的接取状态(任务图标应该显示在哪个npc头上)
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        public int GetNpcIdByQuest(Quest quest)
        {
            if (quest.Info == null)//该任务还没有接取
            {
                return quest.Define.AcceptNPC;
                //questStatus = NpcQuestStatus.Available; //任务状态为可接取
            }
            else
            {
                if (quest.Info.Status == QuestStatus.InProgress)//如果任务还在进行中，任务图标显示在接取任务的npc头上
                {
                    return quest.Define.AcceptNPC;
                    //questStatus = NpcQuestStatus.Incompleter; //任务状态为未完成
                }
                else if (quest.Info.Status == QuestStatus.Complated)
                {
                    return quest.Define.SubmitNPC;//任务图标显示在提交任务的npc头上
                    //questStatus = NpcQuestStatus.Complete; //任务状态为已完成
                }
            }
            //questStatus = NpcQuestStatus.None;
            return 0;
        }

        public bool OpenNpcQuest()
        {
            #region 基于npc调用任务框
            //Dictionary<NpcQuestStatus,List<Quest>> status = new Dictionary<NpcQuestStatus, List<Quest>>();
            //if(this.npcQuests.TryGetValue(npcID, out status))
            //{
            //    //如果正在追踪任务，才能打开任务对话框
            //    //if (doingQuets != null && doingQuets.Define.AcceptNPC == npcID)
            //    //{
            //    //    return ShowQuestDialog(doingQuets);
            //    //}
            //    if(doingQuets != null)
            //    {
            //        if (status[NpcQuestStatus.Complete].Count > 0)
            //            return ShowQuestDialog(status[NpcQuestStatus.Complete].First());
            //        if (status[NpcQuestStatus.Available].Count > 0)
            //            return ShowQuestDialog(status[NpcQuestStatus.Available].First());
            //        if (status[NpcQuestStatus.Incompleter].Count > 0)
            //            return ShowQuestDialog(status[NpcQuestStatus.Incompleter].First());
            //        return ShowQuestDialog(doingQuets);
            //    }
            //}
            #endregion

            //基于追踪的任务调用任务框(无需传入npcId)
            if (doingQuets != null)
                return ShowQuestDialog(doingQuets);
            else
                return false;
        }

        private bool ShowQuestDialog(Quest quest)
        {
            if (quest.Info == null || quest.Info.Status == QuestStatus.Complated)
            {
                UIQuestDialog dlc = UIManager.Instance.Show<UIQuestDialog>();
                dlc.SetQuest(quest);
                dlc.OnClose += OnQuestDialogClose;
                return true;
            }
            if (quest.Info != null || quest.Info.Status == QuestStatus.Complated)
            {
                if (!string.IsNullOrEmpty(quest.Define.DialogIncomplete))
                {
                    MessageBox.Show(quest.Define.DialogIncomplete);
                }
            }
            return true;
        }

        private void OnQuestDialogClose(UIWindow sender, UIWindow.WindowResult result)
        {
            UIQuestDialog dlg = (UIQuestDialog)sender;
            if(result == UIWindow.WindowResult.Yes)
            {
                if(dlg.quest.Info == null)
                {
                    QuestService.Instance.SendQuestAccept(dlg.quest);
                }
                else if(dlg.quest.Info.Status == QuestStatus.Complated)
                {
                    QuestService.Instance.SendQuestSubmit(dlg.quest);
                }
            }
            else if(result == UIWindow.WindowResult.No)
            {
                MessageBox.Show(dlg.quest.Define.DialogDeny);
            }
        }

        private Quest RefreshQuestStatus(NQuestInfo quest)
        {
            this.npcQuests.Clear();
            Quest result = new Quest();
            if (this.allQuests.ContainsKey(quest.QuestId))//如果任务已存在，则更新任务信息
            {
                //更新已有任务状态
                this.allQuests[quest.QuestId].Info = quest;
                result = this.allQuests[quest.QuestId];
            }
            else
            {
                //创建新的任务实例
                result = new Quest(quest);
                this.allQuests[quest.QuestId] = result;
            }

            CheckAvailableQuests();

            if(onQuestStatusChanged != null)
                onQuestStatusChanged(result); //触发任务状态改变事件
            return result;
        }

        /// <summary>
        /// 跟踪任务
        /// </summary>
        /// <param name="quest">要跟踪的任务</param>
        /// <param name="follow">开始跟踪还是取消跟踪</param>
        public void FollowQuest(Quest quest,bool follow)
        {
            if (quest == null)
                return;
            int npcId = this.GetNpcIdByQuest(quest);
            NpcController nc = NPCManager.Instance.FindNpc(npcId).GetComponent<NpcController>();
            if (nc != null)
            {
                if(nc.statusMark != null)
                {
                    nc.statusMark.SetActive(follow);
                    if (follow)
                    {
                        doingQuets = quest;
                        if (doingQuets.Info != null)
                            Debug.LogFormat("开始追踪任务ID: {0}，任务名: {1}，当前状态: {2}", doingQuets.Info.QuestId, doingQuets.Define.Name, doingQuets.Info.Status);
                        else
                            Debug.LogFormat("开始追踪任务ID: {0}，任务名: {1}", doingQuets.Define.ID, doingQuets.Define.Name);
                        nc.statusMark.GetComponent<UIQuestStatus>().SetQuestStatus(this.GetQuestStatusByNpc(npcId, quest));
                        this.onQuestStatusChanged(doingQuets);
                    }
                    else
                    {
                        if(doingQuets == null)
                        {
                            MessageBox.Show("还没有正在追踪的任务");
                            return; 
                        } 
                        if (doingQuets.Info != null)
                            Debug.LogFormat("停止追踪任务ID: {0}，任务名: {1}，当前状态: {2}", doingQuets.Info.QuestId,doingQuets.Define.Name,doingQuets.Info.Status);
                        else
                            Debug.LogFormat("停止追踪任务ID: {0}，任务名: {1}", doingQuets.Define.ID, doingQuets.Define.Name);
                        doingQuets = null;
                    }
                }
            }
            else
            {
                Debug.LogError("无法找到对应NPC");
            }
        }

        public void OnQuestAccepted(NQuestInfo info)
        {
            var quest = this.RefreshQuestStatus(info);
            MessageBox.Show(quest.Define.DialogAccept);
        }

        public void OnSubmitQuest(NQuestInfo info)
        {
            var quest = this.RefreshQuestStatus(info);
            MessageBox.Show(quest.Define.DialogFinish);
        }
    }
}

