using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float RangeDestroy = 30f;

    void Start()
    {
        Destroy(gameObject, RangeDestroy);
    }


    void Update()
    {
        float movex = MoveSpeed * Time.deltaTime;
        transform.Translate(movex, 0f, 0f);
    }
}
