using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{ 
    void Start()
    {
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resName));
        PlayerObj playerObj = obj.AddComponent<PlayerObj>();
        playerObj.speed = info.speed * 20;
        playerObj.maxHp = 10;
        playerObj.nowHp = info.hp;
        playerObj.roundSpeed = 20;
        GamePanel.Instance.ChangeHp(info.hp);
    }
}
