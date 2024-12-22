using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Start()
    {
        // ����������� ������������� ������
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        // �������� �������� ��� �������� �� ����������� � ���������
        float horizontal = Input.GetAxis("Horizontal"); // ���� ��� ��� X (A/D)
        float vertical = Input.GetAxis("Vertical");     // ���� ��� ��� Z (W/S)

        // ����������� �������� ��� ����������� �� Y
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Rotate()
    {
        // �������� ������ �� X � Y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������������ �������� �� Y, ����� ������ �� ����������������

        transform.localRotation = Quaternion.Euler(xRotation, transform.eulerAngles.y + mouseX, 0f);
    }
}
