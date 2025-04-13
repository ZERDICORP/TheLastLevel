using System.Collections;
using UnityEngine;
using TMPro;

public class Typewriter : MonoBehaviour
{
	private static Typewriter instance;
	private readonly string leadingChar = "";
	private readonly bool leadingCharBeforeDelay = false;

	public static Typewriter Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindFirstObjectByType<Typewriter>();
				if (instance == null)
				{
					GameObject singletonObject = new GameObject(typeof(Typewriter).ToString());
					instance = singletonObject.AddComponent<Typewriter>();
				}
			}
			return instance;
		}
	}

	public void StartTypewriter(TMP_Text tmpProText, string message, AudioSource voice, float delayBefore = 0f, float delayBetweenChars = 0.1f)
	{
		StopAllCoroutines();
		StartCoroutine(TypeText(tmpProText, message, delayBefore, delayBetweenChars, voice));
	}

	private IEnumerator TypeText(TMP_Text tmpProText, string message, float delayBefore, float timeBetweenChars, AudioSource voice)
	{
		if (message == "НЕТ-НЕТ-НЕТ-НЕТ-НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ…") {
			PlayerPrefs.SetInt("switch.possible", 1);
			PlayerPrefs.Save();
		}

		tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBefore);

		PlayerPrefs.SetInt("dialog.engine.typewriter.finished", 0);
		PlayerPrefs.Save();
		foreach (char c in message)
		{
			if (Random.value < 0.6f) voice.Play();

			if (PlayerPrefs.GetInt("dialog.engine.break.tpw") == 1)
			{
				PlayerPrefs.SetInt("dialog.engine.break.tpw", 0);
				tmpProText.text = "";
				break;
			}

			if (PlayerPrefs.GetInt("dialog.engine.skip") == 1)
			{
				PlayerPrefs.SetInt("dialog.engine.skip", 0);
				tmpProText.text = message + leadingChar;
				break;
			}

			if (tmpProText.text.Length > 0)
			{
				tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
			}
			tmpProText.text += c;
			tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBetweenChars);
		}

		PlayerPrefs.SetInt("dialog.engine.typewriter.finished", 1);
		PlayerPrefs.Save();

		if (leadingChar != "")
		{
			tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
		}
	}
}
