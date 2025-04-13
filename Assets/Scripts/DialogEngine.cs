using TMPro;
using System.Collections.Generic;
using UnityEngine;

class DialogEngine : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogWrapper;

    [SerializeField]
    private GameObject leftDialog;

    [SerializeField]
    private GameObject rightDialog;

    private TMP_Text leftTmpText;
    private TMP_Text rightTmpText;

    public AudioSource leftVoice;
    public AudioSource rightVoice;

    private int currentReplicaIndex = 0;

    private void Awake()
    {
        PlayerPrefs.SetInt("dialog.engine.start", 0);
        PlayerPrefs.SetInt("dialog.engine.next", 0);
        PlayerPrefs.SetInt("dialog.engine.skip", 0);
        PlayerPrefs.SetInt("dialog.engine.started", 0);
        PlayerPrefs.SetInt("dialog.engine.typewriter.finished", 0);
        PlayerPrefs.Save();

        leftTmpText = leftDialog.GetComponentInChildren<TMP_Text>();
        rightTmpText = rightDialog.GetComponentInChildren<TMP_Text>();

        leftDialog.SetActive(false);
        rightDialog.SetActive(false);
        dialogWrapper.SetActive(false);
    }

    public static void DialogSkipOrNext()
    {
        if (PlayerPrefs.GetInt("dialog.engine.typewriter.finished") == 1)
            PlayerPrefs.SetInt("dialog.engine.next", 1);
        else
            PlayerPrefs.SetInt("dialog.engine.skip", 1);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && PlayerPrefs.GetInt("dialog.engine.started") == 1)
            DialogSkipOrNext();

        if (PlayerPrefs.GetInt("dialog.engine.start") == 1)
        {
            PlayerPrefs.SetInt("dialog.engine.start", 0);
            PlayerPrefs.SetInt("dialog.engine.started", 1);
            PlayerPrefs.Save();
            string dialogTopic = PlayerPrefs.GetString("dialog.engine.topic");
            dialogWrapper.SetActive(true);
            startTopic(dialogTopic);
        }
        if (PlayerPrefs.GetInt("dialog.engine.next") == 1)
        {
            PlayerPrefs.SetInt("dialog.engine.next", 0);
            PlayerPrefs.Save();
            string dialogTopic = PlayerPrefs.GetString("dialog.engine.topic");
            startTopic(dialogTopic);
        }

        if (PlayerPrefs.GetInt("dialog.engine.break") == 1)
        {
            PlayerPrefs.SetInt("dialog.engine.break", 0);
            PlayerPrefs.Save();

            leftDialog.SetActive(false);
            rightDialog.SetActive(false);
            dialogWrapper.SetActive(false);
            PlayerPrefs.SetInt("dialog.engine.started", 0);
            PlayerPrefs.Save();
            currentReplicaIndex = 0;
        }
    }

    private void startTopic(string topic)
    {
        if (topic == "Test")
            Execute(new TestDialog().replicas());
        if (topic == "Fall")
            Execute(new FallDialog().replicas());
        // PROD
        if (topic == "Level1StartDialog")
            Execute(new Level1StartDialog().replicas());
        if (topic == "FirstJumpDialog")
            Execute(new FirstJumpDialog().replicas());
        if (topic == "SecondJumpDialog")
            Execute(new SecondJumpDialog().replicas());
        if (topic == "ThirdJumpDialog")
            Execute(new ThirdJumpDialog().replicas());
        if (topic == "Level2StartDialog")
            Execute(new Level2StartDialog().replicas());
        if (topic == "DoubleJumpAchiveDialog")
            Execute(new DoubleJumpAchiveDialog().replicas());
        if (topic == "SuccessDblJumpDialog")
            Execute(new SuccessDblJumpDialog().replicas());
        if (topic == "FailedDblJumpDialog")
            Execute(new FailedDblJumpDialog().replicas());
        if (topic == "Level3StartDialog")
            Execute(new Level3StartDialog().replicas());
        if (topic == "FirstTryToPassWallDialog")
            Execute(new FirstTryToPassWallDialog().replicas());
        if (topic == "LastTryToPassWallDialog")
            Execute(new LastTryToPassWallDialog().replicas());
        if (topic == "GlitchFoundDialog")
            Execute(new GlitchFoundDialog().replicas());
        if (topic == "NooooDialog")
            Execute(new NooooDialog().replicas());
        if (topic == "AfterFirstHiddenWorldDialog")
            Execute(new AfterFirstHiddenWorldDialog().replicas());
        if (topic == "UVFirstSeeGlitch")
            Execute(new UVFirstSeeGlitch().replicas());
        if (topic == "KeyFoundDialog")
            Execute(new KeyFoundDialog().replicas());
    }

    private void Execute(List<Replica> replicas)
    {
        if (currentReplicaIndex >= replicas.Count)
        {
            leftDialog.SetActive(false);
            rightDialog.SetActive(false);
            dialogWrapper.SetActive(false);
            PlayerPrefs.SetInt("dialog.engine.started", 0);
            PlayerPrefs.Save();
            currentReplicaIndex = 0;
            return;
        }

        Replica replica = replicas[currentReplicaIndex];

        if (replica.Role == Role.Left)
        {
            rightDialog.SetActive(false);
            leftDialog.SetActive(true);
            Typewriter.Instance.StartTypewriter(leftTmpText, replica.Text, leftVoice);
        }
        if (replica.Role == Role.Right)
        {
            leftDialog.SetActive(false);
            rightDialog.SetActive(true);
            Typewriter.Instance.StartTypewriter(rightTmpText, replica.Text, rightVoice);
        }

        currentReplicaIndex += 1;
    }
}