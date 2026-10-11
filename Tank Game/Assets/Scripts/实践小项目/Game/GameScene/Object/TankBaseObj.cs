using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    public int atk;
    public int def;
    public int maxHp;
    public int hp;
    public Transform tankHead;

    public float moveSpeed = 10;
    public float roundSpeed = 100;
    public float headRoundSpeed = 100;
    
    public GameObject deadEff;

    public abstract void Fire();
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0)
        {
            return;
        }
        this.hp -= dmg;
        if (this.hp <= 0)
        {
            this.hp = 0;
            this.Dead();
        }
    }
    public virtual void Dead()
    {
        Destroy(this.gameObject);
        if (deadEff != null)
        {
            GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            audioSource.Play();
        }
    }
}
