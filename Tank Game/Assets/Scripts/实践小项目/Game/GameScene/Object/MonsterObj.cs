using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : TankBaseObj
{
    private Transform targetPos;
    public Transform[] randomPos;
    public Transform lookAtTarget;
    public float fireDis = 5;
    public float fireOffsetTime = 1;
    private float nowTime = 0;
    public Transform[] shootPos;
    public GameObject bullet;
    public Texture maxHpBK;
    public Texture hpBK;
    private Rect maxHpRect;
    private Rect nowHpRect;
    private float showTime = 0;
    void Start()
    {
        RandomPos();
    }
    void Update()
    {
        this.transform.LookAt(targetPos);
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if(Vector3.Distance(this.transform.position, targetPos.position) < 0.05f)
        {
            RandomPos();
        }
        if (lookAtTarget != null)
        {
            tankHead.transform.LookAt(lookAtTarget);
            if (Vector3.Distance(this.transform.position, lookAtTarget.transform.position) <= fireDis)
            {
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffsetTime)
                {
                    Fire();
                    nowTime = 0;
                }
            }
        }
    }
    public void RandomPos()
    {
        if (randomPos.Length == 0)
        {
            return;
        }
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
    }
    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            BulletObj bulletObj = obj.GetComponent<BulletObj>();
            bulletObj.SetFather(this);
        }
    }
    public override void Dead()
    {
        base.Dead();
        GamePanel.Instance.AddScore(10);
    }
    private void OnGUI()
    {
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y = Screen.height - screenPos.y;
            maxHpRect.x = screenPos.x - 50;
            maxHpRect.y = screenPos.y - 50;
            maxHpRect.width = 100;
            maxHpRect.height = 20;
            GUI.DrawTexture(maxHpRect, hpBK);
            nowHpRect.x = screenPos.x - 50;
            nowHpRect.y = screenPos.y - 50;
            nowHpRect.width = (float)hp / maxHp * 100f;
            nowHpRect.height = 20;
            GUI.DrawTexture(nowHpRect, maxHpBK);
        }
        
    }
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        showTime = 3;
    }
}
