using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public Vector3 startPos; //파이어볼 시작위치
    public Vector3 endPos; //파이어볼 종료위치 = 파이어볼 범위 오브젝트 위치

    public Vector3 startHeightPos;
    public Vector3 endHeightPos;
    public Vector3 movePos;

    float t; //베지어곡선에 사용할 시간t
    
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


        //화살표 키눌렀을때 실행
        if (isTimeTrigger)
        {
            t += Time.deltaTime * 10;
            //1초 동안 베지에 곡선으로 그려지게 설정하기
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