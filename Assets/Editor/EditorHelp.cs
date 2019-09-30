using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorHelp : ScriptableWizard
{
    [MenuItem("MyTool/SetGameObjectActive %w")]
    public static void SetGameObjectActive()
    {
        GameObject[] objs = new GameObject[Selection.objects.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Selection.objects[i] as GameObject;
        }
        if (objs == null || objs.Length == 0)
        {
            return;
        }
        bool bisActive = !objs[0].activeSelf;
        foreach (var obj in objs)
        {
            obj.SetActive(bisActive);
        }

    }
	
}
