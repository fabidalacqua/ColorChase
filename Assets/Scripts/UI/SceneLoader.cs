using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{ 
    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private Slider _slider;

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadAsynchoronously(index));
    }

    IEnumerator LoadAsynchoronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        _panel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            _slider.value = progress;

            yield return null;
        }
    }
}
