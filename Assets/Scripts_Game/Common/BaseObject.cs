using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class BaseObject : Object, ICloneable
{
    public BaseObject()
    {
        
    }
    public virtual void Init(BaseObject obj)
    {

    }
	public virtual void UnInit()
    {

    }
    

    public virtual void Destroy()
    {
        
    }

    public virtual object Clone()
    {
        return new BaseObject();
    }

}
