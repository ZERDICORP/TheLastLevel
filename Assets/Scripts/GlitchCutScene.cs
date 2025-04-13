using System.Collections;
using UnityEngine;

class GlitchCutScene : MonoBehaviour
{
    [SerializeField] private GameObject GLITCH;

    private void Start()
    {
        PlayerPrefs.SetInt("glitch.cutscene.start", 0);
        PlayerPrefs.Save();

        GLITCH.SetActive(false);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("glitch.cutscene.start") == 1)
        {
            PlayerPrefs.SetInt("glitch.cutscene.start", 0);
            PlayerPrefs.SetInt("glitch.first.use", 1);
            PlayerPrefs.SetInt("pick.glitch", 1);
            PlayerPrefs.Save();

            GLITCH.SetActive(true);
        }
    }
}
