using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckRadius = 0.2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int extraJump;
    private bool hasCutout = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        //Handle movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        //Handle jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }else if (extraJump > 0){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJump = 0;
            }

            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnCollisionExit2D(Collision2D collision)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            extraJump = 1;
        }

        if (other.gameObject.CompareTag("Cutout")){
            hasCutout = true;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
