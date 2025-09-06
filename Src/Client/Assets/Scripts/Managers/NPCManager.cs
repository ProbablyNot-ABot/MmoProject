using Common.Data;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    class NPCManager : Singleton<NPCManager>
    {
        public delegate bool NpcActionHandler(NpcDefine npc);
        Dictionary<NpcFunction,NpcActionHandler> eventMap = new Dictionary<NpcFunction, NpcActionHandler>();
        Dictionary<int,GameObject> npcs = new Dictionary<int,GameObject>();
        //Dictionary<int,Vector3> npcPositions = new Dictionary<int, Vector3>();

        public void RegisterNpcEvent(NpcFunction function, NpcActionHandler action)
        {
            if(!eventMap.ContainsKey(function))
            {
                eventMap[function] = action;
            }
            else
            {
                eventMap[function] += action;
            }
            NpcActionHandler handler = null;
            NpcDefine npc = null;

            if (npc == null)
                return;
            bool result = handler(npc);
        }

        public NpcDefine GetNpcDefine(int npcID)
        {
            NpcDefine npc = null;
            DataManager.Instance.NPCs.TryGetValue(npcID, out npc);
            return npc;
        }

        public bool Interactive(int npcId)
        {
            if (DataManager.Instance.NPCs.ContainsKey(npcId))
            {
                var npc = DataManager.Instance.NPCs[npcId];
                return Interactive(npc);
            }
            return false;
        }

        public bool Interactive(NpcDefine npc)
        {
            if(DoTaskInteractive(npc))
            {
                return true;
            }
            else if(npc.Type == NpcType.Functional)
            {
                return DoFunctionInteractive(npc);
            }
            return false;
        }

        private bool DoTaskInteractive(NpcDefine npc)
        {
            var status = QuestManager.Instance.GetQuestStatusByNpc(npc.ID);
            if (status == NpcQuestStatus.None)
                return false;
            return QuestManager.Instance.OpenNpcQuest();
        }

        private bool DoFunctionInteractive(NpcDefine npc)
        {
            if (npc.Type != NpcType.Functional)
                return false;
            if(!eventMap.ContainsKey(npc.Function))
                return false;
            return eventMap[npc.Function](npc);
        }

        public void AddNpc(int npc, GameObject npcObj)
        {
            if(!this.npcs.ContainsKey(npc))
                this.npcs.Add(npc, npcObj);
        }

        public GameObject FindNpc(int npc)
        {
            if (this.npcs.ContainsKey(npc))
            {
                return this.npcs[npc];
            }
            return null;
        }

        public void UpdateNpcPosition(int npc,Vector3 pos)
        {
            this.npcs[npc].transform.position = pos;
            //this.npcPositions[npc] = pos;
        }

        public Vector3 GetNpcPosition(int npc)
        {
            return this.npcs[npc].transform.position;
            //return this.npcPositions[npc];
        }
    }
}
