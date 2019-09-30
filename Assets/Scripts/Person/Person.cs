using System;
using UnityEngine;

public class Person : ClassPool<Person>
{
    public GameObject Obj { get; private set; }
    public PersonData SelfData { get; private set; }
    private Vector3Int m_ViewCenter { get { return GameManager.Instance.ViewCenter; } }
    private bool bIsShow;
    public void Init(PersonData _data)
    {
        SelfData = _data;
        OnViewPosRefresh();
        EventCenter.AddListener(GameEventType.RefreshViewCenter, OnViewPosRefresh);
    }

    public void UnInit()
    {
        EventCenter.RemoveLister(GameEventType.RefreshViewCenter, OnViewPosRefresh);
    }

    public void OnViewPosRefresh()
    {
        var lerp = SelfData.pos - m_ViewCenter;
        bool bIsInView = IsPersonInView(lerp);
        if(bIsInView && !bIsShow)
        {
            ShowSelf();
        }
        else if(!bIsInView && bIsShow)
        {
            HideSelf();
        }
    }

    public bool IsPersonInView(Vector3Int lerp)
    {
        bool bIsInView = false;
        if(Mathf.Abs(lerp.x) <= GlobalVar.WIDTH_VIEW &&
           Mathf.Abs(lerp.y) <= GlobalVar.HEIGHT_VIEW &&
           lerp.z == 0)
        {
            bIsInView = true;
        }
        return bIsInView;
    }

    public void ShowSelf()
    {
        bIsShow = true;
        Obj = ResManager.Instance.Get(1);
        Obj.transform.position = SelfData.pos;
    }

    public void HideSelf()
    {
        bIsShow = false;
        ResManager.Instance.Put(Obj);
        Obj = null;
    }

    

   
}
