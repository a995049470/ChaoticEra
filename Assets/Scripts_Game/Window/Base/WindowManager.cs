using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Single<WindowManager> 
{
    private Dictionary<string, Window> m_WindowDic;
    private const string m_WindowPath = "UI/UIWindows";
    public WindowManager()
    {
        m_WindowDic = new Dictionary<string, Window>();
    }

    public void Open<T>() where T : Window
    {
        T window = null;
        string key = typeof(T).ToString();
        if(!m_WindowDic.ContainsKey(key))
        {
            string path = m_WindowPath + "/" + key;
            var prefab = Resources.Load<GameObject>(path);
            if(prefab != null)
            {
                window = GameObject.Instantiate(prefab).GetComponent<T>();
                m_WindowDic.Add(key, window);
            }
        }
        else
        {
            window = m_WindowDic[key] as T;
        }
        window.Open();
    }

    public void Close<T>() where T : Window
    {
        string key = typeof(T).ToString();
        m_WindowDic[key].Close();
    }
}
