using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Managers
{
    class MiniMapManager : Singleton<MiniMapManager>
    {
        public UIMinMap miniMap;

        private Collider minimapBoundingBox;
        public Collider MinimapBoundingBox
        {
            get { return minimapBoundingBox; }
        }
        public Transform PlayerTransform
        {
            get
            {
                if (User.Instance.CurrentCharacterObject == null)
                {
                    return null;
                }
                return User.Instance.CurrentCharacterObject.transform;
            }
        }
        public Sprite LoadCurrentMiniMap()
        {
            return Resloader.Load<Sprite>("UI/Minimap/" + User.Instance.CurrentMapData.MiniMap);
            //return Resloader.Load<Sprite>("UI/Minimap/buluzheng");
        }

        public void UpdateMiniMap(Collider minimapBoundingBox)//地图发生变化的时候调用这个接口
        {
            this.minimapBoundingBox = minimapBoundingBox;
            if(this.miniMap != null)
            {
                this.miniMap.UpdateMap();
            }
            else
            {
                Debug.Log("MiniMap is null");
            }
        }
    }
}