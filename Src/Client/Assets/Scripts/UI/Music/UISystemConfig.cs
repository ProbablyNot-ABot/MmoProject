using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystemConfig : UIWindow
{
    public Image musicOff;
    public Image soundOff;

    public Toggle toggleMusic;
    public Toggle toggleSound;

    public Slider sliderMusic;
    public Slider sliderSound;

    // Start is called before the first frame update
    void Start()
    {
        this.toggleMusic.isOn = Config.MusicOn;
        this.toggleSound.isOn = Config.SoundOn;
        this.sliderMusic.value = Config.MusicVolume;
        this.sliderSound.value = Config.SoundVolume;
    }

    public override void OnYesClick()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        PlayerPrefs.Save();
        Debug.Log("保存声音设置");
        base.OnYesClick();
    }

    public void MusicToogle(bool on)
    {
        on = toggleMusic.isOn;
        musicOff.enabled = !on;
        Config.MusicOn = on;
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
    }

    public void SoundToogle(bool on)
    {
        on = toggleSound.isOn;
        soundOff.enabled = !on;
        Config.SoundOn = on;
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
    }

    public void MusicVolume(float vol)
    {
        vol = sliderMusic.value;
        //Debug.Log("音乐音量是:" + vol);
        Config.MusicVolume = (int)vol;
        PlaySound();
    }

    public void SoundVolume(float vol)
    {
        vol = sliderSound.value;
        //Debug.Log("音效音量是:" + vol);
        Config.SoundVolume = (int)vol;
        PlaySound();
    }

    float lastPlay = 0;
    // Update is called once per frame
    private void PlaySound()
    {
        if (Time.realtimeSinceStartup - lastPlay > 0.1)
        {
            lastPlay = Time.realtimeSinceStartup;
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        }
    }
}
