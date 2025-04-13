using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Animator anim;
    public bool isFinish;

    private AnimStates AnimState
    {
        get { return (AnimStates)anim.GetInteger("open"); }
        set { anim.SetInteger("open", (int)value); }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isFinish || PlayerPrefs.GetInt("pick.key") == 1)
            {
                AnimState = AnimStates.open;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if (isFinish)
            {
                PlayerPrefs.SetInt("finished", 1);
                PlayerPrefs.Save();
            }
            return;
        }
    }

    public enum AnimStates
    {
        idle,
        open
    }
}
