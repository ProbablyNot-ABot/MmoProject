using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using SkillBridge.Message;
using Managers;

public class UILogin : MonoBehaviour {


    public InputField username;
    public InputField password;
    public Button buttonLogin;
    public Button buttonRegister;

    // Use this for initialization
    void Start () {
        UserService.Instance.OnLogin = OnLogin;
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void OnClickLogin()
    {
        if (string.IsNullOrEmpty(this.username.text))
        {
            MessageBox.Show("�������˺�");
            return;
        }
        if (string.IsNullOrEmpty(this.password.text))
        {
            MessageBox.Show("����������");
            return;
        }
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        // Enter Game
        UserService.Instance.SendLogin(this.username.text,this.password.text);

    }

    void OnLogin(Result result, string message)
    {
        if (result == Result.Success)
        {
            //��¼�ɹ��������ɫѡ��
            //MessageBox.Show("��¼�ɹ���׼����ɫѡ��" + message,"��ʾ", MessageBoxType.Information);
            SceneManager.Instance.LoadScene("CharSelect");
            SoundManager.Instance.PlayMusic(SoundDefine.Music_Select);
        }
        else
            MessageBox.Show(message, "����", MessageBoxType.Error);
    }
}
