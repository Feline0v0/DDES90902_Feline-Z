using UnityEngine;

public class FirstPersonControllerA : MonoBehaviour
{
    public Camera playerCamera; // ���������������壩
    public GameObject targetUI;      // ��ʾ��UIͼ��
    public LayerMask targetLayer; // Ŀ��ͼ�㣨����Ϊ"AAA"��

    public float moveSpeed = 5.0f; // �ƶ��ٶ�
    public float mouseSensitivity = 100.0f; // ���������

    private float rotationX = 0.0f; // ��ֱ��ת�Ƕ�
    private bool isLookingAtTarget = false;

    public float maxRayDistance;

    void Start()
    {
        // �����������̶�Y��λ��
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }

        // ����UI��ʼ״̬
        if (targetUI != null)
        {
            targetUI.gameObject.SetActive(false);
        }

        // ���������
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ����������루�ӽ���ת��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); // ���ƴ�ֱ��ת�Ƕ�

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);

        // ����������루�ƶ���
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // �������߼��Ŀ��
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance, targetLayer))
        {
            // ����׼Ŀ��
            if (!isLookingAtTarget)
            {
                isLookingAtTarget = true;
                if (targetUI != null)
                {
                    targetUI.gameObject.SetActive(true);
                }
            }

            // �����������ӡ����1
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.gameObject.GetComponent<Item>().Start_event();
            }
        }
        else
        {
            // ����뿪Ŀ��
            if (isLookingAtTarget)
            {
                isLookingAtTarget = false;
                if (targetUI != null)
                {
                    targetUI.gameObject.SetActive(false);
                }
            }
        }
    }
}