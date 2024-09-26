using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
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
