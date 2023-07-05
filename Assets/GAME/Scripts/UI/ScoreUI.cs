using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

    public void DisplayScore(int score)
    {
        currentScore = score;
        scoreText.text = currentScore.ToString();
        gameObject.SetActive(true);
    }
    public void SetFinalScore(int score)
    {
        currentScore = score;
        scoreText.text = "ResultScore: " + currentScore.ToString();
    }
    public void HideScore()
    {
        gameObject.SetActive(false);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}