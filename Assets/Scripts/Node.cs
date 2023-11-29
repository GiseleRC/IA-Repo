using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Node> _neightbours = new List<Node>();
    [SerializeField] private float _neightbourRadius;
    [SerializeField] private LayerMask _enemies;

    private void Awake()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _neightbourRadius);
        foreach (var hitCollider in hitColliders)
        {
            RaycastHit hit;
            var isNode = hitCollider.gameObject.GetComponent<Node>();
            if (isNode != null/* && isNode.gameObject.GetComponent<LayerMask>().value != 7*/)
            {
                Physics.Raycast(transform.position, isNode.transform.position - transform.position, out hit);

                if (hit.transform == isNode.transform)
                {
                    _neightbours.Add(isNode);
                }
            }
        }
    }

    public List<Node> GetNeightbours()
    {
        return _neightbours;
    }
}