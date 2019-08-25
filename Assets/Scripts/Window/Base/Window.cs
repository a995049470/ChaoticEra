using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    private bool m_bIsInit = false;

    protected virtual void BindController()
    {

    }

    protected virtual void AddListeners()
    {

    }

    protected virtual void OnPerOpen()
    {

    }

    protected virtual void OnAfterOpen()
    {

    }

    protected virtual void OnPerColse()
    {

    }

    protected virtual void OnAfterClose()
    {

    }

    public void Open()
    {
        try
        {
            if (!m_bIsInit)
            {
                BindController();
                AddListeners();
            }
            OnPerOpen();
            this.gameObject.SetActive(true);
            OnAfterOpen();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    public void Close()
    {
        try
        {
            OnPerColse();
            this.gameObject.SetActive(false);
            OnAfterClose();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}