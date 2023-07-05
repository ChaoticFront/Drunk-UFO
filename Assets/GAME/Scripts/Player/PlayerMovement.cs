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
    private float explosionForce = 3f; // Значение силы разлета объектов
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
        // Проверяем падение игрока
        if (transform.position.y < -1f)
        {
            // Вызываем метод для остановки игры и отображения экрана "Game Over" с задержкой
            StartCoroutine(ShowGameOverScreen());
        }
    }

    private IEnumerator ShowGameOverScreen()
    {
        // Отключаем движение игрока
        enabled = false;
        currentScore = scoreUI.GetCurrentScore();
        // Применяем силу разлета в зависимости от угла столкновения
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

        // Применяем силу разлета к игроку
        rb.AddForce(-transform.forward * explosionForce, ForceMode.Impulse);

        // Ждем заданную задержку
        yield return new WaitForSecondsRealtime(gameOverDelay);
        // Set the final score
        int finalScore = scoreUI.GetCurrentScore();
        scoreUI = score.GetComponent<ScoreUI>();
        // Показываем экран "Game Over" или выполняем другие действия, связанные с окончанием игры
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<GameOver>().SetFinalScore(currentScore);
    }

    private void FixedUpdate()
    {
        // Движение вперед с постоянной скоростью
        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + forwardMovement);

        // Получаем входные данные от джойстика
        float horizontalInput = joystick.Horizontal;

        // Вычисляем горизонтальное движение в глобальном направлении
        Vector3 horizontalMovement = Vector3.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime;

        // Добавляем горизонтальное движение к текущей позиции игрока
        Vector3 newPosition = rb.position + horizontalMovement;
        rb.MovePosition(newPosition);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Проверяем столкновение с объектами
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Вызываем метод для остановки игры и отображения экрана "Game Over" с задержкой
            StartCoroutine(ShowGameOverScreen());
        }
    }
}
