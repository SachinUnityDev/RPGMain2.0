using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableCameraOnSceneLoad : MonoBehaviour
{
    public Camera cameraToDisable;

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, Scene newScene)
    {
        // Disable the specified camera
        if(newScene.name == "INTRO")
            if (cameraToDisable != null)
            {
                cameraToDisable.gameObject.SetActive(false);
            }
    }
}
