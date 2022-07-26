using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _ui;
    public float timeBeforeNextJump = 1f;
    private float canJump = 0f;
    float jumpTime = 0f;

    [SerializeField]
    protected Raft RaftObject;
    protected Transform RaftCompareObj;
    public Rigidbody _rigidbody;

    public Vector3 RaftOffsetpos = Vector3.zero;
    public MapManager MapManagerCom;

    //직렬화는 외부 스크립트에서 접근하지 않고 inspector에서 접근할수 있게 함
    [SerializeField]
    protected DirectionType _directionType = DirectionType.Up;
    protected int TreeLayerMask = -1;

    
    public Vector3 startPos;//이동전 위치
    public Vector3 endPos; //이동할 위치

    public Vector3 startHeightPos;  //이동전 위치 높이
    public Vector3 endHeightPos;  //이동후 위치 높이
    public Vector3 movePos;

    public float StepTime = 0f; //베지어곡선에 사용할 시간t

    bool isTimeTrigger = false;

    void setPosition(Vector3 movePos)
    {
        startPos = transform.position;
        endPos = transform.position + movePos;

        startHeightPos = startPos + Vector3.up;
        endHeightPos = endPos + Vector3.up;

    }


    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        string[] templayer = new string[] { "Tree" };
        TreeLayerMask = LayerMask.GetMask(templayer);
        MapManagerCom.UpdateForwardNBackMove((int)transform.position.z);
    }
    public enum DirectionType
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    protected bool IsCheckDirectionViewMove(DirectionType p_movetype)
    {

        Vector3 direction = Vector3.zero;

        switch (p_movetype)
        {
            case DirectionType.Up:
                direction = Vector3.forward;
                break;
            case DirectionType.Down:
                direction = Vector3.back;
                break;
            case DirectionType.Left:
                direction = Vector3.left;
                break;
            case DirectionType.Right:
                direction = Vector3.right;
                break;
            default:
                Debug.LogErrorFormat($"SetActorMove Error : {p_movetype}");
                break;
        }

        RaycastHit hitobj;
        if(Physics.Raycast(transform.position, direction, out hitobj, 1f, TreeLayerMask))
        {
            return false;
        }

        return true;
    }

    protected void SetActorMove(DirectionType p_movetype)
    {
        if(!IsCheckDirectionViewMove(p_movetype))
        {
            return;
        }

        Vector3 offsetpos = Vector3.zero;

        switch (p_movetype)
        {
            case DirectionType.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                offsetpos = Vector3.forward;
                setPosition(offsetpos);
                isTimeTrigger = true;
                break;
            case DirectionType.Down:
                transform.rotation = Quaternion.Euler(0, -180, 0);
                offsetpos = Vector3.back;
                setPosition(offsetpos);
                isTimeTrigger = true;
                break;
            case DirectionType.Left:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                offsetpos = Vector3.left;
                setPosition(offsetpos);
                isTimeTrigger = true;
                break;
            case DirectionType.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                offsetpos = Vector3.right;
                setPosition(offsetpos);
                isTimeTrigger = true;
                break;
            default:
                Debug.LogErrorFormat($"SetActorMove Error : {p_movetype}");
                break;
        }

        //transform.position += offsetpos;
        RaftOffsetpos += offsetpos;

        GameManager.Instance.AddScore();
        MapManagerCom.UpdateForwardNBackMove((int)transform.position.z);


    }

    void BezierCurve()
    {
        Vector3 a = Vector3.Lerp(startPos, startHeightPos, StepTime);
        Vector3 b = Vector3.Lerp(startHeightPos, endHeightPos, StepTime);
        Vector3 c = Vector3.Lerp(endHeightPos, endPos, StepTime);

        Vector3 d = Vector3.Lerp(a, b, StepTime);
        Vector3 e = Vector3.Lerp(b, c, StepTime);

        Vector3 f = Vector3.Lerp(d, e, StepTime);

        transform.position = f;
    }

    protected void InputUpdate()
    {
        Vector3 movePos = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetActorMove(DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetActorMove(DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetActorMove(DirectionType.Right);
        }


        //화살표 키눌렀을때 실행
        if (isTimeTrigger)
        {
            StepTime += Time.deltaTime * 10;
            //1초 동안 베지에 곡선으로 그려지게 설정하기
            if (StepTime <= 1f)
            {
                BezierCurve();
            }
            else
            {
                StepTime = 1f;
                BezierCurve();
                StepTime = 0f;
                isTimeTrigger = false;
            }
        }

    }

    protected void UpdateRaft()
    {
        if(RaftObject == null)
        {
            return;
        }
        Vector3 actorpos = RaftObject.transform.position + RaftOffsetpos;
        transform.position = actorpos;
    }

    private void Update()
    {
        

        UpdateRaft();
        InputUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Raft")
        {
            RaftObject = other.transform.parent.GetComponent<Raft>();

            if(RaftObject != null)
            {
                RaftCompareObj = RaftObject.transform;
                RaftOffsetpos = transform.position - RaftObject.transform.position;
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Raft" && RaftCompareObj == other.transform.parent)
        {
            RaftCompareObj = null;
            RaftObject = null;
            RaftOffsetpos = Vector3.zero;
        }
    }


}
