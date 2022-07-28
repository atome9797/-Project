using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false; // 사망 상태
    public Camera Camera;
    public GameObject _target;

    private void die()
    {
        // 사망 처리
        //1. _isDead = true
        //2. 애니메이션 업데이트
        //3. 플레이어 캐릭터 멈추기
        //4. 죽을때 소리도 재생
        isDead = true;
        gameObject.SetActive(false);  //컴포넌트 메세지 전송안됨
        GameManager.Instance.End();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnTriggerEnter : " + collision.collider.tag);
        if (collision.collider.tag == "Car" || collision.collider.tag == "Water")
        {
            if (isDead == false)
            {
                die();
            }
        }
    }

    private void Update()
    {
        
        if (CheckObjectIsInCamera() == false)
        {
            die();
        }
    }


    public bool CheckObjectIsInCamera()
    {
        Vector3 screenPoint = Camera.WorldToViewportPoint(_target.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        return onScreen;
    }


}
