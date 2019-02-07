using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager instance;
    Player[] _Players;
    [SerializeField]
    Color[] _PlayerColor;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            _Players = new Player[GameParameters.instance.GetPlayerCount()];
            for (int i = 0; i < _Players.Length; i++)
            {
                _Players[i] = new Player();
            }
            InitPlayers();
        }
    }

    public void Update()
    {
        //if (GameParameters.instance.GetPlayerCount())
        {

        }
    }

    public void InitPlayers()
    {
        for (int i = 0; i < _Players.Length; i++)
        {
            _Players[i].Init(i);
        }
    }

    public void AddPlayer(ulong id)
    {
        if (CanAddPlayer(id))
        {
            int first = FirstAvailableId();
            _Players[first].Assign(id);
            Debug.Log("Player " + first + " " +_Players[first].GetBodyId());
        }
        else Debug.Log("Can't add player");
    }

    public void ResetPlayer(ulong id)
    {
        GetPlayer(id).Reset();
    }

    bool CanAddPlayer(ulong id)
    {
        for (int i = 0; i < _Players.Length; i++)
        {
            if (_Players[i].IsAssigned() && _Players[i].GetBodyId() == id)
            {
                Debug.Log("Already assigned or id already taken");
                return false;
            }
        }        
        foreach(Player p in _Players)
        {
            if (!p.IsAssigned()) return true;
        }
        Debug.Log("All bodies are assigned");
        return false;
    }

    int FirstAvailableId()
    {
        foreach(Player p in _Players)
        {
            if (!p.IsAssigned()) return p.GetPlayerId();
        }
        return -1;
    }

    public Player [] GetPlayers()
    {
        return _Players;
    }

    Player GetPlayer(int id)
    {
        return _Players[id];
    }

    Player GetPlayer(ulong bodyId)
    {
        foreach(Player p in _Players)
        {
            if(bodyId == p.GetBodyId())
            {
                return p;
            }
        }
        return null;
    }

    public Color GetPlayerColor(int id)
    {
        return _PlayerColor[id];
    }
}
