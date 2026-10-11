using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;
    
    public MusicData musicData;
    public RankData rankData;
    public RoleData roleData;
    public BulletData bulletData;
    public FireData fireData;
    public int nowSelHeroIndex = 0;
    private GameDataMgr()
    {
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;
    }
    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }
    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.isMusicOpen = isOpen;
        MusicBK.Instance.SetBKMusicIsOpen(isOpen);
    }
    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        MusicBK.Instance.SetBKMusicValue(value);
    }
    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.isSoundOpen = isOpen;
    }
    public void SetSoundValue(float value)
    {
        musicData.soundValue = value;
    }
    public void AddRankData(string name,int time)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.list.Add(rankInfo);
        rankData.list.Sort((a, b) =>
        {
            if (a.time > b.time)
            {
                return -1;
            }
            return 1;
        });
        if (rankData.list.Count > 20)
        {

            rankData.list.RemoveAt(20);
        }
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
}
