using JetBrains.Annotations;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public TrajectoryLine trajectoryLine;
    public float power = 10f;
    public Vector2 traPos = new Vector2(1f, 0f);

    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 dragtStart;

    public float speedDrag = 2f; // ปรับความเร็วในการลาก

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // ปรับแรงโน้มถ่วงให้เหมาะสม
        rb.linearDamping = 0.5f; // ปรับความต้านทานอากาศให้เหมาะสม
        trajectoryLine.line.enabled = false;
    }

    void Update()
    {
        //เริ่มกด
        if (Input.GetMouseButtonDown(0))
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
        }



    }




}



  


