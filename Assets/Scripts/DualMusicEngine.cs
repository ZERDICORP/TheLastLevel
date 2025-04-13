using UnityEngine;
using System.Collections;

public class DualMusicEngine : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 1.5f;
    public float defaultVolume = 0.5f;  // Значение по умолчанию для громкости

    public AudioClip[] clips;
    private int currentState = 0;

    void Start()
    {
        // Проверяем, что клипы загружены
        if (clips.Length == 0)
        {
            Debug.LogError("No audio clips found in the Resources/Audio folder!");
            return;
        }

        PlayerPrefs.SetInt("dual.music.engine.next", 0);
        PlayerPrefs.Save();

        // Останавливаем аудио-источник, если он уже воспроизводит что-то
        audioSource.Stop();

        // Начинаем воспроизведение с первого клипа
        audioSource.clip = clips[0];
        audioSource.volume = defaultVolume;  // Устанавливаем громкость по умолчанию
        audioSource.Play();
    }

    void Update()
    {
        int state = PlayerPrefs.GetInt("dual.music.engine.next");
        if (state == 1)
        {
            // Если значение в PlayerPrefs равно 1, переключаем музыку
            SwitchToNextTrack();
            // После переключения сбрасываем значение обратно в 0
            PlayerPrefs.SetInt("dual.music.engine.next", 0);
            PlayerPrefs.Save();
        }
    }

    void SwitchToNextTrack()
    {
        // Определяем следующий клип (например, по кругу)
        int nextTrackIndex = (currentState + 1) % clips.Length;

        // Выбираем новый клип для текущего источника
        AudioClip nextClip = clips[nextTrackIndex];
        StartCoroutine(FadeSwitch(nextClip));

        // Обновляем текущий трек
        currentState = nextTrackIndex;
    }

    IEnumerator FadeSwitch(AudioClip nextClip)
    {
        float t = 0f;
        float initialVolume = audioSource.volume;

        // Плавно затухаем текущую музыку
        while (t < fadeDuration)
        {
            float progress = t / fadeDuration;
            audioSource.volume = initialVolume * (1f - progress);  // Плавно уменьшаем громкость
            t += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;  // Устанавливаем громкость в 0
        audioSource.Stop();  // Останавливаем текущую музыку

        // Устанавливаем новый клип и начинаем воспроизведение с начальной громкостью
        audioSource.clip = nextClip;
        audioSource.Play();

        // Плавно увеличиваем громкость новой музыки
        t = 0f;
        while (t < fadeDuration)
        {
            float progress = t / fadeDuration;
            audioSource.volume = defaultVolume * progress;
            t += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = defaultVolume;  // Устанавливаем окончательную громкость
    }
}
