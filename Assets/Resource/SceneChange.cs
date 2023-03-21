using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void Change(string sceneName)
    {
        StartCoroutine(ChangeCoroutine(sceneName));
    }


    IEnumerator FadeOut()
    {
        RectTransform mask = transform.GetChild(0).GetComponent<RectTransform>();
        float t = 0;
        while (t <= 1f)
        {
            t += Time.deltaTime;
            mask.sizeDelta = Vector2.one * Mathf.Lerp(1700f, 0f, t);
            yield return null;
        }
    }

    IEnumerator ChangeCoroutine(string sceneName)
    {
        float t = 0;
        RectTransform mask = transform.GetChild(0).GetComponent<RectTransform>();
        while (t <= 1f)
        {
            t += Time.deltaTime;
            mask.sizeDelta = Vector2.one * Mathf.Lerp(0f, 1700f, t);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

}
