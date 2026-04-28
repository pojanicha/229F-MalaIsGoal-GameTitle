using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimeControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float timeScale; 
   
    bool isGameOver = false;

    public static TimeControl Instance; 

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return; // Exit the Update method if the game is already over


        if (timeScale > 0)
        {
            timeScale -= Time.deltaTime;
        }

        else
        {
            timeScale = 0;
            isGameOver = true; // Set the flag to indicate the game is over

          

        }

        int minutes = Mathf.FloorToInt(timeScale / 60);
        int seconds = Mathf.FloorToInt(timeScale % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }

    public void AddTime(float amount)
    {
        timeScale += amount;

        StartCoroutine(ChancgeColorGreen());

    }

    IEnumerator ChancgeColorGreen()
    {
        timeText.color = Color.green;

        yield return new WaitForSeconds(0.5f);

        timeText.color = Color.white;
    }

}
