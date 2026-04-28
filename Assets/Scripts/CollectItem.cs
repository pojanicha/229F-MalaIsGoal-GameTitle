using UnityEngine;
using UnityEngine.Timeline;

public class CollectItem : MonoBehaviour
{
    private int addTime = 5;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {            
            TimeControl.Instance.AddTime(addTime);

           
            Destroy(other.gameObject);
        }
    }
}

