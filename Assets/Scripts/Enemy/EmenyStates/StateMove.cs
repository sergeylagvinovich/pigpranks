using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : States
{

    private List<Vector2> _directions;
    private Vector2 _currentDirection;
    private float _timeToTurn;
    private float _timeDelay;
    private LayerMask _player;
    private LayerMask _objects;
    private float _distanceToObjects;
    private float _distanceToPlayer;
    private float _speed;
    private CapsuleCollider2D _capsuleCollider2D;
    private Transform _self;
    private Animator _animator;
    private Vector2 _sizeCapsule;


    public StateMove(LayerMask player, LayerMask objects, float distanceToObjects, float distanceToPlayer, float speed, CapsuleCollider2D capsuleCollider2D, Transform self, Animator animator, Vector2 sizeCapsule)
    {
        _player = player;
        _objects = objects;
        _distanceToObjects = distanceToObjects;
        _distanceToPlayer = distanceToPlayer;
        _speed = speed;
        _capsuleCollider2D = capsuleCollider2D;
        _self = self;
        _animator = animator;
        _sizeCapsule = sizeCapsule;

        _directions = new List<Vector2>();
        _directions.Add(new Vector2(1, 0));
        _directions.Add(new Vector2(-1, 0));
        _directions.Add(new Vector2(0, 1));
        _directions.Add(new Vector2(0, -1));
        _currentDirection = _directions[Random.Range(0, 4)];
        _timeToTurn = 4f;
        _timeDelay = _timeToTurn;
    }

    public override void Execute()
    {
        if (CheckMove())
        {
            Move();
        }
        else
        {
            Turn();
        }

        if (_timeDelay <= 0)
        {
            Turn();
            _timeDelay = _timeToTurn;
        }
        else
        {
            _timeDelay -= Time.deltaTime;
        }
    }
    
    private bool CheckMove()
    {
        Debug.Log(Physics2D.RaycastAll(_self.position, _currentDirection, _distanceToObjects, _objects).Length);
        if(Physics2D.RaycastAll(_self.position, _currentDirection, _distanceToObjects, _objects).Length>0)
        {
            return false;
        }
        return true;
    }

    private void Turn()
    {
        _currentDirection = _directions[Random.Range(0, 4)];
    }

    private void Move()
    {

        Vector2 direction = new Vector2(_self.position.x, _self.position.y);
        _animator.SetFloat("Move x", _currentDirection.x);
        _animator.SetFloat("Move y", _currentDirection.y);
        if (_currentDirection.x != 0)
        {
            _capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
            _capsuleCollider2D.size = _sizeCapsule;
        }

        if (_currentDirection.y != 0)
        {
            _capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
            _capsuleCollider2D.size = new Vector2(_sizeCapsule.y, _sizeCapsule.x);
        }

        _self.position = Vector2.MoveTowards(_self.position, direction + _currentDirection, _speed * Time.deltaTime);
    }

    public override void Exit()
    {
        
    }
}
