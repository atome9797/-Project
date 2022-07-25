using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 5f;

    Vector3 OffsetVal;
    
    void Start()
    {
        OffsetVal = transform.position - Target.position;
    }

    void Update()
    {
        Vector3 targetcamerapos = Target.position + OffsetVal;

        transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);
    }
}
