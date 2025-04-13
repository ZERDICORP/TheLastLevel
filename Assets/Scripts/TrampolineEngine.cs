using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TrampolineEngine : MonoBehaviour
{
    public float bounce = 20f;

    string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);

            if (sceneName == "Level3" && PlayerPrefs.GetInt("try.pass") == 0)
            {
                PlayerPrefs.SetInt("try.pass", 1);
                PlayerPrefs.Save();
            }
            return;
        }
    }
}
