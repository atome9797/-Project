using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public Vector3 startPos; //���̾ ������ġ
    public Vector3 endPos; //���̾ ������ġ = ���̾ ���� ������Ʈ ��ġ

    public Vector3 startHeightPos;
    public Vector3 endHeightPos;
    public Vector3 movePos;

    float t; //�������� ����� �ð�t
    
    bool isTimeTrigger = false;



    void setPosition (Vector3 movePos)
    {
        startPos = transform.position;
        endPos = transform.position + movePos;

        startHeightPos = startPos + Vector3.up;
        endHeightPos = endPos + Vector3.up;
    }


    void Update()
    {

        Vector3 movePos = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movePos = Vector3.forward;
            setPosition(movePos);
            isTimeTrigger = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movePos = Vector3.back;
            setPosition(movePos);
            isTimeTrigger = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movePos = Vector3.left;
            setPosition(movePos);
            isTimeTrigger = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movePos = Vector3.right;
            setPosition(movePos);
            isTimeTrigger = true;
        }


        //ȭ��ǥ Ű�������� ����
        if (isTimeTrigger)
        {
            t += Time.deltaTime * 10;
            //1�� ���� ������ ����� �׷����� �����ϱ�
            if (t <= 1f)
            { 
                BezierCurve();
            }else
            {
                t = 1f;
                BezierCurve();
                t = 0f;
                isTimeTrigger = false;
            }
        }


    }


    void BezierCurve()
    {
        Vector3 a = Vector3.Lerp(startPos, startHeightPos, t);
        Vector3 b = Vector3.Lerp(startHeightPos, endHeightPos, t);
        Vector3 c = Vector3.Lerp(endHeightPos, endPos, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 f = Vector3.Lerp(d, e, t);

        transform.position = f;
    }


}