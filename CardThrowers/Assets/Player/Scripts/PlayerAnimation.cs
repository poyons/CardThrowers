using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private bool isEnterAim=false;

    void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }
    private void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        if (!isEnterAim)
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                EnterAim();
            }

            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            CancelEnterAim();
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shot();
        }
    }
    private void Start()
    {
    }

    //カードを構える
    public void EnterAim()
    {
        playerAnimator.SetTrigger("EnterAim");
        isEnterAim=true;
    }
    public void ReEnterAim()
    {
        playerAnimator.SetTrigger("ReEnterAim");
        isEnterAim=false;
    }
    //投げる
    public void Shot()
    {
        playerAnimator.SetTrigger("Shot");
    }
    //キャンセル
    public void CancelEnterAim()
    {
        playerAnimator.SetTrigger("IsStay");
        isEnterAim=false;
    }
}