
using UnityEngine;

public class GameObjectPool
{
    private GameObject[] m_Objs;
    private GameObject m_Prefab;
    private int m_id;
    private int m_Index;

    public GameObjectPool(int _id, int _len)
    {
        m_id = _id;
        m_Objs = new GameObject[_len];
        m_Prefab = Resources.Load<GameObject>(PrefabConfig.Get(m_id).path);
        m_Index = -1;
    }

    public GameObject Get()
    {
        GameObject go = null;
        if(m_Index == -1)
        {
            go = Object.Instantiate(m_Prefab);
        }
        else
        {
            go = m_Objs[m_Index];
            m_Index--;
        }
        return go;
    }
    public void Put(GameObject obj)
    {
        if(m_Index == m_Objs.Length - 1)
        {
            Object.Destroy(obj);
            return;
        }
        m_Index++;
        m_Objs[m_Index] = obj;
    }

}