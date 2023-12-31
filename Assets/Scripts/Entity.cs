using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _closeNodeRadius;

    public Node closeNode;

    public void CloseNode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _closeNodeRadius);
        float closestNode = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            RaycastHit hit;
            var isNode = hitCollider.gameObject.GetComponent<Node>();
            if (isNode != null)
            {
                Physics.Raycast(transform.position, isNode.transform.position - transform.position, out hit);

                if (hit.transform == isNode.transform)
                {
                    float distance = Vector3.Distance(transform.position, isNode.transform.position);
                    if (distance < closestNode)
                    {
                        closeNode = isNode;
                    }
                }
            }
        }
    }
}