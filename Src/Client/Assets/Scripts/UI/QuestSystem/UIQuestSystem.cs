using Common.Data;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//public class UIQuestSystem : UIWindow
//{
//    public Text title;

//    public GameObject itemPrefab;

//    public TabView Tabs;
//    public ListView listMain;
//    public ListView listBranch;

//    public UIQuestInfo questInfo;

//    private bool showAvailableList = false;
//    // Start is called before the first frame update
//    void Start()
//    {
//        this.listMain.onItemSelected += this.OnQuestSelected;
//        this.listBranch.onItemSelected += this.OnQuestSelected;
//        this.Tabs.OnTabSelect += OnSelectTab;
//        RefreshUI();
//        //QuestManager.Instance.OnQuestChanged += RefreshUI;
//    }

//    void OnSelectTab(int idx)
//    {
//        showAvailableList = idx == 1;
//        RefreshUI();
//    }

//    private void OnDestroy()
//    {
//        //QuestManager.Instance.OnQuestChanged -= RefreshUI;
//    }

//    void RefreshUI()
//    {
//        ClearAllQuestList();
//        InitAllQuestItems();
//    }

//    void InitAllQuestItems()
//    {
//        foreach(var kv in QuestManager.Instance.allQuests)
//        {
//            if (showAvailableList)
//            {
//                if (kv.Value.Info != null)
//                    continue;
//            }
//            else
//            {
//                if(kv.Value.Info == null)
//                    continue;
//            }

//            GameObject go = Instantiate(itemPrefab, kv.Value.Define.Type == QuestType.Main ? this.listMain.transform : this.listBranch.transform);
//            UIQuestItem ui = go.GetComponent<UIQuestItem>();
//            ui.SetQuestInfo(kv.Value);
//            if (kv.Value.Define.Type == QuestType.Main)
//                this.listMain.AddItem(ui);
//            else
//                this.listBranch.AddItem(ui);
//        }
//    }

//    void ClearAllQuestList()
//    {
//        this.listMain.RemoveAll();
//        this.listBranch.RemoveAll();
//    }

//    public void OnQuestSelected(ListView.ListViewItem item)
//    {
//        UIQuestItem questItem = item as UIQuestItem;
//        this.questInfo.SetQuestInfo(questItem.quest);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

public class UIQuestSystem : UIWindow
{
    public ListView listMain;//����������ʾ��
    public ListView listBranch;//֧��������ʾ��
    public TabView tabs;//���������л�Tab
    public GameObject itemPrefab;//����itemԤ����
    public UIQuestInfo questInfo;//������Ϣ��ʾ��
    public bool showAvailableList = false; //�Ƿ���ʾ�ɽ������б�

    void Start()
    {
        //��ѡ������ʱ����questInfo����ʾ������Ϣ
        listMain.onItemSelected += OnQuestSelected;
        listBranch.onItemSelected += OnQuestSelected;
        tabs.OnTabSelect += OnSelectTab;
        RefreshQuest();
    }

    private void OnQuestSelected(ListView.ListViewItem item)
    {
        if (item == null)//���û��ѡ�����������κβ���
            return; 
        if (item.owner == this.listMain)
        {
            if (listBranch.SelectedItem != null)
                this.listBranch.SelectedItem = null; //���ѡ������������������֧�������ѡ��
        }
        if (item.owner == this.listBranch)
        {
            if(listMain.SelectedItem != null)
                this.listMain.SelectedItem = null; //���ѡ�����֧��������������������ѡ��
        }
        //��ѡ���������Ϣ��ʾ��questInfo��
        UIQuestItem questItem = item as UIQuestItem;
        if (questItem != null)
        {
            questInfo.SetQuestInfo(questItem.quest);
        }
    }

    private void OnSelectTab(int idx)//idx��Tab������ֵ��0��ʾ�����е�����1��ʾ�ɽ�ȡ������
    {
        showAvailableList = idx == 1; //���ѡ����ǵڶ���Tab������ʾ�ɽ������б�
        RefreshQuest();
    }

    void RefreshQuest()
    {
        //��������б�
        ClearAllQuestList();
        //��ʼ����������
        InitAllQuestItems();
    }

    private void ClearAllQuestList()
    {
        listMain.RemoveAll();
        listBranch.RemoveAll();
    }
    //��ȡһ������󣬸������Info�Ͳ�Ϊnull�ˣ��������޸�����Info�أ���QuestManager�У�����ȡ����ʱ�������QuestManager.Instance.AddQuest(quest)������������л��޸������Info���������ﲻ��Ҫ���޸��ˡ�
    private void InitAllQuestItems()
    {
        foreach(var kv in QuestManager.Instance.allQuests)
        {
            if (showAvailableList)//�ɽ�ȡ������
            {
                if (kv.Value.Info != null)//�ѽ�ȡ��������ʾ
                    continue;
            }
            else//�����е�����
            {
                if (kv.Value.Info == null)//InfoΪ�ձ�ʾ����δ��ȡ
                    continue;
                if (kv.Value.Info.Status == SkillBridge.Message.QuestStatus.Finished)//����ɵ�������ʾ
                    continue;
            }
            GameObject go = Instantiate(itemPrefab, kv.Value.Define.Type == QuestType.Main ? this.listMain.transform : this.listBranch.transform);
            UIQuestItem ui = go.GetComponent<UIQuestItem>();
            ui.SetQuestInfo(kv.Value);
            if (kv.Value.Define.Type == QuestType.Main)
                this.listMain.AddItem(ui);
            else
                this.listBranch.AddItem(ui);
        }
    }

}
