using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }


    //These functions are only meant for devwork and should be deleted from the final version
    void SkipScenes(int amount)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + amount);
    }

    void ReverseScenes(int amount)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - amount);
    }
}
