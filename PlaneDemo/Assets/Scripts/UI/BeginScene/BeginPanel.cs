using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton start;
    public UIButton rank;
    public UIButton setting;
    public UIButton quit;
    public override void Init()
    {
        start.onClick.Add(new EventDelegate(() =>
        {
            ChoosePanel.Instance.ShowMe();
            HideMe();
        }));
        rank.onClick.Add(new EventDelegate(() =>
        {
            RankPanel.Instance.ShowMe();
        }));
        setting.onClick.Add(new EventDelegate(() =>
        {
            SettingPanel.Instance.ShowMe();
        }));
        quit.onClick.Add(new EventDelegate(() =>
        {
            Application.Quit();
        }));
    }
}
