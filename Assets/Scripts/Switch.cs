using UnityEngine;

public class Switch : MonoBehaviour
{
    public Door door;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (door != null)
            {
                door.PlatformDisappear();
            }

        }
    }
    
}
