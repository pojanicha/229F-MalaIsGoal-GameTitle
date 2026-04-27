using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform respawnPoint;
    public static RespawnController Instance;

    private void Awake()
    {
        Instance = this;
    }

   
}
