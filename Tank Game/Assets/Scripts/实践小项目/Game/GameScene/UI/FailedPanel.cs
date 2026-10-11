using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedPanel : BasePanel<FailedPanel>
{
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnContinue;
    // Start is called before the first frame update
    void Start()
    {
        btnQuit.clickEvent += () => 
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        };
        btnContinue.clickEvent += () =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        };
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
