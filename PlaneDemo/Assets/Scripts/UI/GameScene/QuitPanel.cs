using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public UIButton sure;
    public UIButton close;
    public override void Init()
    {
        sure.onClick.Add(new EventDelegate(() => {
            SceneManager.LoadScene("BeginScene");
        }));
        close.onClick.Add(new EventDelegate(() => {
            HideMe();
        }));
        HideMe();
    }
}
