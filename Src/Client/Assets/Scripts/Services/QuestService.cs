using Managers;
using Models;
using Network;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Services
{
    //class QuestService : Singleton<QuestService>, IDisposable
    //{
    //    public QuestService() 
    //    {
    //        MessageDistributer.Instance.Subscribe<QuestAcceptResponse>(this.OnQuestAccept);
    //        MessageDistributer.Instance.Subscribe<QuestSubmitResponse>(this.OnQuestSubmit);
    //    }
    //    public void Dispose()
    //    {
    //        MessageDistributer.Instance.Unsubscribe<QuestAcceptResponse>(this.OnQuestAccept);
    //        MessageDistributer.Instance.Unsubscribe<QuestSubmitResponse>(this.OnQuestSubmit);
    //    }

    //    public bool SendQuestAccept(Quest quest)
    //    {
    //        Debug.Log("SendQuestAccept");
    //        NetMessage message = new NetMessage();
    //        message.Request = new NetMessageRequest();
    //        message.Request.questAccept = new QuestAcceptRequest();
    //        message.Request.questAccept.QuestId = quest.Define.ID;
    //        NetClient.Instance.SendMessage(message);
    //        return true;
    //    }

    //    private void OnQuestAccept(object sender, QuestAcceptResponse message)
    //    {
    //        Debug.LogFormat("OnQuestAccept:{0},ERR{1}", message.Result, message.Errormsg);
    //        if(message.Result == Result.Success)
    //        {
    //            QuestManager.Instance.OnQuestAccepted(message.Quest);
    //            //MessageBox.Show("任务接受成功", "成功", MessageBoxType.Confirm);
    //        }
    //        else
    //        {
    //            MessageBox.Show("任务接受失败", "错误", MessageBoxType.Error);
    //        }
    //    }

    //    public bool SendQuestSubmit(Quest quest)
    //    {
    //        Debug.Log("SendQuestSubmit");
    //        NetMessage message = new NetMessage();
    //        message.Request = new NetMessageRequest();
    //        message.Request.questSubmit = new QuestSubmitRequest();
    //        message.Request.questSubmit.QuestId = quest.Define.ID;
    //        NetClient.Instance.SendMessage(message);
    //        return true;
    //    }
    //    private void OnQuestSubmit(object sender, QuestSubmitResponse message)
    //    {
    //        Debug.LogFormat("OnQuestSubmit:{0},ERR:{1}", message.Result, message.Errormsg);
    //        if (message.Result == Result.Success)
    //        {
    //            Debug.Log("任务完成");
    //            QuestManager.Instance.OnQuestSubmit(message.Quest);
    //            //MessageBox.Show("任务完成成功", "成功", MessageBoxType.Confirm);
    //        }
    //        else
    //        {
    //            MessageBox.Show("任务完成失败", "错误", MessageBoxType.Error);
    //        }
    //    }
    //}

    class QuestService : Singleton<QuestService>,IDisposable
    {
        public QuestService()
        {
            MessageDistributer.Instance.Subscribe<QuestAcceptResponse>(this.OnQuestAccept);
            MessageDistributer.Instance.Subscribe<QuestSubmitResponse>(this.OnQuestSubmit);
        }
        public void Dispose()
        {
            MessageDistributer.Instance.Unsubscribe<QuestAcceptResponse>(this.OnQuestAccept);
            MessageDistributer.Instance.Unsubscribe<QuestSubmitResponse>(this.OnQuestSubmit);
        }

        public bool SendQuestAccept(Quest quest)
        {
            Debug.Log("SendQuestAccept");
            NetMessage message = new NetMessage();
            message.Request = new NetMessageRequest();
            message.Request.questAccept = new QuestAcceptRequest();
            message.Request.questAccept.QuestId = quest.Define.ID;
            NetClient.Instance.SendMessage(message);
            return true;
        }

        private void OnQuestAccept(object sender,QuestAcceptResponse message)
        {
            Debug.LogFormat("OnQuestAccept:{0},ERR{1}", message.Result, message.Errormsg);
            if (message != null)
            {
                if(message.Result == Result.Success)
                {
                    QuestManager.Instance.OnQuestAccepted(message.Quest);
                }
                else
                {
                    MessageBox.Show("任务接取失败","错误",MessageBoxType.Error);
                }
            }
        }

        public bool SendQuestSubmit(Quest quest)
        {
            Debug.Log("SendQuestSubmit");
            NetMessage message = new NetMessage();
            message.Request = new NetMessageRequest();
            message.Request.questSubmit = new QuestSubmitRequest();
            message.Request.questSubmit.QuestId = quest.Define.ID;
            NetClient.Instance.SendMessage(message);
            return true;
        }

        private void OnQuestSubmit(object sender,QuestSubmitResponse message) 
        {
            Debug.LogFormat("OnQuestSubmit:{0},ERR{1}", message.Result, message.Errormsg);
            if (message != null)
            {
                if (message.Result == Result.Success)
                {
                    QuestManager.Instance.OnSubmitQuest(message.Quest);
                }
                else
                {
                    MessageBox.Show("任务提交失败", "错误", MessageBoxType.Error);
                }
            }
        }
    }
}
