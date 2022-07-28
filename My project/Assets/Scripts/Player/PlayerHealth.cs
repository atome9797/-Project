using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false; // ��� ����
    public Camera Camera;
    public GameObject _target;

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
