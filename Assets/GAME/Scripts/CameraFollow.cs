using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ������� ������ (�����)
    public Vector3 offset; // �������� ������ ������������ ������
    public float smoothTime = 0.2f; // ����� �������� ���������� ������ �� �������
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // ��������� �������, � ������� ������ ��������� ������
        Vector3 desiredPosition = target.position + offset;

        // ������ ���������� ������ � �������� �������
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // ���������� ������ �� ������
        transform.LookAt(target);
    }
}
