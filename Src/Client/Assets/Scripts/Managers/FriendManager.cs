using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    class FriendManager : Singleton<FriendManager>
    {
        //所有有效任务
        public List<NFriendInfo> allFriends;
        public void Init(List<NFriendInfo> friends)
        {
            this.allFriends = friends;
        }
    }
}

