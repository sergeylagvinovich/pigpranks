using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomb : MonoBehaviour
{

    [SerializeField] private GameObject _expl;
    [SerializeField] private float _timeToExpl;
    [SerializeField] private float _radiousExpl;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject spriteBomb;
    [SerializeField] private GameObject spriteRadious;

    private float _remainingTime;

    private void Start()
    {
        _remainingTime = _timeToExpl;
    }

    public void InitOnCreate(float radious, int damage)
    {
        _radiousExpl = radious;
        _damage = damage;
        spriteRadious.transform.localScale = new Vector2(_radiousExpl, _radiousExpl);
    }


    public float Radious()
    {
        return _radiousExpl;

    }


    public void Init()
    {
        _remainingTime = _timeToExpl;
    }

    // Update is called once per frame
    void Update()
    {
        if (_remainingTime > 0)
        {
            spriteBomb.transform.localScale = new Vector2(Mathf.Lerp(2,1,(_remainingTime/ _timeToExpl)), 1);
            _remainingTime -= Time.deltaTime;
        }
        else
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radiousExpl);
            
            foreach (var item in hits)
            {
                ObjectsToBeDestroyed obj;
                if (item.gameObject.TryGetComponent<ObjectsToBeDestroyed>(out obj))
                {
                    obj.TakeDamage(_damage);
                }
            }

            Explosion expl = Game.instantiate.GetFreeExpl();
            expl.transform.position = transform.position;
            gameObject.SetActive(false);
        }

    }
}
