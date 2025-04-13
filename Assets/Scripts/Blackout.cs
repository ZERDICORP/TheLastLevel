using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Blackout : MonoBehaviour
{
    public Image dimImage;
    public float dimDuration = 0.5f;
    public float delayTime = 0.2f;
    private string scene;

    private void Start()
    {
        PlayerPrefs.SetInt("blackout.finished", 0);
        PlayerPrefs.Save();

        scene = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetInt("finished", 0);
        PlayerPrefs.Save();

        if (dimImage == null)
        {
            Debug.LogError("Dim Image is not assigned!");
            return;
        }
    }

    private void Awake()
    {
        dimImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(DimScreen(0));
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("finished") == 1)
        {
            PlayerPrefs.SetInt("finished", 0);
            PlayerPrefs.Save();

            StartCoroutine(DimScreen(1));
        }
    }

    private IEnumerator DimScreen(float targetAlpha)
    {
        float startAlpha = dimImage.color.a;
        float timeElapsed = 0;

        while (timeElapsed < dimDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / dimDuration);
            dimImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        dimImage.color = new Color(0, 0, 0, targetAlpha);

        if (targetAlpha == 0)
        {
            if (scene == "Level1")
            {
                PlayerPrefs.SetString("dialog.engine.topic", "Level1StartDialog");
                PlayerPrefs.SetInt("dialog.engine.start", 1);
                PlayerPrefs.Save();
            }
            if (scene == "Level2")
            {
                PlayerPrefs.SetString("dialog.engine.topic", "Level2StartDialog");
                PlayerPrefs.SetInt("dialog.engine.start", 1);
                PlayerPrefs.Save();
            }
            if (scene == "Level3")
            {
                PlayerPrefs.SetString("dialog.engine.topic", "Level3StartDialog");
                PlayerPrefs.SetInt("dialog.engine.start", 1);
                PlayerPrefs.Save();
            }
        }

        if (targetAlpha == 1)
        {
            yield return new WaitForSeconds(delayTime);

            SceneFlowManager.Instance.LoadNextScene();
        }

        PlayerPrefs.SetInt("blackout.finished", 1);
        PlayerPrefs.Save();
    }
}
