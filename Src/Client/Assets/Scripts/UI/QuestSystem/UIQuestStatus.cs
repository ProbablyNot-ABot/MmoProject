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
    public Image[] statusImages; // ����״̬��ͼ������
    private NpcQuestStatus questStatus;

    private void Start()
    {
        //this.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��������״̬��ʾ
    /// </summary>
    /// <param name="status">����״̬</param>
    /// <param name="isShown">׷�ٻ��Ƿ���׷��</param>
    /// <param name="quest">Ҫ׷�ٵ�����</param>
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
                // ��������״̬���ö�Ӧ��ͼ��ɼ���
                statusImages[i].gameObject.SetActive(i == (int)status);
            }
        }
    }
}