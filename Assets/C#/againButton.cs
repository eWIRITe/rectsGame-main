using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class againButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.playing = true;
        GameController._score = 0;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
