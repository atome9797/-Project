using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject CloneTarget;
    public Transform GeneratoinPos;
    public int GenerationPersent = 50;

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
        Transform clonepos = GeneratoinPos;
        Vector3 offsetpos = clonepos.position;
        offsetpos.y = 0f;

        GameObject cloneobj = Instantiate(CloneTarget.gameObject, offsetpos, GeneratoinPos.rotation, transform);
        cloneobj.SetActive(true);

    }

}
