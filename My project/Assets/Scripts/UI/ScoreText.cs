using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _ui;

    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
        GameManager.Instance.OnScoreChanged.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
    }

    public void UpdateText(int score)
    {
        //_ui의 텍스트를 수정
        _ui.text = $"{score}";
    }



}