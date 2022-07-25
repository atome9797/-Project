using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    public int ScoreIncreaseAmount = 1;
    private bool isGameover = false; // 게임 오버 상태
    public ScoreText scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameOverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    private int _score = 0; // 게임 점수

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

    //플레이어 캐릭터가 사망시 게임 오버를 실행하는 메소드
    public void End()
    {
        gameOverUI.SetActive(true);
        isGameover = true;
    }
}
