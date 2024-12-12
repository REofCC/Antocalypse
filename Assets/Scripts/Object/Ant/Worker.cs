using System.Collections;
using UnityEngine;

public class Worker : MonoBehaviour
{

    private BTSelector root;

    EntityData entityData;
    CapsuleCollider2D collider;

    [SerializeField]
    GameObject cargo;

    Vector2 cargoPos;
    Vector2 nodePos;

    TaskType currentTask;
    State state;
    //State nextState;
    Vector2 targetPos;

    public float buildTime = 2f;    //임시 건설시간
    // ToDo : 건설 명령 받을 때 건설시간 받아야 함
    private void Awake()
    {
        entityData = GetComponent<EntityData>();
        collider = GetComponent<CapsuleCollider2D>();
        state = State.Idle;
        cargoPos = cargo.transform.position;
        currentTask = TaskType.None;
    }
    private void Start()
    {
        root = new BTSelector();
        BTSelector orderSelector = new BTSelector();

        BTSequence eatSequence = new BTSequence();
        BTSequence returnSequence = new BTSequence();
        BTSequence gatherSequence = new BTSequence();
        BTSequence buildSequence = new BTSequence();

        BTAction eatAction = new BTAction(Eat);
        BTAction moveAction = new BTAction(Move);
        BTAction storeAction = new BTAction(Store);
        BTAction gatherAction = new BTAction(GatherResource);
        BTAction buildAction = new BTAction(Build);
        BTAction idleAction = new BTAction(Idle);

        BTCondition kcalLow = new BTCondition(IsKcalLow);
        BTCondition isHolding = new BTCondition(IsHolding);
        BTCondition isGatherOrder = new BTCondition(IsGatherOrder);
        BTCondition isBuildOrder = new BTCondition(IsBuildOrder);


        root.AddChild(eatSequence);
        root.AddChild(orderSelector);
        root.AddChild(idleAction);

        eatSequence.AddChild(kcalLow);
        eatSequence.AddChild(moveAction);
        eatSequence.AddChild(eatAction);

        orderSelector.AddChild(returnSequence);
        orderSelector.AddChild(gatherSequence);
        orderSelector.AddChild(buildSequence);

        returnSequence.AddChild(isHolding);
        returnSequence.AddChild(moveAction);
        returnSequence.AddChild(storeAction);

        gatherSequence.AddChild(isGatherOrder);
        gatherSequence.AddChild(moveAction);
        gatherSequence.AddChild(gatherAction);

        buildSequence.AddChild(isBuildOrder);
        buildSequence.AddChild(moveAction);
        buildSequence.AddChild(buildAction);

        root.Evaluate();
    }
    BTNodeState Eat()
    {
        // 섭취 행동 대기시간 및 애니메이션
        return BTNodeState.Success;
    }
    BTNodeState Move()
    {
        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
        {
            //Debug.Log("Move Finish");
            //ChangeState(State.Idle);
            return BTNodeState.Success;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, entityData.speed * Time.deltaTime);
        return BTNodeState.Running;
    }

    BTNodeState Store()
    {
        entityData.isHolding = false;
        Debug.Log("Stored");
        // 자원 보관 추가
        return BTNodeState.Success;
    }
    BTNodeState GatherResource()
    {
        ChangeState(State.Gather);
        Debug.Log("Gathering Resouces");

        entityData.isHolding = true;
        entityData.holdValue = entityData.gatherValue;  //자원 수집량만큼 보유
                                                        //자원 수집 애니메이션 및 딜레이 추가
        Debug.Log("Returning to Cargo");
        return BTNodeState.Success;
    }
    BTNodeState Build()
    {
        if (currentTask == TaskType.Build && state != State.Build)   // 최초 진입 시
        {
            ChangeState(State.Build);
            // 건설 자원 소모 및 대기시간, 애니메이션
            StartCoroutine("BuildTimer");
        }
        else if (currentTask == TaskType.Build && state != State.Build)   // 건설 종료 후
        {
            return BTNodeState.Success;
        }
        return BTNodeState.Running;
    }
    BTNodeState Idle()
    {
        // 유휴 애니메이션
        return BTNodeState.Running;
    }
    bool IsKcalLow()
    {
        if (entityData.kcal<=50)    //수치 조정
            return true;
        else 
            return false;
    }
    bool IsHolding()
    {
        if (entityData.isHolding)
        {
            targetPos = cargoPos;
            ChangeState(State.Return);
            return true;
        }
        return false;
    }
    bool IsGatherOrder()
    {
        if (currentTask == TaskType.Gather)
        {
            targetPos = nodePos;
            return true;
        }
        else
            return false;
    }
    bool IsBuildOrder()
    {
        if (currentTask == TaskType.Build)
            return true;
        else
            return false;
    }

    private void FixedUpdate()
    {
        root.Evaluate();
    }
    void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                this.state = State.Idle;
                break;
            case State.Move:
                this.state = State.Move;
                break;
            case State.Gather:
                this.state = State.Gather;
                break;
            case State.Return:
                this.state = State.Return;
                break;
            case State.Eat:
                this.state = State.Eat;
                break;
            case State.Build:
                this.state = State.Build;
                break;
        }
    }
    //void Move()
    //{
    //    if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
    //    {
    //        Debug.Log("Move Finish");
    //        ChangeState(State.Idle);
    //        MoveEnd();
    //    }
    //    transform.position = Vector2.MoveTowards(transform.position, targetPos, entityData.speed * Time.deltaTime);
    //}
    //void MoveEnd()
    //{
    //    switch(currentTask)
    //    {
    //        case TaskType.None:
    //            ChangeState(State.Idle);
    //            break;
    //        case TaskType.Gather:
    //            if (transform.position.x == cargoPos.x && transform.position.y == cargoPos.y)   // 저장소에 도착 시 
    //            {
    //                ColonyManager.Instance.GetResoruce(entityData.holdValue);   // 보유중인 자원만큼 저장소 자원 추가
    //                targetPos = nodePos;
    //                ChangeState(State.Move);
    //            }
    //            else if (transform.position.x == nodePos.x && transform.position.y == nodePos.y)    // 자원 노드에 도착 시
    //            {
    //                    targetPos = nodePos;
    //                    ChangeState(State.Gather);  // 수집 상태로 변경
    //            }
    //            break;
    //        case TaskType.Build:
    //            ChangeState(State.Idle);
    //            break;
    //        case TaskType.Eat:
    //            ChangeState(State.Idle);
    //            break;
    //    }
    //}
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
    }
    //void Idle()
    //{
    //    // 유휴 행동
    //}

    //bool FoodCheck()
    //{
    //    if (entityData.kcal < 50)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //void GatherResouce()
    //{
    //    Debug.Log("Gathering Resouces");
    //    //자원 수집
    //    entityData.isHolding = true;
    //    entityData.holdValue = entityData.gatherValue;  //자원 수집량만큼 보유
    //    //자원 수집 애니메이션 및 딜레이 추가

    //    targetPos = cargoPos;
    //    Debug.Log("Returning to Cargo");
    //    ChangeState(State.Move);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Cargo"))
    //    {
    //        Debug.Log("arrived at Cargo");
    //    }
    //    else if(collision.CompareTag("ResourceNode"))
    //    {
    //        Debug.Log("arrived at ResourceNode");
    //    }
    //}
    public State GetCurrentState()
    {
        return state;
    }
    public TaskType GetCurrentTask()
    {
        return currentTask;
    }
    public void GetTask(TaskType task)
    {
        currentTask = task;
    }
    public int GetGatherValue()
    {
        return entityData.gatherValue;
    }
    public Vector2 GetTargetNodePos()
    {
        return nodePos;
    }

    IEnumerator BuildTimer()
    {
        yield return new WaitForSeconds(buildTime);
        Debug.Log("Build Finish");
        currentTask = TaskType.None;
    }
}
