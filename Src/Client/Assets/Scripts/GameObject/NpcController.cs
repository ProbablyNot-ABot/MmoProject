using Common.Data;
using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public int npcID;
    public GameObject statusMark;

    //SkinnedMeshRenderer renderer;
    Animator anim;
    NpcDefine npc;
    Color orignColor;

    private bool inInteractive = false;
    private bool canF = false;

    NpcQuestStatus questStatus;

    // Start is called before the first frame update
    void Start()
    {
        //renderer = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();  
        anim = this.gameObject.GetComponent<Animator>();
        //orignColor = renderer.sharedMaterial.color;
        npc = NPCManager.Instance.GetNpcDefine(npcID);
        NPCManager.Instance.AddNpc(npcID, this.gameObject);
        NPCManager.Instance.UpdateNpcPosition(npcID, this.transform.position);
        this.StartCoroutine(Action());
        //RefreshNpcStatus();
        QuestManager.Instance.onQuestStatusChanged += OnQuestStatusChanged;
    }

    void OnQuestStatusChanged(Quest quest)
    {
        this.RefreshNpcStatus(quest);
    }
    
    void RefreshNpcStatus(Quest quest = null)
    {
        //Debug.Log("目前的NPC的ID是：" + this.npcID);
        questStatus = QuestManager.Instance.GetQuestStatusByNpc(this.npcID,quest);//检查自己跟当前任务是什么关系
        //检查自己是不是跟当前任务有关的NPC
         this.statusMark.GetComponent<UIQuestStatus>().SetQuestStatus(questStatus);
        //开始跟踪任务
        //UIWorldElementManager.Instance.AddNpcQuestStatus(this.transform,questStatus,quest);
    }

    private void OnDestroy()
    {
        QuestManager.Instance.onQuestStatusChanged -= OnQuestStatusChanged;
        //if (UIWorldElementManager.Instance != null )
        //    UIWorldElementManager.Instance.RemoveNpcQuestStatus(this.transform);
    }

    IEnumerator Action()
    {
        while (true)
        {
            if (inInteractive)
            {
                yield return new WaitForSeconds(2.0f);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(5f, 10f));
            }
            this.Relax();
        }
    }

    private void Relax()
    {
        anim.SetTrigger("Relax");
    }

    private void Interactive()
     {
        if(!inInteractive)
        {
            inInteractive = true;
            StartCoroutine(DoInteractive());
        }
    }

    IEnumerator DoInteractive()
    {
        yield return FaceToPlayer();
        if (NPCManager.Instance.Interactive(npc))
        {
            anim.SetTrigger("Talk");
        }
        yield return new WaitForSeconds(3.0f);
        inInteractive = false;
    }

    IEnumerator FaceToPlayer()
    {
        Vector3 faceTo = (User.Instance.CurrentCharacterObject.transform.position - this.transform.position).normalized;
        while (Mathf.Abs(Vector3.Angle(this.gameObject.transform.forward, faceTo)) > 5)
        {
            this.gameObject.transform.forward = Vector3.Lerp(this.gameObject.transform.forward, faceTo, Time.deltaTime * 5);
            yield return null;
        }
    }

    private void Update()
    {
        if (canF)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interactive();
                UIMain.Instance.OnHidenNpcInteractive();
                canF = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canF = true;
        UIMain.Instance.OnShowNpcInteractive(this.npc.Name);
    }

    private void OnTriggerExit(Collider other)
    {
        canF = false;
        UIMain.Instance.OnHidenNpcInteractive();
    }

    private void OnMouseDown()
    {
        if(Vector3.Distance(this.transform.position,User.Instance.CurrentCharacterObject.transform.position) > 2f)
            User.Instance.CurrentCharacterObject.StartNav(this.transform.position);
        //Interactive();
    }
    private void OnMouseOver()
    {
        //HightLight(true);
    }
    private void OnMouseEnter()
    {
        //HightLight(true);
    }
    private void OnMouseExit()
    {
        //HightLight(false);
    }

    /*void HightLight(bool highLight)
{
   if(highLight)
   {
       if(renderer.sharedMaterial.color != Color.white)
          renderer.sharedMaterial.color = Color.white;
   }
   else
   {
       if(renderer.sharedMaterial.color != orignColor)
           renderer.sharedMaterial.color = orignColor;
   }
}*/
}
