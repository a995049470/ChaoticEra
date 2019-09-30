using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameEventType
{
    RefreshUI,
    RefreshViewCenter,//<Vector3Int>
}

public class EventCenter
{
    private static Dictionary<GameEventType, Delegate> m_EventDic = new Dictionary<GameEventType, Delegate>();

    private static bool BeforeAddListener(GameEventType type, Delegate action)
    {
        if (!m_EventDic.ContainsKey(type))
        {
            m_EventDic.Add(type, null);
            return true;
        }
        var d = m_EventDic[type];
        if (d.GetType() != action.GetType())
        {
            return false;
        }
        return true;
    }

    public static void AddListener(GameEventType type, Action action)
    {
        if (!BeforeAddListener(type, action))
        {
            return;
        }
        m_EventDic[type] = (Action)m_EventDic[type] + action;
    }

    public static void AddListener<T1>(GameEventType type, Action<T1> action)
    {
        if (!BeforeAddListener(type, action))
        {
            return;
        }
        m_EventDic[type] = (Action<T1>)m_EventDic[type] + action;
    }

    public static void AddListener<T1, T2>(GameEventType type, Action<T1, T2> action)
    {
        if (!BeforeAddListener(type, action))
        {
            return;
        }
        m_EventDic[type] = (Action<T1, T2>)m_EventDic[type] + action;
    }

    private static bool BeforeRemoveListener(GameEventType type, Delegate action)
    {
        if (!m_EventDic.ContainsKey(type))
        {
            return false;
        }
        var d = m_EventDic[type];
        if (d.GetType() != action.GetType())
        {
            return false;
        }
        return true;
    }
    private static void AfterRemoveListener(GameEventType type)
    {
        if (m_EventDic[type] == null)
        {
            m_EventDic.Remove(type);
        }
    }


    public static void RemoveLister(GameEventType type, Action action)
    {
        if (!BeforeRemoveListener(type, action))
        {
            return;
        }
        m_EventDic[type] = (Action)m_EventDic[type] - action;
        AfterRemoveListener(type);
    }

    public static void RemoveLister<T1>(GameEventType type, Action<T1> action)
    {
        if (!BeforeRemoveListener(type, action))
        {
            return;
        }
        m_EventDic[type] = (Action<T1>)m_EventDic[type] - action;
        AfterRemoveListener(type);
    }

    public static void RemoveLister<T1, T2>(GameEventType type, Action<T1, T2> action)
    {
        if (!BeforeRemoveListener(type, action))
        {
            return;
        }
        m_EventDic[type] = (Action<T1, T2>)m_EventDic[type] - action;
        AfterRemoveListener(type);
    }

    public static void Broadcast(GameEventType type)
    {
        if(!m_EventDic.ContainsKey(type))
        {
            return;
        }
        Action action = m_EventDic[type] as Action;
        action?.Invoke();
    }
    public static void Broadcast<T1>(GameEventType type, T1 arg1)
    {
        if (!m_EventDic.ContainsKey(type))
        {
            return;
        }
        Action<T1> action = m_EventDic[type] as Action<T1>;
        action?.Invoke(arg1);
    }
    public static void Broadcast<T1,T2>(GameEventType type, T1 arg1, T2 arg2)
    {
        if (!m_EventDic.ContainsKey(type))
        {
            return;
        }
        Action<T1, T2> action = m_EventDic[type] as Action<T1, T2>;
        action?.Invoke(arg1, arg2);
    }


}
