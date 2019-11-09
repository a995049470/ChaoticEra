using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ResManager : Single<ResManager>
{
    private const int MAX_COUNT = 5;
    private Dictionary<int, GameObjectPool> m_GameObjectPoolDic;
    private Dictionary<int, BaseObjectPool> m_BaseObjectPoolDic;

    public ResManager()
    {
        m_GameObjectPoolDic = new Dictionary<int, GameObjectPool>();       
        InitBaseObjectPoolDic();
    }
    
    private void InitBaseObjectPoolDic()
    {
        m_BaseObjectPoolDic = new Dictionary<int, BaseObjectPool>();
        var keys = PartIDConfig.GetKeys();
        foreach (var key in keys)
        {
            int id = PartIDConfig.Get(key).value;
            BaseObjectPool pool = new BaseObjectPool(id, key, GloblConfig.MAX_POOL_COUNT);
            m_BaseObjectPoolDic[id] = pool;
        }
    }

    public GameObject GetGO(int id)
    {
        GameObject obj = null;
        if(!m_GameObjectPoolDic.ContainsKey(id))
        {
            m_GameObjectPoolDic.Add(id, new GameObjectPool(id, MAX_COUNT));
        }
        obj = m_GameObjectPoolDic[id].Get();
        obj.name = id.ToString();
        obj.SetActive(true);
        return obj;
    }

    public void PutGO(GameObject obj)
    {
        int key = 0;
        if(!int.TryParse(obj.name, out key))
        {
            Destroy(obj);
            return;
        }
        obj.SetActive(false);
        m_GameObjectPoolDic[key].Put(obj);
    }

    public T GetBaseObject<T>(BaseObject _obj) where T : BaseObject, new()
    {
        T value = default(T);
        int key = PartIDConfig.Get(typeof(T).ToString()).value;
        value = m_BaseObjectPoolDic[key].Get() as T;
        value?.Init(_obj);
        return value;
    }

    public T GetBaseObject<T>(BaseObject _obj, int _id) where T : BaseObject, new()
    {
        T value = default(T);
        value = m_BaseObjectPoolDic[_id].Get() as T;
        value?.Init(_obj);
        return value;
    }

    public void PutBaseObject(ref BaseObject _base) 
    {
        _base.UnInit();
        int key = PartIDConfig.Get(_base.GetType().ToString()).value;
        m_BaseObjectPoolDic[key].Put(_base);
        _base = null;
    }

   
}
