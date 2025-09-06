using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINameBar : MonoBehaviour
{
    public Text avatarName;

    public Character character;
    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        if (character != null)
        {
            string name = this.character.Name + "Lv." + this.character.Info.Level;
            if (name != avatarName.text)
            {
                avatarName.text = name;
            }
        }

    }
}
