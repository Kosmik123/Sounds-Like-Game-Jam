using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        //Unlocks and shows cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // If running in the Unity editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}