using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    Dictionary<T, float> _allElements = new Dictionary<T, float>();

    public int count { get { return _allElements.Count; } }

    public void Enqueue(T element, float cost)
    {
        if (!_allElements.ContainsKey(element)) _allElements.Add(element, cost);
        else _allElements[element] = cost;
    }

    public T Dequeue()
    {
        float lowestValue = Mathf.Infinity;
        T element = default;

        foreach (var item in _allElements)
        {
            if (item.Value < lowestValue)
            {
                lowestValue = item.Value;
                element = item.Key;
            }
        }

        _allElements.Remove(element);
        return element;
    }
}