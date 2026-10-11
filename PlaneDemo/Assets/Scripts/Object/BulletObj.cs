using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    private BulletInfo info;
    private float time;
    private AudioClip deadSoundClip;
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        Invoke("DelayDestroy", info.lifeTime);
        deadSoundClip = Resources.Load<AudioClip>("DeadEff/111");
    }
    private void DelayDestroy()
    {
        Dead();
    }
    public void Dead()
    {
        if (deadSoundClip != null)
        {
            MusicData music = GameDataMgr.Instance.musicData;
            float vol = music.isSoundOpen ? music.soundValue : 0;
            AudioSource.PlayClipAtPoint(deadSoundClip, transform.position, vol);
        }
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        obj.transform.position = this.transform.position;
        Destroy(obj, 0.5f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerObj obj = other.GetComponent<PlayerObj>();
            obj.Wound();
            Dead();
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);
        switch (info.type)
        {
            case 2:
                time += Time.deltaTime;
                this.transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed);
                break;
            case 3:
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(PlayerObj.Instance.transform.position - this.transform.position), info.roundSpeed * Time.deltaTime);
                break;
        }
    }
}
