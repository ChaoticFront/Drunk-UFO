using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject scoreUICanvas;
    [SerializeField] private ScoreUI scoreUI;
    private int scoreValue;
    public int ScoreValue => scoreValue;

    private void Start()
    {
        scoreUI = scoreUICanvas.GetComponentInChildren<ScoreUI>();
    }

    private void Update()
    {
        if (player != null && player.CompareTag("Player"))
        {
            scoreValue = Mathf.RoundToInt(player.position.z);
            scoreUI.DisplayScore(scoreValue);
        }
        else
        {
            scoreUI.HideScore();
        }
    }
}