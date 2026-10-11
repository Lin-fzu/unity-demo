using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private static PlayerObj instance;
    public static PlayerObj Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public int nowHp;
    public int maxHp;
    public int speed;
    public int roundSpeed;
    private Quaternion targetQ;
    public bool isDead;
    public void Dead()
    {
        isDead = true;
        EndPanel.Instance.ShowMe();
    }
    public void Wound()
    {
        if (isDead)
        {
            return;
        }
        this.nowHp -= 1;
        GamePanel.Instance.ChangeHp(nowHp);
        if (this.nowHp <= 0)
        {
            this.Dead();
        }
    }
    private float hValue;
    private float vValue;
    private Vector3 nowPos;
    private Vector3 frontPos;
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");
        if (hValue == 0)
        {  
            targetQ = Quaternion.identity;
        }
        else
        {
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        }
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, roundSpeed * Time.deltaTime);
        frontPos = this.transform.position;
        this.transform.Translate(Vector3.forward * vValue * speed * Time.deltaTime);
        this.transform.Translate(Vector3.right * hValue * speed * Time.deltaTime, Space.World);
        nowPos = Camera.main.WorldToScreenPoint(this.transform.position);
        if (nowPos.x <= 50 || nowPos.x >= Screen.width - 50)
        {
            this.transform.position = new Vector3(frontPos.x,this.transform.position.y, this.transform.position.z);
        }
        if (nowPos.y <= 80 || nowPos.y >= Screen.height - 80)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, frontPos.z);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 2000, 1 << LayerMask.NameToLayer("Bullet"))){
                BulletObj obj = hitInfo.transform.GetComponent<BulletObj>();
                obj.Dead();
            }
        }
    }
}
