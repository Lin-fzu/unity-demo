using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton close;
    public UISlider music;
    public UISlider sound;
    public UIToggle musicTog;
    public UIToggle soundTog;
    public override void Init()
    {
        close.onClick.Add(new EventDelegate(() => {
            HideMe();
        }));
        music.onChange.Add(new EventDelegate(() => {
            GameDataMgr.Instance.SetMusicValue(music.value);
        }));
        sound.onChange.Add(new EventDelegate(() => {
            GameDataMgr.Instance.SetSoundValue(sound.value);
        }));
        musicTog.onChange.Add(new EventDelegate(() => {
            GameDataMgr.Instance.SetMusicIsOpen(musicTog.value);
        }));
        soundTog.onChange.Add(new EventDelegate(() => {
            GameDataMgr.Instance.SetSoundIsOpen(soundTog.value);
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        MusicData musicData = GameDataMgr.Instance.musicData;
        music.value = musicData.musicValue;
        musicTog.value = musicData.isMusicOpen;
        sound.value = musicData.soundValue;
        soundTog.value = musicData.isSoundOpen;
    }
    public override void HideMe()
    {
        base.HideMe();
        GameDataMgr.Instance.SaveMusicData();
    }
}
