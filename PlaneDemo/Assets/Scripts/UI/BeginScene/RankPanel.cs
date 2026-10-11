using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton close;
    public UIScrollView scrollView;
    public List<RankItem> itemList = new List<RankItem>();
    public override void Init()
    {
        close.onClick.Add(new EventDelegate(() => {
            HideMe();
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        for(int i = 0; i < list.Count; i++)
        {
            if (itemList.Count > i)
            {
                itemList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                obj.transform.SetParent(scrollView.transform, false);
                obj.transform.localPosition = new Vector3(-14, 190 - i * 60, 0);
                RankItem item = obj.GetComponent<RankItem>();
                item.InitInfo(i + 1, list[i].name, list[i].time);
                itemList.Add(item);
            }
        }
    }
}
