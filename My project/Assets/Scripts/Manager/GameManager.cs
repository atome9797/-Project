using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    public int ScoreIncreaseAmount = 1;
    private bool isGameover = false; // ���� ���� ����
    public ScoreText scoreText; // ������ ����� UI �ؽ�Ʈ
    public GameObject gameOverUI; // ���� ������ Ȱ��ȭ �� UI ���� ������Ʈ

    private int _score = 0; // ���� ����

    private void Update()
    {
        if(isGameover && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AddScore()
    {
        _score += ScoreIncreaseAmount;
        scoreText.UpdateText(_score);
    }

    //�÷��̾� ĳ���Ͱ� ����� ���� ������ �����ϴ� �޼ҵ�
    public void End()
    {
        gameOverUI.SetActive(true);
        isGameover = true;
    }
}
