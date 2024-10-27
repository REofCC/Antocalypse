using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    CircleCollider2D circleCollider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Worker"))
        {
            if (collision.gameObject.GetComponent<Worker>().GetCurrentState()==State.Return) //자원 보유 개미와 충돌 시
            {
                Debug.Log("Gather Resource");
                GameManager.Colony.GetResoruce(3);
            }
        }
    }
}
