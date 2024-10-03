using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private HypeManager hypeManager;

    [SerializeField] private TextMeshProUGUI scoreText, bestScoreText;
    private int score, bestScore;

    [SerializeField] private int hypeThreshold;

    private void Awake() {
        hypeManager = GetComponent<HypeManager>();
    }

    private void Start() {
        UpdateUI();
    }

    public void IncreaseScore() {
        score++;
        if (score >= hypeThreshold) {
            hypeManager.StartFire();
        }
        UpdateUI();
    }

    public void ResetScore() {
        if (score >= hypeThreshold) {
            hypeManager.EndFire();
        }
        score = 0;
        UpdateUI();
    }

    private void UpdateUI() {
        scoreText.text = "Score: " + score;
        if (score > bestScore) {
            bestScore = score;
        }
        bestScoreText.text = "Best score: " + bestScore;
    }
}
