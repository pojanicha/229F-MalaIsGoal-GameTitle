using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    public float speed = 100f;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
