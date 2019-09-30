﻿using UnityEngine;
public class Single<T> : Object where T : new()
{
    private static T m_Instance;
    public static T Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new T();
            }
            return m_Instance;
        }
        
    }
	
}
