using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public NodeData nodeData;
    CircleCollider2D circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ant"))
        {
            Debug.Log("Gather Resource");
            nodeData.value -= 3;
        }
    }
}
