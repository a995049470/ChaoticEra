using UnityEngine;

public class Person : BaseObject
{
    public GameObject Obj { get; private set; }
    public PersonData SelfData { get; private set; }
    private Vector3Int m_ViewCenter { get { return GameManager.Instance.ViewCenter; } }
    public bool BIsShow { get; private set; }
    private PersonPart[] m_PersonParts;

    public override void Init(BaseObject _obj)
    {
        PersonData data = _obj as PersonData;
        SelfData = data;
        OnViewPosRefresh();
        EventCenter.AddListener(GameEventType.RefreshViewCenter, OnViewPosRefresh);
        //测试 
        m_PersonParts = new PersonPart[1] { ResManager.Instance.GetBaseObject<PersonPart>(this, 1001) };
    }

    
    public override void UnInit()
    {
        HideSelf();
        for (int i = 0; i < m_PersonParts.Length; i++)
        {
            var part = m_PersonParts[i];
            part.UnInit();
            //MovePart.Put(part);
        }
        m_PersonParts = null;
        EventCenter.RemoveLister(GameEventType.RefreshViewCenter, OnViewPosRefresh);
    }

    private void OnViewPosRefresh()
    {
        var lerp = SelfData.pos - m_ViewCenter;
        bool bIsInView = IsPersonInView(lerp);
        if(bIsInView)
        {
            ShowSelf();
        }
        else
        {
            HideSelf();
        }
    }

    private bool IsPersonInView(Vector3Int lerp)
    {
        bool bIsInView = false;
        if(Mathf.Abs(lerp.x) <= GloblConfig.WIDTH_VIEW &&
           Mathf.Abs(lerp.y) <= GloblConfig.HEIGHT_VIEW &&
           lerp.z == 0)
        {
            bIsInView = true;
        }
        return bIsInView;
    }

    private void ShowSelf()
    {
        if(BIsShow)
        {
            return;
        }
        BIsShow = true;
        Obj = ResManager.Instance.GetGO(1);
        Obj.transform.position = SelfData.pos;
    }

    private void HideSelf()
    {
        if(!BIsShow)
        {
            return;
        }
        BIsShow = false;
        ResManager.Instance.PutGO(Obj);
        Obj = null;
    }

    public override object Clone()
    {
        return new Person();
    }

   
}
