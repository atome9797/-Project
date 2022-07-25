using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template <typename T> 와 동일
public class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour //where 절은 T에 대한 제약조건을 나타낸다. => T는 MonoBehaviour 상속받기 때문에  컴포넌트만 받을수 있게 된다. 
{

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();


                //자동으로 게임 매니저를 생성하게 해줌 => 동적으로 생성
                /*if(_instance == null)
                {
                    GameObject go = new GameObject("@GameManager");
                    _instance = go.AddComponent<T>();
                }*/
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;

        }
    }

    protected void Awake()
    {
        if (_instance != null)
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }
        _instance = GetComponent<T>();
        //씬이 전환될때에도 파괴가 되지 않도록 한다.
        DontDestroyOnLoad(gameObject);
    }

}
