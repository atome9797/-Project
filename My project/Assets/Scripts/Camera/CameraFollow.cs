using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 5f;

    Vector3 m_OffsetVal;
    
    void Start()
    {
        m_OffsetVal = transform.position - Target.position;
    }

    void Update()
    {
        Vector3 targetcamerapos = Target.position + m_OffsetVal;

        transform.position = Vector3.Lerp(transform.position, targetcamerapos, Smoothing * Time.deltaTime);
    }
}
