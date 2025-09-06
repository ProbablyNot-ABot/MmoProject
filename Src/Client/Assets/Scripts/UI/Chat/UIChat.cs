using Managers;
using Models;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChat : MonoBehaviour
{
    public TextMeshProUGUI textArea;//����������ʾ����
    public TabView channelTab;
    public InputField chatText;//��������ؼ�
    public Text chatTarget;
    public Dropdown channelSelect;

    // Start is called before the first frame update
    void Start()
    {
        this.channelTab.OnTabSelect += OnDisplayChannelSelected;
        ChatManager.Instance.OnChat += RefreshUI;
    }

    private void OnDestroy()
    {
        ChatManager.Instance.OnChat -= RefreshUI;
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.Instance.IsInputMode = chatText.isFocused;
    }

    void OnDisplayChannelSelected(int idx)
    {
        ChatManager.Instance.displayChannel = (ChatManager.LocalChannel)idx;
        RefreshUI();
    }

    private void RefreshUI()
    {
        Debug.Log("ˢ������");
        this.textArea.text = ChatManager.Instance.GetCurrentMessages();
        this.channelSelect.value = (int)ChatManager.Instance.sendChannel - 1;
        if (ChatManager.Instance.SendChannel == SkillBridge.Message.ChatChannel.Private)
        {
            Debug.Log("˽��Ƶ��");
            this.chatTarget.gameObject.SetActive(true);
            if (ChatManager.Instance.PrivateID != 0)
            {
                this.chatTarget.text = ChatManager.Instance.PrivateName + ":";
            }
            else
                this.chatTarget.text = "<��>:";
        }
        else
        {
            this.chatTarget.gameObject.SetActive(false);
        }
    }

    public void OnClickSend()
    {
        //Debug.Log("��������������ǣ�"+this.chatText.text);
        OnEndInput(this.chatText.text);
    }

    public void OnEndInput(string text)
    {
        text = this.chatText.text;
        //Debug.Log("�յ������������ǣ�" + text);
        if (!string.IsNullOrEmpty(text.Trim()))
            this.SendChat(text);
        this.chatText.text = "";
    }

    void SendChat(string content)
    {
        ChatManager.Instance.SendChat(content,ChatManager.Instance.PrivateID,ChatManager.Instance.PrivateName);
        //RefreshUI();//--------------
    }

    public void OnDropdownValueChanged(int value)
    {
        value = this.channelSelect.value;
        Debug.Log("������ѡ�е��ǵ�:" + (value + 1) + "��");
        if (ChatManager.Instance.sendChannel == (ChatManager.LocalChannel)(value + 1))
        {
            return;
        }
        if (!ChatManager.Instance.SetSendChannel((ChatManager.LocalChannel)value + 1))
        {
            this.channelSelect.value = (int)ChatManager.Instance.sendChannel - 1;
        }
        else
        {
            this.RefreshUI();
        }
    }
}
