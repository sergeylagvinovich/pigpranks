using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovebleObjects
{
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private Vector2 sizeCapsule;
    [SerializeField] private float _timeSpawnBoomb;
    [SerializeField] private float _timeDelaySpawnBomb;
    [SerializeField] private Image boomb;

    private bool _canSpawnBoomb;
    private Animator _aminator;
    private CapsuleCollider2D _capsuleCollider2D;

    private void Start()
    {
        _timeDelaySpawnBomb = _timeSpawnBoomb;
        _aminator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _playerInputs.MakeBoomb += CreateBoomb;
    }

    private void CreateBoomb()
    {
        if (_canSpawnBoomb)
        {
            Boomb newBoomb = Game.instantiate.GetFreeBoomb();
            newBoomb.transform.position = transform.position;
            newBoomb.Init();
            _canSpawnBoomb = false;
            _timeDelaySpawnBomb = _timeSpawnBoomb;
        }
    }

    private void SetAnimation(Vector2 direction)
    {
        _aminator.SetFloat("Move x", direction.x);
        _aminator.SetFloat("Move y", direction.y);
    }

    private void FixedUpdate()
    {
        if (!_canSpawnBoomb)
        {
            if (_timeDelaySpawnBomb <= 0)
            {
                _canSpawnBoomb = true;
            }
            else
            {
                _timeDelaySpawnBomb -= Time.deltaTime;
                boomb.fillAmount = Mathf.Lerp(1, 0, _timeDelaySpawnBomb / _timeSpawnBoomb);
            }
        }
        Vector2 direction = _playerInputs.Direction;
        if (direction != Vector2.zero)
        {
            SetAnimation(direction);
            if (direction.y != 0)
            {
                _capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
                _capsuleCollider2D.size = new Vector2(sizeCapsule.x, sizeCapsule.y);
            }
            if(direction.x != 0)
            {
                _capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
                _capsuleCollider2D.size = new Vector2(sizeCapsule.y, sizeCapsule.x);
            }
            Move(_playerInputs.Direction);
        }
    }

    protected override void Disable()
    {
        Game.instantiate.EndGame(Color.red, "You've lost :(");
    }

}
