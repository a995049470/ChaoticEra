using UnityEngine;


public abstract class PersonPart : Object 
{
    private Person m_Person;
    public abstract void Init(Person person);
    public abstract void UnInit();
}
