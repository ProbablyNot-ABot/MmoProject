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
    public ListView listMain;//主线任务显示框
    public ListView listBranch;//支线任务显示框
    public TabView tabs;//任务类型切换Tab
    public GameObject itemPrefab;//任务item预设体
    public UIQuestInfo questInfo;//任务信息显示框
    public bool showAvailableList = false; //是否显示可接任务列表

    void Start()
    {
        //当选择任务时，在questInfo上显示任务信息
        listMain.onItemSelected += OnQuestSelected;
        listBranch.onItemSelected += OnQuestSelected;
        tabs.OnTabSelect += OnSelectTab;
        RefreshQuest();
    }

    private void OnQuestSelected(ListView.ListViewItem item)
    {
        if (item == null)//如果没有选择任务，则不做任何操作
            return; 
        if (item.owner == this.listMain)
        {
            if (listBranch.SelectedItem != null)
                this.listBranch.SelectedItem = null; //如果选择的是主线任务，则清空支线任务的选择
        }
        if (item.owner == this.listBranch)
        {
            if(listMain.SelectedItem != null)
                this.listMain.SelectedItem = null; //如果选择的是支线任务，则清空主线任务的选择
        }
        //将选择的任务信息显示在questInfo上
        UIQuestItem questItem = item as UIQuestItem;
        if (questItem != null)
        {
            questInfo.SetQuestInfo(questItem.quest);
        }
    }

    private void OnSelectTab(int idx)//idx是Tab的索引值，0表示进行中的任务，1表示可接取的任务
    {
        showAvailableList = idx == 1; //如果选择的是第二个Tab，则显示可接任务列表
        RefreshQuest();
    }

    void RefreshQuest()
    {
        //清空任务列表
        ClearAllQuestList();
        //初始化所有任务
        InitAllQuestItems();
    }

    private void ClearAllQuestList()
    {
        listMain.RemoveAll();
        listBranch.RemoveAll();
    }
    //接取一个任务后，该任务的Info就不为null了，在哪里修改他的Info呢？在QuestManager中，当接取任务时，会调用QuestManager.Instance.AddQuest(quest)，在这个方法中会修改任务的Info，所以这里不需要再修改了。
    private void InitAllQuestItems()
    {
        foreach(var kv in QuestManager.Instance.allQuests)
        {
            if (showAvailableList)//可接取的任务
            {
                if (kv.Value.Info != null)//已接取的任务不显示
                    continue;
            }
            else//进行中的任务
            {
                if (kv.Value.Info == null)//Info为空表示任务未接取
                    continue;
                if (kv.Value.Info.Status == SkillBridge.Message.QuestStatus.Finished)//已完成的任务不显示
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
