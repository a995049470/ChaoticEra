using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static object ParseString(string str, string type)
    {
        object o = null;
        var nStr = str.Trim();
        switch (type)
        {
            case "int":
                o = int.Parse(nStr);
                break;
            case "string":
                o = nStr;
                break;
        }
        return o;
    }
	
}
