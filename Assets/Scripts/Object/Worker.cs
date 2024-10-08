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
    Vector2 nodePos;

    bool isHolding; //자원 보유 중

    TaskType currentTask;
    State state;
    State nextState;
    Vector2 targetPos;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        state = State.Idle;
        isHolding = false;
        cargoPos = cargo.transform.position;
        currentTask = TaskType.None;
    }

    private void FixedUpdate()
    {
        if (FoodCheck())    //배고픔 체크
        {
            ChangeState(State.Eat);
        }
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
            case State.Eat:
                this.state = State.Eat;
                break;
            case State.Build:
                this.state = State.Build;
                break;
        }
    }
    void Move()
    {
        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
        {
            Debug.Log("Move Finish");
            ChangeState(State.Idle);
            MoveEnd();
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, entityData.speed * Time.deltaTime);
    }
    void MoveEnd()
    {
        switch(currentTask)
        {
            case TaskType.None:
                ChangeState(State.Idle);
                break;
            case TaskType.Gather:
                if (transform.position.x == cargoPos.x && transform.position.y == cargoPos.y)   // 저장소에 도착 시 
                {
                    ColonyManager.Instance.GetResoruce(entityData.holdValue);   // 보유중인 자원만큼 저장소 자원 추가
                    targetPos = nodePos;
                    ChangeState(State.Move);
                }
                else if (transform.position.x == nodePos.x && transform.position.y == nodePos.y)    // 자원 노드에 도착 시
                {
                        targetPos = nodePos;
                        ChangeState(State.Gather);  // 수집 상태로 변경
                }
                break;
            case TaskType.Build:
                ChangeState(State.Idle);
                break;
            case TaskType.Eat:
                ChangeState(State.Idle);
                break;
        }
    }
    public void GetTask(Vector2 target, TaskType type)
    {
        Debug.Log("Task Confirmed");
        switch (type)
        {
            case TaskType.None:
                targetPos = target;
                currentTask = type;
                //nextState = State.Idle;
                break;
            case TaskType.Gather:
                nodePos = target;
                currentTask = type;
                //nextState = State.Gather;
                break;
            case TaskType.Build:
                targetPos = target;
                currentTask = type;
                //nextState = State.Build;
                break;
        }
        if (state != State.Eat)
        {
            nodePos = target;
            targetPos = nodePos;
            ChangeState(State.Move);
        }
    }
    void Idle()
    {
        // 유휴 행동
    }

    bool FoodCheck()
    {
        if (entityData.kcal < 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GatherResouce()
    {
        Debug.Log("Gathering Resouces");
        //자원 수집
        entityData.isHolding = true;
        entityData.holdValue = entityData.gatherValue;  //자원 수집량만큼 보유
        //자원 수집 애니메이션 및 딜레이 추가

        targetPos = cargoPos;
        Debug.Log("Returning to Cargo");
        ChangeState(State.Move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cargo"))
        {
            Debug.Log("arrived at Cargo");
        }
        else if(collision.CompareTag("ResourceNode"))
        {
            Debug.Log("arrived at ResourceNode");
        }
    }
    public State GetCurrentState()
    {
        return state;
    }
    public TaskType GetCurrentTask()
    {
        return currentTask;
    }
    public bool IsHolding()
    {
        return isHolding;
    }
    public int getGatherValue()
    {
        return entityData.gatherValue;
    }
}
