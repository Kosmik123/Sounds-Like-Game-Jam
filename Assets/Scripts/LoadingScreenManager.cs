using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainMenuScreen;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public void Start()
    {
        loadingScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
        mainMenuScreen.SetActive(false);

        while (!operation.isDone)
        {
            // Clamp the progress value at 0.99 (99%)
            float progress = Mathf.Clamp(operation.progress / 0.9f, 0f, 0.99f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            yield return null;
        }

        // Ensure the final progress is shown as 100% when done
        progressBar.value = 1f;
        progressText.text = "100%";
        yield return new WaitForSeconds(0.5f);
        loadingScreen.SetActive(false);
    }
}