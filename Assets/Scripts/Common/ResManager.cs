using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPool<T> : Single<ClassPool<T>> where T : Object, new()
{
    public T[] ts;
    public int index;
    public ClassPool()
    {
        ts = new T[GlobalVar.MAX_POOL_COUNT];
        index = -1;
    }
    public T Get()
    {
        T value = default(T);
        if(index == -1)
        {
            value = new T();
        }
        else
        {
            value = ts[index];
            index--;
        }
        return value;
    }

    public void Put(T value)
    {
        if(index == ts.Length - 1)
        {
            Destroy(value);
            return;
        }
        index++;
        ts[index] = value;
        
    }


}

public class ResPool<T> : Object where T : Object
{
    public int id;
    public T[] objs;
    public T prefab;
    public int index;

    public ResPool(int _id, int _len)
    {
        id = _id;
        objs = new T[_len];
        prefab = Resources.Load<T>(PrefabConfig.Get(id).path) as T;
        index = -1;
    }

    public T Get()
    {
        T go = null;
        if(index == -1)
        {
            go = Instantiate(prefab);
        }
        else
        {
            go = objs[index];
            index--;
        }
        return go;
    }
    public void Put(T obj)
    {
        if(index == objs.Length - 1)
        {
            Destroy(obj);
            return;
        }
        index++;
        objs[index] = obj;
    }

}

public class ResManager : Single<ResManager>
{
    private const int MAX_COUNT = 5;
    private Dictionary<int, ResPool<GameObject>> m_GameObjectPoolDic;

    public ResManager()
    {
        m_GameObjectPoolDic = new Dictionary<int, ResPool<GameObject>>();
    }
    public GameObject Get(int id)
    {
        GameObject obj = null;
        if(!m_GameObjectPoolDic.ContainsKey(id))
        {
            m_GameObjectPoolDic.Add(id, new ResPool<GameObject>(id, MAX_COUNT));
        }
        obj = m_GameObjectPoolDic[id].Get();
        obj.name = id.ToString();
        obj.SetActive(true);
        return obj;
    }

    public void Put(GameObject obj)
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

	
}
