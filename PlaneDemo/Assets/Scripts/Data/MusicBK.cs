using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBK : MonoBehaviour
{
    private static MusicBK instance;
    public static MusicBK Instance => instance;
    private AudioSource BKaudio;
    private void Awake()
    {
        instance = this;
        BKaudio = this.GetComponent<AudioSource>();
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.isMusicOpen);
        SetBKMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }
    public void SetBKMusicIsOpen(bool isOpen)
    {
        BKaudio.mute = !isOpen;
    }
    public void SetBKMusicValue(float value)
    {
        BKaudio.volume = value*0.06f;
    }
}
