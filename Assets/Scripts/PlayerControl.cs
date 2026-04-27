using JetBrains.Annotations;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public TrajectoryLine trajectoryLine;
    public float power = 10f;

    Rigidbody2D rb;
    private bool isDragging;

    Vector2 DragStartPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trajectoryLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            trajectoryLine.enabled = true;
        }
       


        if (Input.GetMouseButton(0))
        {
            
        }


        if (Input.GetMouseButtonUp(0))
        { 
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = (DragEndPos - DragStartPos) * power;
            rb.linearVelocity = velocity;

        }
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
        {
            Vector2[] results = new Vector2[steps];

            float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
            Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
            
            
            float drag = 1f - timestep * rigidbody.drag;
            Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
            {
                moveStep += gravityAccel;
                moveStep *= drag;
                pos += moveStep;
                results[i] = pos;
            }
            return results;
    }
    }



  


