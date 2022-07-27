using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject CloneTarget;
    public Transform _transform;
    public int GenerationPersent = 50;

    public float height = 0f;
    public float CloneDelaySec = 1f;

    protected float NextSecToClone = 0f;

    void Start()
    {
        
    }
    
    void Update()
    {
        float currSec = Time.time;
        if(NextSecToClone <= currSec) //시간이 넘었을때 실행
        {
            int randomval = Random.Range(0, 100);
            if(randomval > GenerationPersent)
            {
                CloneCar();
            }
            NextSecToClone = currSec + CloneDelaySec; 
        }
    }

    void CloneCar()
    {
        Transform clonepos = _transform;
        Vector3 offsetpos = clonepos.position;
        offsetpos.y = height;

        //ClonePos 의 위치에 맞게 좌표로 생성되도록함 => 자동차 방향 정렬가능
        GameObject cloneobj = Instantiate(CloneTarget.gameObject, offsetpos, _transform.rotation, transform);
        cloneobj.SetActive(true);

    }

}
