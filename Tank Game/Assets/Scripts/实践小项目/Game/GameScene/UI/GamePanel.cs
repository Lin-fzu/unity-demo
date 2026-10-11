using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public CustomGUILabel labScore;
    public CustomGUILabel labTime;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;
    public CustomGUITexture texHp;
    public CustomGUITexture texHpBK;
    public float hpWidth = 500;
    [HideInInspector]
    public int nowScore = 0;
    [HideInInspector]
    public float nowTime = 0;
    private int time;
    void Start()
    {
        btnSetting.clickEvent += () =>
        {
            SettingPanel.Instance.ShowMe();
            Time.timeScale = 0;
        };
        btnQuit.clickEvent += () =>
        {
            QuitPanel.Instance.ShowMe();
            Time.timeScale = 0;
        };
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        time = (int)nowTime;
        labTime.content.text = "";
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + " ±";
        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "∑÷";
        }
        labTime.content.text += time % 60 + "√Î";
    }
    public void AddScore(int score)
    {
        nowScore += score;
        labScore.content.text = nowScore.ToString();
    }
    public void UpdateHp(int maxHp,int HP)
    {
        texHp.guiPos.width = (float)HP / maxHp * hpWidth;
    }
    public void UpdateHpMax(int value)
    {
        hpWidth += value;
        texHpBK.guiPos.width = hpWidth;
    }
}

