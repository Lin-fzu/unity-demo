using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public int moveSpeed = 100;
    public TankBaseObj fatherObj;
    public GameObject effObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube") ||
            other.CompareTag("Player")&&fatherObj.CompareTag("Monster") ||
            other.CompareTag("Monster") && fatherObj.CompareTag("Player"))
        {
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            if (obj != null)
            {
                obj.Wound(fatherObj);
            }
            if (effObj != null)
            {
                GameObject eff = Instantiate(effObj, this.transform.position, this.transform.rotation);
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }
            Destroy(this.gameObject);
        }
    }
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
