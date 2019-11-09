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

    private void Update()
    {
        GameLoop();
    }

    private void Launch()
    {
        WindowManager.Instance.Open<CreateWindow>();
    }

    private void GameLoop()
    {
        TestMove();
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

    public void TestMove()
    {
        Vector2Int dir = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector2Int.right;
        }
        else
        {
            return;
        }
        EventCenter.Broadcast(GameEventType.PersonMove, m_Player.SelfData.id, dir);
    }

}
