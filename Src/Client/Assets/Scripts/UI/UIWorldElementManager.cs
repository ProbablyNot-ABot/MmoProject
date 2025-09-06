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


    public void AddCharacterNameBar(Transform owner,Character character)//��ɫ����ʱ����
    {
        GameObject goNameBar = Instantiate(nameBarPrefab,transform);
        goNameBar.name = "NameBar" + character.entityId;
        goNameBar.GetComponent<UIWorldElement>().owner = owner;
        goNameBar.GetComponent<UINameBar>().character = character;
        goNameBar.SetActive(true);
        elementsNames[owner] = goNameBar;
    }
    public void RemoveCharacterNameBar(Transform owner)//��ɫɾ��ʱ����
    {
        if (elementsNames.ContainsKey(owner))
        {
            Destroy(elementsNames[owner]);
            elementsNames.Remove(owner);
        }
    }

    public void AddNpcQuestStatus(Transform owner,NpcQuestStatus status,Quest quest)
    {
        if (this.elementStatus.ContainsKey(owner))//����Ѿ��������NPC������״̬
        {
            elementStatus[owner].GetComponent<UIQuestStatus>().SetQuestStatus(status);//��������״̬
        }
        else//������������NPC������״̬
        {
            GameObject go = Instantiate(npcStatusPrefab, this.transform);//����һ���µ�����״̬UIԪ��
            go.transform.SetParent(this.transform, false);
            go.name = "NpcQuestStatus" + owner.name;
            go.GetComponent<UIWorldElement>().owner = owner;
            go.GetComponent<UIQuestStatus>().SetQuestStatus(status);
            go.SetActive(true);
            this.elementStatus[owner] = go;//״̬��ӵ��ֵ���
        }
    }

    public GameObject GetNpcQuestStatus(Transform owner)//��ȡNPC������״̬UIԪ��
    {
        if (this.elementStatus.ContainsKey(owner))
        {
            return elementStatus[owner];
        }
        return null;
    }

    public void RemoveNpcQuestStatus(Transform owner)//��ɫɾ��ʱ����
    {
        if (elementStatus.ContainsKey(owner))
        {
            Destroy(elementStatus[owner]);
            elementStatus.Remove(owner);
        }
    }

}
