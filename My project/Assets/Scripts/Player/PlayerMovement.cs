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

    //����ȭ�� �ܺ� ��ũ��Ʈ���� �������� �ʰ� inspector���� �����Ҽ� �ְ� ��
    [SerializeField]
    protected DirectionType _directionType = DirectionType.Up;
    protected int TreeLayerMask = -1;

    
    public Vector3 startPos;//�̵��� ��ġ
    public Vector3 endPos; //�̵��� ��ġ

    public Vector3 startHeightPos;  //�̵��� ��ġ ����
    public Vector3 endHeightPos;  //�̵��� ��ġ ����
    public Vector3 movePos;

    public float StepTime = 0f; //�������� ����� �ð�t

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


        //ȭ��ǥ Ű�������� ����
        if (isTimeTrigger)
        {
            StepTime += Time.deltaTime * 10;
            //1�� ���� ������ ����� �׷����� �����ϱ�
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
