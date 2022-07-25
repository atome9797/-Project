using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _ui;
    
    [SerializeField]
    protected Raft RaftObject;
    protected Transform RaftCompareObj;

    public Vector3 RaftOffsetpos = Vector3.zero;
    public MapManager MapManagerCom;

    //직렬화는 외부 스크립트에서 접근하지 않고 inspector에서 접근할수 있게 함
    [SerializeField]
    protected DirectionType _directionType = DirectionType.Up;
    protected int TreeLayerMask = -1;

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
                offsetpos = Vector3.forward;
                break;
            case DirectionType.Down:
                offsetpos = Vector3.back;
                break;
            case DirectionType.Left:
                offsetpos = Vector3.left;
                break;
            case DirectionType.Right:
                offsetpos = Vector3.right;
                break;
            default:
                Debug.LogErrorFormat($"SetActorMove Error : {p_movetype}");
                break;
        }
        
        transform.position += offsetpos;
        RaftOffsetpos += offsetpos;

        GameManager.Instance.AddScore();
        MapManagerCom.UpdateForwardNBackMove((int)transform.position.z);


    }


    protected void InputUpdate()
    {
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
