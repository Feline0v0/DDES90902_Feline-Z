using UnityEngine;

public class FirstPersonControllerA : MonoBehaviour
{
    public Camera playerCamera; // 玩家摄像机（子物体）
    public GameObject targetUI;      // 显示的UI图像
    public LayerMask targetLayer; // 目标图层（设置为"AAA"）

    public float moveSpeed = 5.0f; // 移动速度
    public float mouseSensitivity = 100.0f; // 鼠标灵敏度

    private float rotationX = 0.0f; // 垂直旋转角度
    private bool isLookingAtTarget = false;

    public float maxRayDistance;

    void Start()
    {
        // 禁用重力并固定Y轴位置
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }

        // 隐藏UI初始状态
        if (targetUI != null)
        {
            targetUI.gameObject.SetActive(false);
        }

        // 锁定鼠标光标
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 处理鼠标输入（视角旋转）
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); // 限制垂直旋转角度

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);

        // 处理键盘输入（移动）
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // 发射射线检测目标
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance, targetLayer))
        {
            // 鼠标对准目标
            if (!isLookingAtTarget)
            {
                isLookingAtTarget = true;
                if (targetUI != null)
                {
                    targetUI.gameObject.SetActive(true);
                }
            }

            // 鼠标左键点击打印数字1
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.gameObject.GetComponent<Item>().Start_event();
            }
        }
        else
        {
            // 鼠标离开目标
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