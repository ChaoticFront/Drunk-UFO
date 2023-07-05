using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Целевой объект (игрок)
    public Vector3 offset; // Смещение камеры относительно игрока
    public float smoothTime = 0.2f; // Время плавного следования камеры за игроком
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Вычисляем позицию, к которой должна двигаться камера
        Vector3 desiredPosition = target.position + offset;

        // Плавно перемещаем камеру к желаемой позиции
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Направляем камеру на игрока
        transform.LookAt(target);
    }
}
