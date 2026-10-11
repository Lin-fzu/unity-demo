using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;

    public CustomGUIButton btnClose;
    void Start()
    {
        sliderMusic.changeValue += (value) => GameDataMgr.Instance.ChangeBKValue(value);
        sliderSound.changeValue += (value) => GameDataMgr.Instance.ChangeSoundValue(value);
        togMusic.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseBKMusic(value);
        togSound.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseSound(value);
        btnClose.clickEvent += () =>
        {
            HideMe();
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
        };
        HideMe();
    }

    public void UpdatePanelInfo()
    {
        MusicData data = GameDataMgr.Instance.musicData;
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenBK;
        togSound.isSel = data.isOpenSound;
    }
    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
