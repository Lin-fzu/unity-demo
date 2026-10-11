using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : BasePanel<EndPanel>
{
    public UIButton sure;
    public UIInput InputName;
    public UILabel time;
    private int endTime;
    public override void Init()
    {
        sure.onClick.Add(new EventDelegate(() => {
            GameDataMgr.Instance.AddRankData(InputName.value, endTime);
            SceneManager.LoadScene("BeginScene");
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        endTime = (int)GamePanel.Instance.nowTime;
        time.text = GamePanel.Instance.time.text;
    }
}
