using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapObjectArray;
    public Transform ParentTransform = null;

    public int MinPosZ = -20;
    public int MaxPosZ = 20;

    void Start()
    {
        for (int i = MinPosZ; i < MaxPosZ; i++)
        {
            CloneRoad(i);
        }

    }

    void CloneRoad(int p_posz)
    {

        int randomIndex = Random.Range(0, mapObjectArray.Length);
        GameObject cloneobj = Instantiate(mapObjectArray[randomIndex]);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        int randomrot = Random.Range(0, 2);
        if(randomrot == 1)
        {
            cloneobj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void Update()
    {
        
    }
}
