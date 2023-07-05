using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;
    public bl_Joystick joystick;
    private Rigidbody rb;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private float gameOverDelay = 1.5f;
    private float explosionForce = 3f; // �������� ���� ������� ��������
    [SerializeField] private Score score;
    [SerializeField] private GameObject scoreUICanvas;
    [SerializeField] private ScoreUI scoreUI;
    private int currentScore;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreUI = scoreUICanvas.GetComponentInChildren<ScoreUI>();
    }


    private void Update()
    {
        // ��������� ������� ������
        if (transform.position.y < -1f)
        {
            // �������� ����� ��� ��������� ���� � ����������� ������ "Game Over" � ���������
            StartCoroutine(ShowGameOverScreen());
        }
    }

    private IEnumerator ShowGameOverScreen()
    {
        // ��������� �������� ������
        enabled = false;
        currentScore = scoreUI.GetCurrentScore();
        // ��������� ���� ������� � ����������� �� ���� ������������
        Collider[] colliders = FindObjectsOfType<Collider>();
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Obstacle"))
            {
                Rigidbody obstacleRb = collider.GetComponent<Rigidbody>();
                if (obstacleRb != null)
                {
                    Vector3 direction = collider.transform.position - transform.position;
                    direction.Normalize();
                    obstacleRb.AddForce(direction * explosionForce, ForceMode.Impulse);
                }
            }
        }

        // ��������� ���� ������� � ������
        rb.AddForce(-transform.forward * explosionForce, ForceMode.Impulse);

        // ���� �������� ��������
        yield return new WaitForSecondsRealtime(gameOverDelay);
        // Set the final score
        int finalScore = scoreUI.GetCurrentScore();
        scoreUI = score.GetComponent<ScoreUI>();
        // ���������� ����� "Game Over" ��� ��������� ������ ��������, ��������� � ���������� ����
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<GameOver>().SetFinalScore(currentScore);
    }

    private void FixedUpdate()
    {
        // �������� ������ � ���������� ���������
        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + forwardMovement);

        // �������� ������� ������ �� ���������
        float horizontalInput = joystick.Horizontal;

        // ��������� �������������� �������� � ���������� �����������
        Vector3 horizontalMovement = Vector3.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime;

        // ��������� �������������� �������� � ������� ������� ������
        Vector3 newPosition = rb.position + horizontalMovement;
        rb.MovePosition(newPosition);
    }


    private void OnTriggerEnter(Collider other)
    {
        // ��������� ������������ � ���������
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // �������� ����� ��� ��������� ���� � ����������� ������ "Game Over" � ���������
            StartCoroutine(ShowGameOverScreen());
        }
    }
}
