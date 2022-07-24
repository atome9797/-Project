using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected Raft RaftObject;
    protected Transform RaftCompareObj;

    public Vector3 m_RaftOffsetpos = Vector3.zero;

    private void Start()
    {
        string[] templayer = new string[] { "Tree" };
        m_TreeLayerMask = LayerMask.GetMask(templayer);
    }
    public enum E_DirectionType
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    //직렬화는 외부 스크립트에서 접근하지 않고 inspector에서 접근할수 있게 함
    [SerializeField]
    protected E_DirectionType _directionType = E_DirectionType.Up;
    protected int m_TreeLayerMask = -1;

    protected bool IsCheckDirectionViewMove(E_DirectionType p_movetype)
    {
        Vector3 direction = Vector3.zero;

        switch (p_movetype)
        {
            case E_DirectionType.Up:
                direction = Vector3.forward;
                break;
            case E_DirectionType.Down:
                direction = Vector3.back;
                break;
            case E_DirectionType.Left:
                direction = Vector3.left;
                break;
            case E_DirectionType.Right:
                direction = Vector3.right;
                break;
            default:
                Debug.LogErrorFormat($"SetActorMove Error : {p_movetype}");
                break;
        }

        RaycastHit hitobj;
        if(Physics.Raycast(transform.position, direction, out hitobj, 1f, m_TreeLayerMask))
        {
            return false;
        }

        return true;
    }

    protected void SetActorMove(E_DirectionType p_movetype)
    {
        if(!IsCheckDirectionViewMove(p_movetype))
        {
            return;
        }

        Vector3 offsetpos = Vector3.zero;

        switch (p_movetype)
        {
            case E_DirectionType.Up:
                offsetpos = Vector3.forward;
                break;
            case E_DirectionType.Down:
                offsetpos = Vector3.back;
                break;
            case E_DirectionType.Left:
                offsetpos = Vector3.left;
                break;
            case E_DirectionType.Right:
                offsetpos = Vector3.right;
                break;
            default:
                Debug.LogErrorFormat($"SetActorMove Error : {p_movetype}");
                break;
        }

        transform.position += offsetpos;
        m_RaftOffsetpos += offsetpos;
    }


    protected void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(E_DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetActorMove(E_DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetActorMove(E_DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetActorMove(E_DirectionType.Right);
        }
    }

    protected void UpdateRaft()
    {
        if(RaftObject == null)
        {
            return;
        }
        Vector3 actorpos = RaftObject.transform.position + m_RaftOffsetpos;
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
                m_RaftOffsetpos = transform.position - RaftObject.transform.position;
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
            m_RaftOffsetpos = Vector3.zero;
        }
    }


}
