using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    private GameObject[] _childs;
    private int _childCount;


    private void Awake()
    {
        _childCount = transform.childCount;
        _childs = new GameObject[_childCount];

        for(int i = 0; i < _childCount; ++i)
        {
            _childs[i] = transform.GetChild(i).gameObject;
        }
    }


    public void Activate()
    {
        for(int i  = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(true);
            Debug.Log("자식");
        }
    }


    private void OnEnable()
    {
        Debug.Log("구독");
        GameManager.Instance.OnGameEnd.AddListener(Activate);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd.RemoveListener(Activate);
        Debug.Log("구독취소");
    }


}
