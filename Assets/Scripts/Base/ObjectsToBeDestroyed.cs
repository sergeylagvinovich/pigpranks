using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToBeDestroyed : MonoBehaviour
{
    [SerializeField] protected float _health;


    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <=0)
        {
            Disable();
        }
    }

    protected virtual void Disable()
    {
       
        gameObject.SetActive(false);
       
    }
}
