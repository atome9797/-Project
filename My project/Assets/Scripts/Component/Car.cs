using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float MoveSpeed = 1f;
    public float RangeDestroy = 12;

    void Start()
    {
        Destroy(gameObject, 12f);
    }

    void Update()
    {
        float movex = MoveSpeed * Time.deltaTime;
        transform.Translate(movex, 0f, 0f);
    }
}
