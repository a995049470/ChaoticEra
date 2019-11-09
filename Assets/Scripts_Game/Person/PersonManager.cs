﻿using System.Collections;
using System.Collections.Generic;
public class PersonManager : Single<PersonManager> 
{
    private Dictionary<int, Person> m_PersonDic;
    private int m_CurID;
    public PersonManager()
    {
        m_PersonDic = new Dictionary<int, Person>();
    }

    public Person CreatePerson(PersonData data)
    {
        int id = GetNewID();
        data.id = id;
        Person person = ResManager.Instance.GetBaseObject<Person>(data);
        m_PersonDic.Add(id, person);
        return person;
    }

    public int GetNewID()
    {
        m_CurID++;
        return m_CurID;
    }
}