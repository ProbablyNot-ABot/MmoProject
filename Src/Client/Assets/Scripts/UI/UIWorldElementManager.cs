using Entities;
using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElementManager : MonoSingleton<UIWorldElementManager>
{
    public GameObject nameBarPrefab;
    public GameObject npcStatusPrefab;

    private Dictionary<Transform,GameObject> elementsNames = new Dictionary<Transform,GameObject>();
    private Dictionary<Transform,GameObject> elementStatus = new Dictionary<Transform,GameObject>();


    public void AddCharacterNameBar(Transform owner,Character character)//角色创建时调用
    {
        GameObject goNameBar = Instantiate(nameBarPrefab,transform);
        goNameBar.name = "NameBar" + character.entityId;
        goNameBar.GetComponent<UIWorldElement>().owner = owner;
        goNameBar.GetComponent<UINameBar>().character = character;
        goNameBar.SetActive(true);
        elementsNames[owner] = goNameBar;
    }
    public void RemoveCharacterNameBar(Transform owner)//角色删除时调用
    {
        if (elementsNames.ContainsKey(owner))
        {
            Destroy(elementsNames[owner]);
            elementsNames.Remove(owner);
        }
    }

    public void AddNpcQuestStatus(Transform owner,NpcQuestStatus status,Quest quest)
    {
        if (this.elementStatus.ContainsKey(owner))//如果已经存在这个NPC的任务状态
        {
            elementStatus[owner].GetComponent<UIQuestStatus>().SetQuestStatus(status);//更新任务状态
        }
        else//如果不存在这个NPC的任务状态
        {
            GameObject go = Instantiate(npcStatusPrefab, this.transform);//创建一个新的任务状态UI元素
            go.transform.SetParent(this.transform, false);
            go.name = "NpcQuestStatus" + owner.name;
            go.GetComponent<UIWorldElement>().owner = owner;
            go.GetComponent<UIQuestStatus>().SetQuestStatus(status);
            go.SetActive(true);
            this.elementStatus[owner] = go;//状态添加到字典中
        }
    }

    public GameObject GetNpcQuestStatus(Transform owner)//获取NPC的任务状态UI元素
    {
        if (this.elementStatus.ContainsKey(owner))
        {
            return elementStatus[owner];
        }
        return null;
    }

    public void RemoveNpcQuestStatus(Transform owner)//角色删除时调用
    {
        if (elementStatus.ContainsKey(owner))
        {
            Destroy(elementStatus[owner]);
            elementStatus.Remove(owner);
        }
    }

}
