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

    public void UpdateText(int score)
    {
        //_ui의 텍스트를 수정
        _ui.text = $"{score}";
    }



}