using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }

    private Dictionary<Type, UIElement> UIReources = new Dictionary<Type, UIElement>();
        
    public UIManager()
    {
        UIReources.Add(typeof(UISetting), new UIElement() { Resources = "UI/UISetting", Cache = true });
        UIReources.Add(typeof(UIBag), new UIElement() { Resources = "UI/UIBag", Cache = false });
        UIReources.Add(typeof(UIShop), new UIElement() { Resources = "UI/UIShop", Cache = false });
        UIReources.Add(typeof(UICharEquip), new UIElement() { Resources = "UI/UICharEquip", Cache = false });
        UIReources.Add(typeof(UIQuestSystem), new UIElement() { Resources = "UI/UIQuestSystem", Cache = false });
        UIReources.Add(typeof(UIFriends), new UIElement() { Resources = "UI/UIFriends", Cache = true });//false
        UIReources.Add(typeof(UIQuestDialog), new UIElement() { Resources = "UI/UIQuestDialog", Cache = false });
        UIReources.Add(typeof(UIGuild), new UIElement() { Resources = "UI/Guild/UIGuild", Cache = false });
        UIReources.Add(typeof(UIGuildList), new UIElement() { Resources = "UI/Guild/UIGuildList", Cache = false });
        UIReources.Add(typeof(UIGuildPopNoGuild), new UIElement() { Resources = "UI/Guild/UIGuildPopNoGuild", Cache = false });
        UIReources.Add(typeof(UIGuildPopCreate), new UIElement() { Resources = "UI/Guild/UIGuildPopCreate", Cache = false });
        UIReources.Add(typeof(UIGuildApplyList), new UIElement() { Resources = "UI/Guild/UIGuildApplyList", Cache = false });
        UIReources.Add(typeof(UIPopCharMenu), new UIElement() { Resources = "UI/UIPopCharMenu", Cache = false });
        UIReources.Add(typeof(UIRide), new UIElement() { Resources = "UI/UIRide", Cache = false });
        UIReources.Add(typeof(UISystemConfig), new UIElement() { Resources = "UI/UISystemConfig", Cache = false });
    }

    public T Show<T>()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Open);
        Type type = typeof(T);
        if (this.UIReources.ContainsKey(type))
        {
            UIElement info = this.UIReources[type];
            if (info.Instance != null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if(prefab == null)
                {
                    return default(T);
                }
                info.Instance = (GameObject)GameObject.Instantiate(prefab);
            }
            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }

    public void Close(Type type)
    {
        //SoundManager.Instance.PlaySound("ui_close");
        if (this.UIReources.ContainsKey(type))
        {
            UIElement info = this.UIReources[type];
            if (info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }

    public void Close<T>()
    {
        this.Close(typeof(T));
    }
}
