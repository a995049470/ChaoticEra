using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEx : Text 
{
    [SerializeField]
    private bool bIsKey;
    private string m_Key;

    protected override void Awake()
    {
        base.Awake();
        ChangetText();

    }


    private void ChangetText()
    {
        if(!Application.isPlaying)
        {
            return;
        }
        if(!bIsKey)
        {
            return;
        }
        m_Key = this.text.TrimEnd();
        string str = LanguageConfig.Get(m_Key).value;
        this.text = str;
    
    }

    
}
