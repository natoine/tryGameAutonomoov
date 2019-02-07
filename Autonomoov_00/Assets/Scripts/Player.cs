using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    [Range (1,4)] int _id;
    ulong _BodyId;
    bool _IsAssigned = false;
    GameObject _Body;
    

    public void Assign(ulong bodyId)
    {
        if (!_IsAssigned)
        {
            _BodyId = bodyId;
            _Body = GameObject.Find("" + bodyId);
            _IsAssigned = true;
        }
    }
    public void Init(int id)
    {
        _id = id;
        _IsAssigned = false;
    }

    public void Reset()
    {
        _IsAssigned = false;
        _Body = null;
        _BodyId = new ulong();
    }

    public int GetPlayerId()
    {
        return _id;
    }

    public ulong GetBodyId()
    {
        return _BodyId;
    }

    public bool IsAssigned()
    {
        return _IsAssigned;
    }
}
