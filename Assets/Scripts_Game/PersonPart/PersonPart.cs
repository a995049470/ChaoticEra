using UnityEngine;


public class PersonPart : BaseObject
{
    protected Person m_Person;
    private static PersonPart[][] m_PesronParts;
    
    public override void Init(BaseObject _obj)
    {
        m_Person = _obj as Person;
    }

    

    
    
   
}
