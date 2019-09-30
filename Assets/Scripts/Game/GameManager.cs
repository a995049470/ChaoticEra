using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleMonoBehaviour<GameManager> 
{
    private Person m_Player;
    [SerializeField]
    private Transform m_Cmr;
    public Vector3Int ViewCenter { get; private set; }
    private void Start()
    {
        Launch();
    }

    private void Launch()
    {
        WindowManager.Instance.Open<CreateWindow>();
    }

    private void GameLoop()
    {
        RefreshViewPos();
    }

    public void RefreshViewPos()
    {
        if(m_Player == null)
        {
            return;
        }
        if(ViewCenter == m_Player.SelfData.pos)
        {
            return;
        }
        ViewCenter = m_Player.SelfData.pos;
        EventCenter.Broadcast(GameEventType.RefreshViewCenter);
    }


    public void SetPlayer(Person player)
    {
        m_Player = player;
        ViewCenter = player.SelfData.pos;
        m_Cmr.position = ViewCenter;
        EventCenter.Broadcast(GameEventType.RefreshViewCenter);
    }	
}
