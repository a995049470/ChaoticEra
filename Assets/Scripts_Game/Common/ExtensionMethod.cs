using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class ExtensionMethod 
{
    public static void AddListener(this Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}
