using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerMeshObject;

    private DynamicJoystick joystick;
    private Rigidbody rb;
    private MainPlayer mainPlayer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float raycastDistance = 1f; // Raycast mesafesi

    private void Start()
    {
        moveSpeed = GameManager.instance.playerSpeed;
        rb = GameManager.instance.rb;
        joystick = GameManager.instance.joystick;
        mainPlayer = gameObject.GetComponent<MainPlayer>();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isMoveable)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
        Vector3 moveVector = movement * moveSpeed * Time.fixedDeltaTime;

        if (movement != Vector3.zero)
        {
            // Hareket yönünde Raycast yaparak çarpýþmayý kontrol et (isTrigger olanlarý yok sayarak)
            if (!Physics.Raycast(transform.position, movement, raycastDistance, ~0, QueryTriggerInteraction.Ignore))
            {
                rb.MovePosition(transform.position + moveVector);
                mainPlayer.playerCurrentState = MainPlayer.PlayerState.Walk;

                if (playerMeshObject != null)
                {
                    playerMeshObject.transform.forward = movement; // moveVector.normalized yerine movement kullanýldý
                }
            }
        }
        else
        {
            mainPlayer.playerCurrentState = MainPlayer.PlayerState.Idle;
        }
    }
}
