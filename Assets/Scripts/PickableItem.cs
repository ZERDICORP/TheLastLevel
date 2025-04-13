using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public string stateId;
    public string actionPayload;

    private void Awake()
    {
        PlayerPrefs.SetInt(stateId, 0);
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(stateId, 1);
            PlayerPrefs.Save();
            gameObject.SetActive(false);

            PlayerPrefs.SetString("dialog.engine.topic", actionPayload);
            PlayerPrefs.SetInt("dialog.engine.start", 1);
            PlayerPrefs.Save();
            return;
        }
    }
}
