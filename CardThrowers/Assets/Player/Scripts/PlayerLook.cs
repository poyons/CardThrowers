
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class PlayerLook : MonoBehaviour
{
    [Header("視点設定")]
    [SerializeField] private float mouseSensitivity = 0.03f;

    [Header("上下の視点制限")]
    [SerializeField] private float minVerticalAngle = -40f;
    [SerializeField] private float maxVerticalAngle = 60f;

    [Header("左右の視点制限")]
    [SerializeField] private float minHorizontalAngle = -90f;
    [SerializeField] private float maxHorizontalAngle = 90f;

    private Camera playerCamera;
    private Transform player;
    private float verticalRotation;
    private float horizontalRotation;
    private float initialHorizontalRotation;

    private void Awake()
    {
        // 自分についているCameraコンポーネントを取得
        playerCamera = GetComponent<Camera>();

        // 親オブジェクトのPlayerを取得
        player = transform.parent;

        if (player == null)
        {
            Debug.LogError(
                "PlayerLook: カメラの親にPlayerを設定してください。",
                this
            );

            enabled = false;
            return;
        }

        initialHorizontalRotation = player.eulerAngles.y;
    }

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x;

        if (verticalRotation > 180f)
        {
            verticalRotation -= 360f;
        }

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            minVerticalAngle,
            maxVerticalAngle
        );

        horizontalRotation = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        if (Mouse.current == null || playerCamera == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // 左右：親のPlayerを回転
        horizontalRotation += mouseDelta.x * mouseSensitivity;

        horizontalRotation = Mathf.Clamp(
            horizontalRotation,
            minHorizontalAngle,
            maxHorizontalAngle
        );

        player.rotation = Quaternion.Euler(
            0f,
            initialHorizontalRotation + horizontalRotation,
            0f
        );

        // 上下：カメラ自身を回転
        verticalRotation -= mouseDelta.y * mouseSensitivity;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            minVerticalAngle,
            maxVerticalAngle
        );

        transform.localRotation = Quaternion.Euler(
            verticalRotation,
            0f,
            0f
        );
    }
}
