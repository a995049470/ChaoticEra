using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPartManager : Single<PersonPartManager>
{
    private Dictionary<string, Stack<PersonPart>> m_PersonPartDic;
    public PersonPartManager()
    {
        this.m_PersonPartDic = new Dictionary<string, Stack<PersonPart>>();
    }

    public T Get<T>(Person person) where T : PersonPart, new()
    {
        T part = null;
        string key = typeof(T).ToString();
        if (!m_PersonPartDic.ContainsKey(key))
        {
            m_PersonPartDic.Add(key, new Stack<PersonPart>());
        }
        var s = m_PersonPartDic[key];
        if(s.Count == 0)
        {
            part = new T();
        }
        else
        {
            part = s.Pop() as T;
        }
        part.Init(person);
        return part;
    }

    public void Push<T>(T part) where T : PersonPart
    {
        string key = typeof(T).ToString();
        if (!m_PersonPartDic.ContainsKey(key))
        {
            m_PersonPartDic.Add(key, new Stack<PersonPart>());
        }
        part.UnInit();
        m_PersonPartDic[key].Push(part);
    }
   


}
