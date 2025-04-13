using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject normalLevel;
    public GameObject hiddenWorld;
    public Image dimImage; // Ссылка на Image, который будет затемнять экран
    public float dimDuration = 0.5f; // Время затемнения или осветления
    public float delayTime = 0.2f; // Задержка перед переключением (например, перед сменой уровня)

    private bool isDimmed = false; // Флаг, указывающий, затемнен ли экран

    private bool isNormalWorld = true;

    string sceneName;
    bool firstHidden;

    private void Start()
    {
        PlayerPrefs.SetInt("pick.glitch", 0);

        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level4" || sceneName == "Level5" || sceneName == "Test")
        {
            PlayerPrefs.SetInt("pick.glitch", 1);
        }

        PlayerPrefs.Save();

        if (dimImage == null)
        {
            Debug.LogError("Dim Image is not assigned!");
            return;
        }

        // Начать с яркости 0 (прозрачный экран)
        dimImage.color = new Color(0, 0, 0, 0);

        hiddenWorld.SetActive(false);
        normalLevel.SetActive(true);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("dialog.engine.started") == 1 && PlayerPrefs.GetInt("switch.possible") == 0)
        {
            return;
        }

        if (PlayerPrefs.GetInt("switch.possible") == 1)
        {
            PlayerPrefs.SetInt("switch.possible", 1);
			PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.E) && PlayerPrefs.GetInt("pick.glitch") == 1)
        {
            if (sceneName == "Level3" && PlayerPrefs.GetInt("glitch.first.use") == 1)
            {
                firstHidden = true;

                PlayerPrefs.SetInt("glitch.first.use", 0);
                PlayerPrefs.SetString("dialog.engine.topic", "NooooDialog");
                PlayerPrefs.SetInt("dialog.engine.start", 1);
                PlayerPrefs.Save();

                StartCoroutine(DelayedAction(2f, () =>
                {
                    PlayerPrefs.SetInt("dialog.engine.break", 1);
                    PlayerPrefs.SetInt("dialog.engine.break.tpw", 1);
                    PlayerPrefs.Save();

                    rn();
                }));
            }
            else rn();
        }
    }
    private void rn()
    {
        PlayerPrefs.SetInt("dual.music.engine.next", 1);
        PlayerPrefs.Save();

        if (isDimmed)
        {
            // Если экран уже затемнен, освещаем его
            StartCoroutine(DimScreen(0)); // Освещаем экран
        }
        else
        {
            // Если экран не затемнен, затемняем его
            StartCoroutine(DimScreen(1)); // Затемняем экран
        }

        // Меняем флаг, чтобы отслеживать состояние
        isDimmed = !isDimmed; // Меняем состояние флага
    }


    private IEnumerator DimScreen(float targetAlpha)
    {
        float startAlpha = dimImage.color.a;
        float timeElapsed = 0;

        // Плавное изменение альфы
        while (timeElapsed < dimDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / dimDuration);
            dimImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Устанавливаем точное значение в конце
        dimImage.color = new Color(0, 0, 0, targetAlpha);

        if (targetAlpha == 0)
        {
            if (sceneName == "Level3" && firstHidden && !isNormalWorld)
            {
                PlayerPrefs.SetString("dialog.engine.topic", "AfterFirstHiddenWorldDialog");
                PlayerPrefs.SetInt("dialog.engine.start", 1);
                PlayerPrefs.Save();
            }
            if (sceneName == "Level3" && firstHidden && isNormalWorld)
            {
                PlayerPrefs.SetString("dialog.engine.topic", "UVFirstSeeGlitch");
                PlayerPrefs.SetInt("dialog.engine.start", 1);
                PlayerPrefs.Save();
                firstHidden = false;
            }
        }

        // Пауза перед выполнением других действий, если экран затемнен
        if (targetAlpha == 1)
        {
            yield return new WaitForSeconds(delayTime);

            // Переключаем между мирами
            if (isNormalWorld)
            {
                normalLevel.SetActive(false);
                hiddenWorld.SetActive(true);
                isNormalWorld = false;
            }
            else
            {
                hiddenWorld.SetActive(false);
                normalLevel.SetActive(true);
                isNormalWorld = true;
            }

            // После смены мира, начинаем осветлять экран
            StartCoroutine(DimScreen(0)); // Освещаем экран
            isDimmed = false; // Сбрасываем флаг затемнения
        }
    }

    private IEnumerator DelayedAction(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);  // Ожидаем задержку
        action?.Invoke();  // Выполняем действие
    }
}
