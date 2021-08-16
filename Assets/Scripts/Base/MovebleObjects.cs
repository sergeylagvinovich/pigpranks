using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovebleObjects : ObjectsToBeDestroyed
{
    [SerializeField] protected float _moveSpeed;

    protected void Move(Vector2 direction)
    {
        direction += new Vector2(transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, direction, _moveSpeed * Time.deltaTime);
    }
}
