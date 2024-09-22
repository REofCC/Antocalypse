using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField]
    EntityData entityData;
    CapsuleCollider2D collider;

    [SerializeField]
    GameObject cargo;

    Vector2 cargoPos;

    bool isHolding; //자원 보유 중

    
    State state;
    State nextState;
    Vector2 targetPos;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        state = State.Idle;
        isHolding = false;
        cargoPos = cargo.transform.position;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
             case State.Gather:
                GatherResouce();
                break;
        }
    }
    void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                this.state = State.Idle;
                break;
            case State.Gather:
                this.state = State.Gather;
                break;
            case State.Move:
                this.state = State.Move;
                break;
        }
    }
    public void MoveToward(Vector2 target, TaskType type)
    {
        Debug.Log("Start Move");
        switch (type)
        {
            case TaskType.Basic: 
                targetPos = target;
                nextState = State.Idle;
                break;
            case TaskType.Gather: 
                targetPos = target;
                nextState = State.Gather;
                break;
            case TaskType.Build: 
                targetPos = target;
                nextState = State.Build;
                break;
        }

        ChangeState(State.Move);
        targetPos = target;
    }
    void Move()
    {
        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
        {
            Debug.Log("Move Finish");
            ChangeState(nextState);
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, entityData.speed * Time.deltaTime);
    }
    void Idle()
    {
        // 유휴 행동
    }

    void GatherResouce()
    {
        Debug.Log("Gathering Resouces");
        //자원 수집
        entityData.isHolding = true;
        entityData.holdValue = entityData.gatherValue;
        //자원 수집 애니메이션 및 딜레이 추가

        targetPos = cargoPos;
        Debug.Log("Returning to Cargo");
        nextState = State.Idle;
        ChangeState(State.Move);
    }

    public State GetCurrentState()
    {
        return state;
    }
}
