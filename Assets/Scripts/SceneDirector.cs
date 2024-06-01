using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void Start()
    {
        if (transform.parent != null)
            Debug.LogError("Scene Director must be placed at scene root!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadInterior(string scene)
    {
        GameObject root = Instantiate(new GameObject("Root"));
        DontDestroyOnLoad(root);
        foreach (var t in SceneManager.GetActiveScene().GetRootGameObjects())
            t.transform.SetParent(root.transform);
        root.SetActive(false);
        _internal.currentExterior = root.transform;
        _internal.currentScene = scene;
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void UnloadInterior()
    {
        if (_internal.currentExterior == null)
        {
            Debug.LogError("No exterior loaded");
            return;
        }

        // unload objects from the interior
        SceneManager.UnloadScene(_internal.currentScene);

        // restore previous level
        _internal.currentExterior.gameObject.SetActive(true);
        GameObject root = Instantiate(new GameObject("Root"));
        while (_internal.currentExterior.childCount > 0)
        {
            foreach (Transform t in _internal.currentExterior)
                t.SetParent(root.transform);
        }
        while (root.transform.childCount > 0)
        {
            foreach (Transform t in root.transform)
                t.SetParent(null);
        }


        // reset current exterior
        DestroyImmediate(root);
        DestroyImmediate(_internal.currentExterior.gameObject);
        _internal.currentExterior = null;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private static class _internal
    {
        public static string currentScene;
        public static Transform currentExterior;
    }
}
