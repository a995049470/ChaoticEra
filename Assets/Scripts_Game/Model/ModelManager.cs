using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Single<ModelManager>
{
    private Dictionary<string, Model> m_ModelDic;
    public ModelManager()
    {
        m_ModelDic = new Dictionary<string, Model>();
    }

    public T Get<T>() where T : Model, new()
    {
        T model = null;
        string key = typeof(T).ToString();
        if(!m_ModelDic.ContainsKey(key))
        {
            m_ModelDic.Add(key, new T());
            m_ModelDic[key].Init();
        }
        model = m_ModelDic[key] as T;
        return model;
    }

}
