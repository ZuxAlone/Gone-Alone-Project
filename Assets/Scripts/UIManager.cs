using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartPlaying()
    {
        GameManager.Instance.StartPlaying();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        DestroyAudioManager();
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    private void DestroyAudioManager()
    {
        Destroy(FindObjectOfType<AudioManager>().gameObject);
    }
}
