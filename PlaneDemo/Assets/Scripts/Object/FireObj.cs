using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_Pos_type
{
    TopLeft,
    Top,
    TopRight,
    Left,
    Right,
    ButtomLeft,
    Buttom,
    ButtomRight,
}
public class FireObj : MonoBehaviour
{
    public E_Pos_type type;
    private FireInfo fireInfo;
    private int nowNum;
    private float nowCD;
    private float nowDelay;
    private BulletInfo nowBulletInfo;
    private float changeAngle;
    private Vector3 nowDir;

    private Vector3 pos;
    private Vector3 initDir;
    
    void Update()
    {
        UpdatePos();
        ResetFireInfo();
        UpdateFire();
    }
    private void UpdatePos()
    {
        pos.z = 250;
        switch (type)
        {
            case E_Pos_type.TopLeft:
                pos.x = 0;
                pos.y=Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_type.Top:
                pos.x = Screen.width / 2;
                pos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_type.TopRight:
                pos.x = Screen.width;
                pos.y = Screen.height;
                initDir = Vector3.left;
                break;
            case E_Pos_type.Left:
                pos.x = 0;
                pos.y = Screen.height / 2;
                initDir = Vector3.right;
                break;
            case E_Pos_type.Right:
                pos.x = Screen.width;
                pos.y = Screen.height / 2;
                initDir = Vector3.left;
                break;
            case E_Pos_type.ButtomLeft:
                pos.x = 0;
                pos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_type.Buttom:
                pos.x = Screen.width / 2;
                pos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_type.ButtomRight:
                pos.x = Screen.width;
                pos.y = 0;
                initDir = Vector3.left;
                break;
        }
        this.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
    private void ResetFireInfo()
    {
        if (nowCD != 0 && nowNum != 0)
        {
            return;
        }
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay > 0)
            {
                return;
            }
        }
        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfoList;
        fireInfo = list[Random.Range(0, list.Count)];
        nowNum = fireInfo.num;
        nowCD = fireInfo.cd;
        nowDelay = fireInfo.delay;
        string[] strs = fireInfo.ids.Split(',');
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        int randomBulletID = Random.Range(beginID, endID + 1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfoList[randomBulletID - 1];
        if (fireInfo.type == 2)
        {
            switch (type)
            {
                case E_Pos_type.TopLeft:
                case E_Pos_type.TopRight:
                case E_Pos_type.ButtomLeft:
                case E_Pos_type.ButtomRight:
                    changeAngle = 90f / (nowNum + 1);
                    break;
                case E_Pos_type.Top:
                case E_Pos_type.Left:
                case E_Pos_type.Right:
                case E_Pos_type.Buttom:
                    changeAngle = 180f / (nowNum + 1);
                    break;
            }
        }

    }
    private void UpdateFire()
    {
        if (nowCD == 0 && nowNum == 0)
        {
            return;
        }
        nowCD -= Time.deltaTime;
        if (nowCD > 0)
        {
            return;
        }
        GameObject bullet;
        BulletObj bulletObj;
        switch (fireInfo.type)
        {
            case 1:
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                bulletObj = bullet.AddComponent<BulletObj>();
                bulletObj.InitInfo(nowBulletInfo);
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObj.Instance.transform.position - this.transform.position);
                --nowNum;
                nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case 2:
                if (nowCD == 0)
                {
                    for(int i = 0; i < nowNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        bulletObj = bullet.AddComponent<BulletObj>();
                        bulletObj.InitInfo(nowBulletInfo);
                        bullet.transform.position = this.transform.position;
                        nowDir = Quaternion.AngleAxis(changeAngle * (i + 1), Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                    nowCD = nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    bulletObj = bullet.AddComponent<BulletObj>();
                    bulletObj.InitInfo(nowBulletInfo);
                    bullet.transform.position = this.transform.position;
                    nowDir = Quaternion.AngleAxis(changeAngle * (fireInfo.num - nowNum + 1), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    --nowNum;
                    nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                }
                    break;
        }
    }
}
