using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWindow : Window 
{
    [SerializeField] Text[] m_AttValus;
    [SerializeField] Button m_RandomBtn;
    [SerializeField] Button m_CreatBtn;

    private CreateModel m_CreateModel { get { return ModelManager.Instance.Get<CreateModel>(); } }

    protected override void AddListeners()
    {
        base.AddListeners();
        m_RandomBtn.AddListener(OnRandomBtnDonw);
        m_CreatBtn.AddListener(OnCreatBtnDown);
    }
    #region ButtonEvent
    private void OnRandomBtnDonw()
    {
        m_CreateModel.RandomAtt();
    }

    private void OnCreatBtnDown()
    {
        m_CreateModel.CreatPlayer();
    }
    #endregion

    protected override void OnPerOpen()
    {
        base.OnPerOpen();
        EventCenter.AddListener(GameEventType.RefreshUI, RefreshUI);
        RefreshUI();
    }

    protected override void OnPerColse()
    {
        base.OnPerColse();
        EventCenter.RemoveLister(GameEventType.RefreshUI, RefreshUI);
    }

    private void RefreshUI()
    {
        for (int i = 0; i < m_AttValus.Length; i++)
        {
            m_AttValus[i].text = m_CreateModel.data.atts[i].ToString();
        }
    }
}
