using Managers;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class UIQuestStatus : MonoBehaviour
//{
//    public Image[] statusImages;
//    private NpcQuestStatus questStatus;

//    public void SetQuestStatus(NpcQuestStatus status)
//    {
//        this.questStatus = status;
//        for (int i = 0; i < 4; i++)
//        {
//            if (this.statusImages[i] != null)
//            {
//                this.statusImages[i].gameObject.SetActive(i == (int)status);
//            }
//        }
//    }

//}

public class UIQuestStatus : MonoBehaviour
{
    public Image[] statusImages; // 任务状态的图标数组
    private NpcQuestStatus questStatus;

    private void Start()
    {
        //this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新任务状态显示
    /// </summary>
    /// <param name="status">任务状态</param>
    /// <param name="isShown">追踪还是放弃追踪</param>
    /// <param name="quest">要追踪的任务</param>
    public void SetQuestStatus(NpcQuestStatus status)//status==None==0
    {
        if (status != NpcQuestStatus.None)
        {
            this.gameObject.SetActive(true);
        }
        for (int i = 0; i < 4; i++)
        {
            if (statusImages[i] != null)
            {
                // 根据任务状态设置对应的图标可见性
                statusImages[i].gameObject.SetActive(i == (int)status);
            }
        }
    }
}