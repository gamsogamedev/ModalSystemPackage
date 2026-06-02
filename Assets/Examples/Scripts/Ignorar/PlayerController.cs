using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool canMove = false;
    
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private GameObject sword;
    
    private void Update()
    {
        if (!canMove) return;

        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed) moveX += 1f;
            if (Keyboard.current.aKey.isPressed) moveX -= 1f;
            if (Keyboard.current.wKey.isPressed) moveY += 1f;
            if (Keyboard.current.sKey.isPressed) moveY -= 1f;
        }
#else
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
#endif

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            transform.Translate(moveDirection * (moveSpeed * Time.deltaTime), Space.World);
        }
    }

    private void HandleInteraction()
    {
        bool interactPressed = false;

#if ENABLE_INPUT_SYSTEM
        // NEW INPUT SYSTEM
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactPressed = true;
        }
#else
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactPressed = true;
        }
#endif

        if (interactPressed)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, 1f, interactableLayer);

            if (hit != null)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                canMove = false;
                interactable?.Interact(this);
            }
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EquipSword()
    {
        sword.SetActive(true);
    }

    public Sprite GetPlayerSprite()
    {
        return playerRenderer.sprite;
    }

    public void SetPlayerSprite(Sprite sprite)
    {
        playerRenderer.sprite = sprite;
    }
}