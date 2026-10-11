using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton left;
    public UIButton right;
    public UIButton start;
    public UIButton close;
    public Transform heroPos;
    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumeObjs;
    private GameObject airplaneObj;
    public override void Init()
    {
        left.onClick.Add(new EventDelegate(() => {
            --GameDataMgr.Instance.nowSelHeroIndex;
            if (GameDataMgr.Instance.nowSelHeroIndex < 0)
            {
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;
            }
            ChangeHero();
        }));
        right.onClick.Add(new EventDelegate(() => {
            ++GameDataMgr.Instance.nowSelHeroIndex;
            if (GameDataMgr.Instance.nowSelHeroIndex >= GameDataMgr.Instance.roleData.roleList.Count)
            {
                GameDataMgr.Instance.nowSelHeroIndex = 0;
            }
            ChangeHero();
        }));
        start.onClick.Add(new EventDelegate(() => {
            SceneManager.LoadScene("GameScene");
        }));
        close.onClick.Add(new EventDelegate(() => {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        HideMe();
    }
    private void ChangeHero()
    {
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();
        DestroyObj();
        airplaneObj = Instantiate(Resources.Load<GameObject>(info.resName));
        airplaneObj.transform.SetParent(heroPos, false);
        airplaneObj.transform.localPosition = Vector3.zero;
        airplaneObj.transform.localRotation = Quaternion.identity;
        airplaneObj.transform.localScale = Vector3.one*info.scale;
        airplaneObj.layer = LayerMask.NameToLayer("UI");
        for(int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < info.hp);
            speedObjs[i].SetActive(i < info.speed);
            volumeObjs[i].SetActive(i < info.volume);
        }
    }
    private void DestroyObj()
    {
        if (airplaneObj != null)
        {
            Destroy(airplaneObj);
            airplaneObj = null;
        }
    }
    private float time = 0;
    private bool isSel = false;
    void Update()
    {
        time += Time.deltaTime;
        heroPos.transform.Translate(Vector3.up * Mathf.Sin(time * 1.5f) * 0.0005f, Space.World);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 1000, 1 << LayerMask.NameToLayer("UI"))){
                isSel = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSel = false;  
        }
        if (Input.GetMouseButton(0) && isSel)
        {
            heroPos.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse X") * 10, Vector3.up);
            heroPos.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * 5, Vector3.right);
        }
    }
    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeHero();
    }
    public override void HideMe()
    {
        base.HideMe();
        DestroyObj();
    }
}
