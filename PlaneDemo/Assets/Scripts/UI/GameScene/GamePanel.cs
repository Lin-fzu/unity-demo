using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton back;
    public UILabel time;
    public List<GameObject> hpObjs;
    public  float nowTime = 0;
    public override void Init()
    {
        back.onClick.Add(new EventDelegate(()=>{
            QuitPanel.Instance.ShowMe();
        }));
    }
    public void ChangeHp(int hp)
    {
        for(int i = 0; i < hpObjs.Count; i++)
        {
            hpObjs[i].SetActive(i < hp);
        }
    }
    private void Update()
    {
        nowTime += Time.deltaTime;
        time.text = "";
        if ((int)nowTime / 3600 > 0)
        {
            time.text +=(int) nowTime / 3600 + "h";
        }
        if ((int)nowTime % 3600 / 60 > 0 || time.text != "")
        {
            time.text += (int)nowTime % 3600 / 60 + "m";
        }
        time.text += (int)nowTime % 60 + "s";
    }

}
