using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Play");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnCreditClick()
    {
        SceneManager.LoadScene("Credit");
    }
}
