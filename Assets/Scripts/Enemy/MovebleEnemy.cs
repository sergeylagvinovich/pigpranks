using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovebleEnemy : ObjectsToBeDestroyed
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected LayerMask _cantMove;
    [SerializeField] protected LayerMask _player;
    [SerializeField] protected float _distanceToObject;
    [SerializeField] protected float _distanceToPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Vector2 _sizeCapsule;
    private States _state;
    private CapsuleCollider2D _capsuleCollider2D;


    public void InitOnStart(float speed, int health)
    {
        _moveSpeed = speed;
        _health = health;
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _state = new StateMove(_player, _cantMove, _distanceToObject, _distanceToPlayer, _moveSpeed, _capsuleCollider2D, transform, _animator, _sizeCapsule);
        
    }

    void Awake()
    {
        InitOnStart(1, 2);
    }

    void Update()
    {
        _state.Execute();
    }

    public bool IsDead()
    {
        return _health<=0;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log('s');
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.TakeDamage(1);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (_health == 1)
        {
            _animator.SetBool("TakeDamage", true);
        }
    }

    protected override void Disable()
    {
        Game.instantiate.CountDogs -= 1;
        base.Disable();
        if (Game.instantiate.CountDogs == 0)
        {
            Game.instantiate.EndGame(Color.green, "You won!!!!");
        }
    }
}
