using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� ���� ���¸� ǥ���ϰ�, ���� ������ UI�� �����ϴ� ���� �Ŵ���
// ������ �� �ϳ��� ���� �Ŵ����� ������ �� �ִ�.
// �̱��� ����

public class GameManager : SingletonBehaviour<GameManager>
{
    // ������ ����
    //public event UnityAction OnGameEnd2;
    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();

    public int ScoreIncreaseAmount = 1;
    public int CurrnetScore
    {
        get
        {
            return _currnetScore;
        }
        set
        {
            _currnetScore = value;
            OnScoreChanged.Invoke(_currnetScore);
        }
    }

    private int _currnetScore = 0; // ���� ����
    private bool _isEnd = false; // ���� ���� ����

    void Update()
    {
        // ���� ���� ���¿��� ������ ������� �� �ְ� �ϴ� ó��
        if (_isEnd && Input.GetKeyDown(KeyCode.R))
        {
            reset();
            SceneManager.LoadScene(0);
        }
    }

    // ������ ������Ű�� �޼���
    public void AddScore()
    {
        CurrnetScore += ScoreIncreaseAmount;
    }

    // �÷��̾� ĳ���Ͱ� ����� ���� ������ �����ϴ� �޼���
    public void End()
    {
        _isEnd = true;
        OnGameEnd.Invoke();
    }

    private void reset()
    {
        _currnetScore = 0;
        _isEnd = false;
    }
}