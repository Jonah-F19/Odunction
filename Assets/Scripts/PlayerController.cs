using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats/Info")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckRadius = 0.2f;
    public int health = 100; // Player's health starts at 100
    private Rigidbody2D rb;
    private bool isGrounded;
    private int extraJump;
    public bool hasCutout = false;
    [Header("UI Screens")]
    public GameObject deathScreen;  // Reference to the Death Screen Panel
    public GameObject pauseScreen;  // Reference to the Pause Screen Panel

    public GameObject levelCompleteScreen;

    [Header("Misc")]
    public GameObject oKeyInDoor;

    private bool isPaused = false;  // Track whether the game is paused

    public GameObject fedora;

    public GameObject fedoraPickup;

    public GameObject fedoraText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false); // Ensure the pause screen is hidden at the start
        }
    }

    void Update()
    {
        // Handle movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (extraJump > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJump = 0;
            }
        }

        // Handle pause/unpause when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door")){
            if (hasCutout){
                CompleteLevel();
            }
        }

        if (collision.gameObject.CompareTag("Enemy")){
            health -= 10;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // You can add exit collision logic here if needed
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player touches the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            extraJump = 1; // Allow one extra jump
        }

        // Check if the player collects a cutout
        if (other.gameObject.CompareTag("Cutout"))
        {
            hasCutout = true;
            Destroy(other.gameObject);
        }

        // Check if the player is hit by a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(25);
            Destroy(other.gameObject); // Destroy the projectile
        }

        if (other.gameObject.CompareTag("EnemyHitbox"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Destroy(other.transform.parent.gameObject);
            fedoraPickup.SetActive(true);
            fedoraText.SetActive(true);

        }

        if (other.gameObject.CompareTag("KillZone"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("Health"))
        {
            if (health < 100){
                health += 50;
                Destroy(other.gameObject);
            }
        }

    if (other.gameObject.CompareTag("Fedora"))
    {
        moveSpeed += 2;
        jumpForce += 2;
        Debug.Log("Fedora collected!"); // Check if this prints
        Destroy(other.gameObject);
        fedora.SetActive(true);
        Debug.Log("Fedora activated!"); // Ensure this prints
    }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Reset grounded status when leaving the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Health"))
        {
            if (health < 100){
                health += 50;
                Destroy(other.gameObject);
            }
        }
    }

    // Method to handle player taking damage
    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);

        // Optional: Check if the player should "die"
        if (health <= 0)
        {
            Die();
        }
    }

    // Method called when health reaches zero
    void Die()
    {
        // Pause the game by setting time scale to 0
        Time.timeScale = 0f;

        // Activate the Death Screen UI
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        Debug.Log("Player has died!");
    }

    // Method to toggle pause state
    void TogglePause()
    {
        isPaused = !isPaused; // Toggle pause state

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // Method to pause the game
    void PauseGame()
    {
        Time.timeScale = 0f; // Stop time
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(true); // Show the pause screen
        }
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume time
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false); // Hide the pause screen
        }
        isPaused = false; // Ensure the pause state is reset
    }


    void CompleteLevel(){
        oKeyInDoor.SetActive(true);
        hasCutout = false;
        Time.timeScale = 0f;
        if (levelCompleteScreen != null){
            levelCompleteScreen.SetActive(true);
        }
    }
}