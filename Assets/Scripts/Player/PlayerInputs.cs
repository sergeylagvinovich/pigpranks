using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private Vector2 _directionMove = Vector2.zero;

    public Vector2 Direction { get { return _directionMove; } }

    public Action MakeBoomb;

    public void SetDirectionY(float y)
    {
        _directionMove = Vector2.zero;
        _directionMove.y = y;
    }

    public void SetDirectionX(float x)
    {
        _directionMove = Vector2.zero;
        _directionMove.x = x;
    }

    public void onClick()
    {
        MakeBoomb?.Invoke();
    }

}
