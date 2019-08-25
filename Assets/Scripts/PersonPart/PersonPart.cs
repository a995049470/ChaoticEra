using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersonPart 
{
    private Person m_Person;
    public abstract void Init(Person person);
    public abstract void UnInit();
}
