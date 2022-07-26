using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false; // 사망 상태

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
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter : " + other.tag);
        if(other.tag == "Car" || other.tag == "Water")
        {
            if(isDead == false)
            {
                die();
            }
        }
    }


}
