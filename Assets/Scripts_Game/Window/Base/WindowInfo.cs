using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindowType
{
    Base,
}

[DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
public class WindowInfo : MonoBehaviour 
{
    private static GameObject m_Root;
    [SerializeField]
    private WindowType m_Type;
    private RectTransform m_Rect;

    [ExecuteInEditMode]
    private void Awake()
    {
        FindParent();
    }

    private void FindParent()
    {
        if(m_Rect == null)
        {
            m_Rect = this.transform.GetComponent<RectTransform>();
        }
        if(m_Root == null)
        {
            m_Root = GameObject.FindGameObjectWithTag("UIRoot");
            if(m_Root == null)
            {
                var go = Resources.Load<GameObject>("UI/Base/UIRoot");
                m_Root = Instantiate(go, Vector3.zero, Quaternion.identity);
                m_Root.name = "UIRoot";
            }
        }
        RectTransform parent = m_Root.transform.Find("UIWindows").GetChild(m_Type.GetHashCode()) as RectTransform;
        if(parent != null)
        {
            this.transform.SetParent(parent);
            m_Rect.sizeDelta = parent.sizeDelta;
            m_Rect.anchoredPosition3D = Vector3.zero;
            m_Rect.anchorMin = parent.anchorMin;
            m_Rect.anchorMax = parent.anchorMax;
            m_Rect.gameObject.layer = parent.gameObject.layer;
        }

    }
    
    
    

}
