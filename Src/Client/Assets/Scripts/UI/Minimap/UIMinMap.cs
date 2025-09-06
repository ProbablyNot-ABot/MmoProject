using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMinMap : MonoBehaviour
{
    public Collider miniMapBoudingBox;
    public Image miniMap;
    public Image arrow;
    public Text mapName;

    private Transform playerTransform;

	// Use this for initialization
	void Start () {

        MiniMapManager.Instance.miniMap = this;
        this.UpdateMap();
    }

    public void UpdateMap()
    {
        this.mapName.text = User.Instance.CurrentMapData.Name;
        this.miniMap.overrideSprite = MiniMapManager.Instance.LoadCurrentMiniMap();
        //Debug.Log(MiniMapManager.Instance.LoadCurrentMiniMap().ToString());
        this.miniMap.SetNativeSize();
        this.miniMap.transform.localPosition = Vector3.zero;
        this.miniMapBoudingBox = MiniMapManager.Instance.MinimapBoundingBox;
        this.playerTransform = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (playerTransform == null && User.Instance.CurrentCharacterObject != null)
        {
            this.playerTransform = User.Instance.CurrentCharacterObject.transform;
        }*/
        if(playerTransform == null)
        {
            playerTransform = MiniMapManager.Instance.PlayerTransform;
        }
        if (miniMapBoudingBox == null || playerTransform == null) return;

        float realWidth = miniMapBoudingBox.bounds.size.x;
        float realHeight = miniMapBoudingBox.bounds.size.z;

        float relaX = playerTransform.position.x - miniMapBoudingBox.bounds.min.x;//相对位置
        float relaY = playerTransform.position.z - miniMapBoudingBox.bounds.min.z;

        float pivotX = relaX / realWidth;
        float pivotY = relaY / realHeight;

        this.miniMap.rectTransform.pivot = new Vector2(pivotX, pivotY);
        this.miniMap.rectTransform.localPosition = Vector2.zero;
        this.arrow.transform.eulerAngles = new Vector3(0,0,-playerTransform.eulerAngles.y);//将角色的y轴使小箭头的z轴旋转
    }

}
