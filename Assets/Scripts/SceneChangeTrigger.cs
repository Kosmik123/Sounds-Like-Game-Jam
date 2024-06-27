using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public float teleportDelay = 0.1f;
    public string SceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(ChangeSceneAfterDelay());
            }
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(teleportDelay);
        ChangeScene();
    }

    private void ChangeScene()
    {
        Debug.Log("Scene change");
        SceneManager.LoadScene(SceneToLoad);
    }
}
