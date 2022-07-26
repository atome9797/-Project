using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false; // ��� ����

    private void die()
    {
        // ��� ó��
        //1. _isDead = true
        //2. �ִϸ��̼� ������Ʈ
        //3. �÷��̾� ĳ���� ���߱�
        //4. ������ �Ҹ��� ���
        isDead = true;
        gameObject.SetActive(false);  //������Ʈ �޼��� ���۾ȵ�
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
