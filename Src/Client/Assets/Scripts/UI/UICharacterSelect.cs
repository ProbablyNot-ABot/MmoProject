using Managers;
using Models;
using Services;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UICharacterSelect : MonoBehaviour
{
    public GameObject panelGreate;
    public GameObject panelSelect;

    public GameObject btnCreateCancel;

    public InputField charName;
    CharacterClass characterClass;

    public Transform uiCharacterList;
    public GameObject uiCharacterInfo;
    
    public List<GameObject> uiCharacters = new List<GameObject>();

    public Image[] titles;
    public Text decs;

    public Text[] names;

    private int selectCharacterIndex = -1;

    public UICharacterView characterView;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.Load();
        UserService.Instance.OnCharacterCreate = OnCharacterCreate;
        InitCharacterSelect(true);
    }

    private void InitCharacterSelect(bool init)
    {
        panelGreate.SetActive(false);
        panelSelect.SetActive(true);   
        if(init)
        {
            foreach (var old in uiCharacters)
            {
                Destroy(old);
            }
            uiCharacters.Clear();

            for(int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
            {
                GameObject go = Instantiate(uiCharacterInfo, uiCharacterList);
                UICharInfo charInfo = go.GetComponent<UICharInfo>();
                charInfo.info = User.Instance.Info.Player.Characters[i];

                Button button = go.GetComponent<Button>();
                int idx = i;
                button.onClick.AddListener(() =>
                {
                    OnSelectCharacter(idx);
                });
                uiCharacters.Add(go);
                go.SetActive(true);
            }
            
        }
    }

    public void InitCharacterCreate()
    {
        panelGreate.SetActive(true);
        panelSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickCreate()
    {
        if (string.IsNullOrEmpty(this.charName.text))
        {
            MessageBox.Show("请输入角色名称");
            return;
        }
        UserService.Instance.SendCharacterCreate(this.charName.text,this.characterClass);//第二个参数是当前选中的是哪个角色
    }
    public void OnSelectedClass(int charClass)
    {
        Debug.Log("点击了角色");
        this.characterClass = (CharacterClass)charClass;
        characterView.CurrectCharacter = charClass - 1;
        for (int i = 0; i < 3; i++)
        {
            titles[i].gameObject.SetActive(i == charClass - 1);
            names[i].text = DataManager.Instance.Characters[i + 1].Name;
        }
        decs.text = DataManager.Instance.Characters[charClass].Description;
    }
    void OnCharacterCreate(Result result,string message)
    {
        if(result == Result.Success)
        {
            InitCharacterSelect(true);
        }
        else
            MessageBox.Show(message,"错误",MessageBoxType.Error);
    }

    public void OnSelectCharacter(int index)
    {
        selectCharacterIndex = index;
        var cha = User.Instance.Info.Player.Characters[index];
        Debug.LogFormat("Select Char:[{0} {1} {2}]", cha.Id, cha.Name, cha.Class);
        User.Instance.CurrentCharacter = cha;
        characterView.CurrectCharacter = ((int)cha.Class - 1);

        for(int i = 0;i < User.Instance.Info.Player.Characters.Count;i++)
        {
            UICharInfo ci = this.uiCharacters[i].GetComponent<UICharInfo>();
            ci.Selected = index == i;
        }
    }
    public void OnClickPlay()
    {
        if(selectCharacterIndex >= 0)
        {
            UserService.Instance.SendGameEnter(selectCharacterIndex);
        }
    }
}
