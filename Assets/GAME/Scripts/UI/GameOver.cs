using TMPro;
using UnityEngine;


public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultScoreText;
    private ScoreUI scoreUI;

    private void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>();
        if (scoreUI != null)
        {
            int currentScore = scoreUI.GetCurrentScore();
            SetFinalScore(currentScore);
        }
    }

    public void SetFinalScore(int score)
    {
        resultScoreText.text = "<size=120>score:</size>\n" + score.ToString();
    }
}


