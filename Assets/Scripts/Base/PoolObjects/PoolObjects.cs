using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects<T> where T:MonoBehaviour
{
    private List<T> _pool;
    private bool _autoFill;
    private T _prefab;
    private Transform _parent;

    public bool AutoFill { get => _autoFill; set => _autoFill = value; }

    public PoolObjects(T prefab, int count, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool active = false)
    {
        T obj = GameObject.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(active);
        _pool.Add(obj);
        return obj;
    }

    public bool HasFree(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
                element = item;
                return true;
            }
        }
        element = null;
        return false;
    }

    public List<T> GetAllObjectWithoutActivate()
    {
        return _pool;
    }

    public T GetFreeObject()
    {
        if(HasFree(out T element))
        {
            return element;
        }

        if (_autoFill)
        {
            return CreateObject(true);
        }

        throw new System.Exception($"Нет свободных элементов типа {typeof(T)}");
    }

    public int Count()
    {
        return _pool.Count;
    }

    public T this[int index]
    {
        get { return _pool[index]; }
        set { /* set the specified index to value here */ }
    }

}
