using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{

    public float MoveSpeed = 10f;
    public float RangeDestroy = 4;

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
