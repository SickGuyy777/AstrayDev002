using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // It is static so we can use it later in shooting and stuff
    public static bool isPaused;

    [SerializeField]
    private GameObject pauseMenu;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>
    /// </summary>
    private void Start() =>
            pauseMenu.SetActive(false);

    /// <summary>
    /// See <see cref="MonoBehaviour"/>
    /// </summary>
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    /// <summary>
    /// Load the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        //Currently not working becuase we dont have MainMenu
        Debug.LogError("Currently not working becuase we dont have MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Resume the game - 'unpause' the game and hide the UI.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    /// <summary>
    /// Pause the game and show the pause menu UI
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
    }
}
