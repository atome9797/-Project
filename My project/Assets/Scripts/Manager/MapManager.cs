using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public enum LastRoadType
    {
        Grass = 0,
        Road,
        Max
    }


    //public GameObject[] mapObjectArray;

    public Road DefaultRoad = null;
    public Road WaterRoad = null;
    public GrassSpawn GrassRoad = null;

    public Transform ParentTransform = null;

    public int MinPosZ = -20;
    public int MaxPosZ = 20;

    public int FrontOffsetPosZ = 20;
    public int BackOffsetPosZ = 10;

    void Start()
    {
       

    }

    public int GroupRandomRoadLine(int p_posz)
    {
        int randomCount = Random.Range(1, 4);

        for(int i = 0; i < randomCount; i ++)
        {
            GeneratorRoadLine(p_posz + i);
        }

        return randomCount;
    }

    public int GroupRandomWaterLine(int p_posz)
    {
        int randomCount = Random.Range(1, 4);

        for (int i = 0; i < randomCount; i++)
        {
            GeneratorWaterLine(p_posz + i);
        }

        return randomCount;
    }

    public int GroupRandomGrassLine(int p_posz)
    {
        int randomCount = Random.Range(1, 3);

        for (int i = 0; i < randomCount; i++)
        {
            GeneratorGrassLine(p_posz + i);
        }

        return randomCount;
    }

    public void GeneratorRoadLine(int p_posz)
    {
        GameObject cloneobj = Instantiate(DefaultRoad.gameObject);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        int randomrot = Random.Range(0, 2);
        if (randomrot == 1)
        {
            cloneobj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        cloneobj.name = "RoadLine_" + p_posz.ToString();

        mapList.Add(cloneobj.transform);
        mapListDic.Add(p_posz, cloneobj.transform);
    }

    public void GeneratorWaterLine(int p_posz)
    {
        GameObject cloneobj = Instantiate(WaterRoad.gameObject);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        int randomrot = Random.Range(0, 2);
        if (randomrot == 1)
        {
            cloneobj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        cloneobj.name = "WatersLine_" + p_posz.ToString();

        mapList.Add(cloneobj.transform);
        mapListDic.Add(p_posz, cloneobj.transform);
    }

    public void GeneratorGrassLine(int p_posz)
    {
        GameObject cloneobj = Instantiate(GrassRoad.gameObject);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        cloneobj.name  = "GrassLine_" +p_posz.ToString();

        mapList.Add(cloneobj.transform);
        mapListDic.Add(p_posz, cloneobj.transform);
    }

    protected LastRoadType m_LastRoadType = LastRoadType.Max;
    protected List<Transform> mapList = new List<Transform>();
    protected Dictionary<int, Transform> mapListDic = new Dictionary<int, Transform>();
    protected int m_LastLinePos = 0;
    
    protected int m_MinLine = 0;
    public int m_DeleteLine = 10;
    public int m_BackOffsetLineCount = 30;


    //움직일때마다 카메라의 속도에 따라 새로 생성되는 발판
    public void UpdateForwardNBackMove(int p_posz)
    {
        if(mapList.Count <= 0)
        {
            m_LastRoadType = LastRoadType.Grass;
            m_MinLine = MinPosZ;
            int i = 0;
            //초기용 라인들 세팅
            for (i = MinPosZ; i < MaxPosZ; i++)
            {
                int offsetval = 0;
                if(i < 0)
                {
                    GeneratorGrassLine(i);
                }
                else
                {
                    if(m_LastRoadType == LastRoadType.Grass)
                    {
                        int randomval = Random.Range(0, 2);
                        if(randomval == 0)
                        {
                            //묶음으로 발판 나오게 하기
                            offsetval = GroupRandomWaterLine(i);
                        }
                        else
                        {
                            //묶음으로 발판 나오게 하기
                            offsetval = GroupRandomRoadLine(i);
                        }

                        m_LastRoadType = LastRoadType.Road;
                    }
                    else
                    {
                        //묶음으로 풀발판 나오게 하기 => 풀생성은 GrassSpawn에서 해결
                         offsetval = GroupRandomGrassLine(i);
                        m_LastRoadType = LastRoadType.Grass;
                    }
                    i += offsetval - 1;
                }
            }
            m_LastLinePos = i;
        
        }
        //새롭게 생성
        if(m_LastLinePos < p_posz + FrontOffsetPosZ)
        {
            int offsetval = 0;
            if (m_LastRoadType == LastRoadType.Grass)
            {
                int randomval = Random.Range(0, 2);
                if (randomval == 0)
                {
                    offsetval = GroupRandomWaterLine(m_LastLinePos);
                }
                else
                {
                    offsetval = GroupRandomRoadLine(m_LastLinePos);
                }

                m_LastRoadType = LastRoadType.Road;
            }
            else
            {
                offsetval = GroupRandomGrassLine(m_LastLinePos);
                m_LastRoadType = LastRoadType.Grass;
            }

            m_LastLinePos += offsetval;
        }

        //많이 지나갔으면 지우기
        if(p_posz - m_BackOffsetLineCount > m_MinLine - m_DeleteLine)
        {
            int count = m_MinLine + m_DeleteLine;
            for(int i = m_MinLine; i < count; ++i)
            {
                RemoveLine(i);
            }

            m_MinLine += m_DeleteLine;
        }
    }

    void RemoveLine(int p_posz)
    {
        if(mapListDic.ContainsKey(p_posz))
        {
            Transform transobj = mapListDic[p_posz];
            Destroy(transobj.gameObject);

            mapList.Remove(transobj);
            mapListDic.Remove(p_posz);
        }
        else
        {
            Debug.LogErrorFormat($"RemoveLine Error  {p_posz}");
        }
    }
    
}
