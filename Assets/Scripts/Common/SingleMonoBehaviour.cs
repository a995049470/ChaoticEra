using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static GameObject m_Root;
    private static T m_Instance;
    public static T Instance
    {
        get
        {           
            if(m_Instance == null)
            {
                m_Instance = m_Root.GetComponent<T>();
                if(m_Instance == null)
                {
                    m_Instance = m_Root.AddComponent<T>();
                }
                
            }
            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_Root == null)
        {
            m_Root = this.gameObject;
            DontDestroyOnLoad(m_Root);
        }
    }
	
}
