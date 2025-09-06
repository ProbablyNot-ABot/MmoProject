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
        None = 0,//������
        Complete,//ӵ������ɿ��ύ������
        Available,//ӵ�пɽ�������
        Incompleter,//ӵ��δ�������
    }

    //public class QuestManager : Singleton<QuestManager>
    //{
    //    //������Ч����
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
    //        //��ʼ����������
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
    //        //��ʼ����������
    //        foreach (var kv in DataManager.Instance.Quests)
    //        {
    //            if (kv.Value.LimitClass != CharacterClass.None && kv.Value.LimitClass != User.Instance.CurrentCharacter.Class)
    //                continue;//������ְҵ

    //            if (kv.Value.LimitLevel > User.Instance.CurrentCharacter.Level)
    //                continue;//�����ϵȼ�

    //            if (this.allQuests.ContainsKey(kv.Key))
    //                continue;//�����Ѵ���

    //            if (kv.Value.PreQuest > 0)//��ʾ����ǰ������
    //            {
    //                Quest preQuest;
    //                if (this.allQuests.TryGetValue(kv.Value.PreQuest, out preQuest))//��ȡǰ������
    //                {
    //                    if (preQuest.Info == null)
    //                        continue;//ǰ������δ��ȡ
    //                    if (preQuest.Info.Status != QuestStatus.Finished)
    //                        continue;//ǰ������δ���
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
    //    /// ��ȡNPC����״̬
    //    /// </summary>
    //    /// <param name="npcId"></param>
    //    /// <returns></returns>
    //    public NpcQuestStatus GetQuestStatusByNpc(int npcId)
    //    {
    //        Dictionary<NpcQuestStatus,List<Quest>> status = new Dictionary<NpcQuestStatus,List<Quest>>();
    //        if(this.npcQuests.TryGetValue(npcId,out status))//��ȡNPC����
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
    //            //�����µ�����״̬
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
        public List<NQuestInfo> questInfos = new List<NQuestInfo>(); //������Ч������Ϣ
        public Dictionary<int, Quest> allQuests = new Dictionary<int, Quest>();//���������ֵ䣬keyΪ����ID��valueΪ����ʵ��
        //NPC�����ֵ䣬keyΪNPC ID��valueΪ��NPC������״̬�ֵ䣬NPC����״̬�ֵ��keyΪ����״̬��valueΪ��״̬�µ������б�
        public Dictionary<int,Dictionary<NpcQuestStatus, List<Quest>>> npcQuests = new Dictionary<int, Dictionary<NpcQuestStatus, List<Quest>>>();
        public UnityAction<Quest> onQuestStatusChanged;
        public Quest doingQuets;

        public void Init(List<NQuestInfo> quests)//����������Ϣ�б�
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

            //��ʼ��NPC����
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
                if (kv.Value.LimitClass != CharacterClass.None && kv.Value.LimitClass != User.Instance.CurrentCharacter.Class)//��ǰ���������ְҵ��Ϊ��(��ǰ�ǲ�����ְҵ������) && ����ҵ�ǰְҵ�����������ʾ������
                    continue;
                if (kv.Value.LimitLevel > User.Instance.CurrentCharacter.Level)//�����ҵȼ����ڸ������Ҫ��ȼ�������ʾ������
                    continue;
                if (allQuests.ContainsKey(kv.Key))//�����Ѵ��ڣ��������
                    continue;
                if (kv.Value.PreQuest > 0)//���������ǰ������
                {
                    Quest preQuest = new Quest();
                    if (this.allQuests.TryGetValue(kv.Value.PreQuest, out preQuest))//��ȡǰ������
                    {
                        if (preQuest.Info == null) continue; //û�н�ȡǰ������
                        if (preQuest.Info.Status != QuestStatus.Finished) continue;//ǰ������û�����
                    }
                    else//ǰ�����񲻴��ڣ�������������
                    {
                        continue;
                    }
                }
                Quest quest = new Quest(kv.Value);
                this.allQuests[quest.Define.ID] = quest;
            }
        }

        /// <summary>
        /// ���NPC����״̬���ֵ���
        /// </summary>
        /// <param name="npcID">npcID</param>
        /// <param name="quest">Ҫ��npc��ӵ�����</param>
        void AddNpcQuest(int npcID,Quest quest)
        {
            if (!this.npcQuests.ContainsKey(npcID)) //���NPC�����ֵ���û�и�NPC������״̬�ֵ䣬�򴴽�һ���µ�
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

            if(quest.Info == null) //���������ϢΪ�գ���ʾ�ǿɽ�ȡ����
            {
                if (quest.Define.AcceptNPC == npcID && !npcQuests[npcID][NpcQuestStatus.Available].Contains(quest))
                {
                    npcQuests[npcID][NpcQuestStatus.Available].Add(quest);
                }
            }
            else //���������Ϣ��Ϊ�գ���ʾ���ѽ�ȡ������
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
            //����Լ��Ƿ����������йص�״̬(�Լ���AcceptNPC����SubmitNpc)
            if (quest.Define.AcceptNPC == npcID)//����Լ��������ȡNPC
            {
                if (quest.Info == null) //���������ϢΪ�գ���ʾ�ǿɽ�ȡ����
                {
                    return NpcQuestStatus.Available; //���ؿɽ�ȡ����״̬
                }
            }
            else if (quest.Define.SubmitNPC == npcID) //����Լ��������ύNPC
            {
                if (quest.Info != null)
                {
                    if (quest.Info.Status == QuestStatus.Complated)
                    {
                        return NpcQuestStatus.Complete; //�������������״̬
                    }
                    else if (quest.Info.Status == QuestStatus.InProgress)
                    {
                        return NpcQuestStatus.Incompleter; //����δ�������״̬
                    }
                }
            }
            return NpcQuestStatus.None;
        }

        /// <summary>
        /// ���������жϸ�����ǰ�Ľ�ȡ״̬(����ͼ��Ӧ����ʾ���ĸ�npcͷ��)
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        public int GetNpcIdByQuest(Quest quest)
        {
            if (quest.Info == null)//������û�н�ȡ
            {
                return quest.Define.AcceptNPC;
                //questStatus = NpcQuestStatus.Available; //����״̬Ϊ�ɽ�ȡ
            }
            else
            {
                if (quest.Info.Status == QuestStatus.InProgress)//��������ڽ����У�����ͼ����ʾ�ڽ�ȡ�����npcͷ��
                {
                    return quest.Define.AcceptNPC;
                    //questStatus = NpcQuestStatus.Incompleter; //����״̬Ϊδ���
                }
                else if (quest.Info.Status == QuestStatus.Complated)
                {
                    return quest.Define.SubmitNPC;//����ͼ����ʾ���ύ�����npcͷ��
                    //questStatus = NpcQuestStatus.Complete; //����״̬Ϊ�����
                }
            }
            //questStatus = NpcQuestStatus.None;
            return 0;
        }

        public bool OpenNpcQuest()
        {
            #region ����npc���������
            //Dictionary<NpcQuestStatus,List<Quest>> status = new Dictionary<NpcQuestStatus, List<Quest>>();
            //if(this.npcQuests.TryGetValue(npcID, out status))
            //{
            //    //�������׷�����񣬲��ܴ�����Ի���
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

            //����׷�ٵ�������������(���贫��npcId)
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
            if (this.allQuests.ContainsKey(quest.QuestId))//��������Ѵ��ڣ������������Ϣ
            {
                //������������״̬
                this.allQuests[quest.QuestId].Info = quest;
                result = this.allQuests[quest.QuestId];
            }
            else
            {
                //�����µ�����ʵ��
                result = new Quest(quest);
                this.allQuests[quest.QuestId] = result;
            }

            CheckAvailableQuests();

            if(onQuestStatusChanged != null)
                onQuestStatusChanged(result); //��������״̬�ı��¼�
            return result;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="quest">Ҫ���ٵ�����</param>
        /// <param name="follow">��ʼ���ٻ���ȡ������</param>
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
                            Debug.LogFormat("��ʼ׷������ID: {0}��������: {1}����ǰ״̬: {2}", doingQuets.Info.QuestId, doingQuets.Define.Name, doingQuets.Info.Status);
                        else
                            Debug.LogFormat("��ʼ׷������ID: {0}��������: {1}", doingQuets.Define.ID, doingQuets.Define.Name);
                        nc.statusMark.GetComponent<UIQuestStatus>().SetQuestStatus(this.GetQuestStatusByNpc(npcId, quest));
                        this.onQuestStatusChanged(doingQuets);
                    }
                    else
                    {
                        if(doingQuets == null)
                        {
                            MessageBox.Show("��û������׷�ٵ�����");
                            return; 
                        } 
                        if (doingQuets.Info != null)
                            Debug.LogFormat("ֹͣ׷������ID: {0}��������: {1}����ǰ״̬: {2}", doingQuets.Info.QuestId,doingQuets.Define.Name,doingQuets.Info.Status);
                        else
                            Debug.LogFormat("ֹͣ׷������ID: {0}��������: {1}", doingQuets.Define.ID, doingQuets.Define.Name);
                        doingQuets = null;
                    }
                }
            }
            else
            {
                Debug.LogError("�޷��ҵ���ӦNPC");
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

