using JetBrains.Annotations;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public TrajectoryLine trajectoryLine;
    public float power = 10f;
    public Vector2 traPos = new Vector2(1f, 0f);

    bool facingRight = true;
    private bool isWallSliding;
    private float wallSldingSpeed = 1f; // ถ้าอยากได้หนืดกว่านี้ปรับตรงนี้ ค่าน้อย = หนืดขึ้นครัฟ

    [SerializeField] private Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 dragtStart;

    public float speedDrag = 2f; // ปรับความเร็วในการลาก

    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // ปรับแรงโน้มถ่วง
        //rb.linearDamping = 0.5f; // ปรับความต้าน
        trajectoryLine.line.enabled = false;
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        //เริ่มกด
        if (Input.GetMouseButtonDown(0) && (isGrounded || isWallSliding))
        {
            isDragging = true;
            dragtStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            trajectoryLine.line.enabled = true;
        }

        //ลาก
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = (dragtStart - dragEnd) * power * speedDrag;
           

            trajectoryLine.velocity = velocity;
            trajectoryLine.startOffset = traPos;

            trajectoryLine.RenderArc();
        }

        //กดปล่อย
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            trajectoryLine.line.enabled = false;

            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = (dragtStart - dragEnd) * power * speedDrag;
            
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(velocity, ForceMode2D.Impulse);

            // หัน

            if (velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (velocity.x < 0 && facingRight)
            {
                Flip();
            }
        }

        WallSlide();
        //Debug.Log("Walled: " + IsWalled() + " | Grounded: " + isGrounded);

    }

    // เช้คชน Obstacle ถ้าชนจะ Respawn
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Respawn();
        }
    }
   
    void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        
       Vector3 spawnPos = transform.position = RespawnController.Instance.respawnPoint.position;
        spawnPos.y += 1f;

        transform.position = spawnPos;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.9f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !isGrounded && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
            

            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSldingSpeed, float.MaxValue));
            rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            Mathf.Max(rb.linearVelocity.y, -wallSldingSpeed));
        }
        else
        {
            isWallSliding = false;
           
        }
    }

    private void OnDrawGizmos()
    {
        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheck.position, 0.9f);
        }
    }


}



  


