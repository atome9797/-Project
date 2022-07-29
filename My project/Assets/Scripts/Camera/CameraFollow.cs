using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 5f;
    public float moveTime = 0.3f;

    Vector3 OffsetVal;

    
    
    void Start()
    {
        OffsetVal = transform.position - Target.position;


        Vector3 targetcamerapos = Target.position + OffsetVal;

        transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);

    }
/*
    void Update()
    {
        Vector3 targetcamerapos = Target.position + OffsetVal;

        transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);
    }*/

       private void Update()
       {
           moveTime += Time.deltaTime;


           Vector3 targetcamerapos = OffsetVal + Vector3.back + new Vector3(0f,0f, moveTime);

           transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);

            
       }

       
}
