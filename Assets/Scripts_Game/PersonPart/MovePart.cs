using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePart : PersonPart 
{
    public override void Init(BaseObject _obj)
    {
        base.Init(_obj);
        EventCenter.AddListener<int, Vector2Int>(GameEventType.PersonMove, Move);
    }
    
    public override void UnInit()
    {
        m_Person = null;
        EventCenter.RemoveLister<int, Vector2Int>(GameEventType.PersonMove, Move);
    }

    private void Move(int _id, Vector2Int _dir)
    {
        if(m_Person.SelfData.id != _id)
        {
            return;
        }
        m_Person.SelfData.pos.x += _dir.x;
        m_Person.SelfData.pos.y += _dir.y;
        if(m_Person.BIsShow)
        {
            m_Person.Obj.transform.position = m_Person.SelfData.pos;
        }
    }

    public override object Clone()
    {
        return new MovePart();
    }
}
