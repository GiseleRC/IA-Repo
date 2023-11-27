using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] List<Node> _neightbours = new List<Node>();
    [SerializeField] float neightbourRadius;
    [SerializeField] LayerMask enemies;

    private void Awake()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neightbourRadius);
        foreach (var hitCollider in hitColliders)
        {
            RaycastHit hit;
            var isNode = hitCollider.gameObject.GetComponent<Node>();
            if (isNode != null)
            {
                Physics.Raycast(transform.position, isNode.transform.position - transform.position, out hit);

                if (hit.transform == isNode.transform)
                {
                    _neightbours.Add(isNode);
                }
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, neightbourRadius);
    //}

    public List<Node> GetNeightbours()
    {
        return _neightbours;
    }
}