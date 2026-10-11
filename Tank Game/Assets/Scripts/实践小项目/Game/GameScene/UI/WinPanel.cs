using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    public CustomGUIButton btnConfirm;
    public CustomGUIInput ipt;
    // Start is called before the first frame update
    void Start()
    {
        btnConfirm.clickEvent += () =>
        {
            Time.timeScale = 1;
            GameDataMgr.Instance.AddRankInfo(ipt.content.text, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
