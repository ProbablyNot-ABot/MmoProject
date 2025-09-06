using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINpcInteractive : MonoBehaviour
{
    private Text text;
    public void OnShow(string npcName)
    {
        text = this.gameObject.GetComponent<Text>();
        text.text = "F | " + npcName;
    }
}
