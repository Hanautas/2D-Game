using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cursors")]
    public Texture2D cursorArrow;
    public Texture2D cursorChat;
    public Texture2D cursorInteract;

    [Header("Scene Management")]
    public bool loadStartScene = true;
    public string startSceneName;
    public string currentSceneName;

    private AsyncOperation currentScene;

    public GameObject loadingScreen;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    void Awake()
    {
        instance = this;

        if (loadStartScene)
        {
            SceneManager.LoadSceneAsync(startSceneName, LoadSceneMode.Additive);

            currentSceneName = startSceneName;
        }
    }

    public void LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true);

        currentScene = SceneManager.UnloadSceneAsync(currentSceneName);

        scenesLoading.Add(currentScene);
        scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));

        currentSceneName = sceneName;

        StartCoroutine(GetSceneLoadProgress());
    }

    [ContextMenu("Reload Scene")]
    public void ReloadScene()
    {
        loadingScreen.SetActive(true);

        currentScene = SceneManager.UnloadSceneAsync(currentSceneName);

        scenesLoading.Add(currentScene);
        scenesLoading.Add(SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        loadingScreen.SetActive(false);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}