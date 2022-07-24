using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn : MonoBehaviour
{
    public List<Transform> EnviromentObjectList = new List<Transform> ();
    public int StartMinVal = -12;
    public int StartMaxVal = 12;

    public int SpawnCreateRandom = 30;

    void GeneratorEnviroment()
    {
        int randomIndex = 0;
        int randomval = 0;
        GameObject tempclone = null; 
        Vector3 offsetpos = Vector3.zero;    

        for (int i = StartMinVal; i < StartMaxVal; ++i)
        {
            randomval = Random.Range(0, 100);

            if(randomval < SpawnCreateRandom)
            {
                randomIndex = Random.Range(0, EnviromentObjectList.Count);
                tempclone = Instantiate(EnviromentObjectList[randomIndex].gameObject);
                tempclone.SetActive(true);
                offsetpos.Set(i, 0.8f, 0f);

                tempclone.transform.SetParent(transform);
                tempclone.transform.localPosition = offsetpos;
            }
        }
        

    }

    void Start()
    {
        GeneratorEnviroment();
    }

    void Update()
    {
        
    }
}
