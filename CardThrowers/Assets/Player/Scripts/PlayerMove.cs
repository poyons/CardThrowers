using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("à⁄ìÆê›íË")]
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Keyboard.current == null)
            return;

        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            x -= 1f;
        }

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            x += 1f;
        }

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
        {
            z += 1f;
        }

        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
        {
            z -= 1f;
        }

        Vector3 moveDirection = new Vector3(x, 0f, z).normalized;

        transform.position += transform.TransformDirection(moveDirection)
                            * moveSpeed * Time.deltaTime;
    }
}
