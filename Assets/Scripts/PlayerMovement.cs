using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float clampPosition = 5f;
    [SerializeField] private float restartDelay = 2f;

    private bool _enabled = true;
    
    private bool _isGrounded;

    private void OnValidate()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_enabled) return;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Vector3 velocity = rb.velocity;
            velocity.z = 0;
            rb.velocity = velocity;
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!_enabled) return;
        
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 moveToPosition = transform.position + (Vector3.forward + Vector3.right * horizontal) * (speed * Time.fixedDeltaTime);
        moveToPosition.x = Mathf.Clamp(moveToPosition.x, -clampPosition, clampPosition);
        rb.MovePosition(moveToPosition);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag($"Ground"))
            _isGrounded = true;
        
        else if (other.gameObject.CompareTag($"Let"))
        {
            _enabled = false;
            rb.isKinematic = true;
            FindAnyObjectByType<GameManager>().DelayDie(restartDelay);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag($"Ground"))
            _isGrounded = false;
    }
}
