using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class BaseObjectPool  
{
    private BaseObject[] m_Objs;
    private BaseObject m_Prefab;
    private int m_id;
    private int m_Index;
    public BaseObjectPool(int _id, int _len, BaseObject _obj)
    {
        m_id = _id;
        m_Objs = new BaseObject[_len];
        m_Prefab = _obj;
        m_Index = -1;
    }
    public BaseObjectPool(int _id, string _typeName, int _len)
    {
        m_id = _id;
        m_Objs = new BaseObject[_len];
        Type type = Type.GetType(_typeName);
        m_Prefab = type.Assembly.CreateInstance(_typeName) as BaseObject;
        m_Index = -1;
    }

    public BaseObject Get()
    {
        BaseObject go = null;
        if(m_Index == -1)
        {
            go = m_Prefab.Clone() as BaseObject;
        }
        else
        {
            go = m_Objs[m_Index];
            m_Index--;
        }
        return go;
    }
    public void Put(BaseObject obj)
    {
        if(m_Index == m_Objs.Length - 1)
        {
            obj.Destroy();
            return;
        }
        m_Index++;
        m_Objs[m_Index] = obj;
    }
}
