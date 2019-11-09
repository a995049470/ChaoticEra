using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCreateTool : MonoBehaviour 
{
    private void Update()
    {
        MouseDown();
    }

    private void MouseDown()
    {
        if(!Input.GetMouseButtonDown(0))
        {
            return;
        }
       
    }
	
}
