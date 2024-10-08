using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField]
    protected NodeData nodeData;
    CircleCollider2D circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Worker"))
        {
            if (collision.gameObject.GetComponent<Worker>().GetCurrentState() == State.Gather) //�ڿ� ���� ���̿� �浹 ��
            {
                Debug.Log("Gather Resource");
                nodeData.value -= collision.gameObject.GetComponent<Worker>().getGatherValue();
            }
        }
    }
}
