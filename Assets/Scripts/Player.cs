using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
	private float speed = 2.5f;
	private float jumpForce = 5f;
	[SerializeField] private Transform spawnPoint;

	public AudioSource deatchSound;

	private bool isGrounded = false;
	private int jumpCount = 0;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	// Platform logic
	private bool isOnPlatform = false;
	private GameObject currentPlatform;
	private Vector3? lastPlatformPos = null;

	private int jumpTries = 0;
	private bool tryingJump = false;

	private AnimStates AnimState
	{
		get { return (AnimStates)anim.GetInteger("state"); }
		set { anim.SetInteger("state", (int)value); }
	}

	private string sceneName;

	private void Start()
	{
		PlayerPrefs.SetInt("pick.dbljump", 0);
		PlayerPrefs.SetInt("pick.glitch", 0);

		sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "Level3" || sceneName == "Level4" || sceneName == "Level5" || sceneName == "Test")
		{
			PlayerPrefs.SetInt("pick.dbljump", 1);
		}

		if (sceneName == "Level1")
		{
			speed = 0.5f;
			jumpForce = 0;
		}

		PlayerPrefs.Save();
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		if (isOnPlatform && currentPlatform != null)
		{
			if (lastPlatformPos.HasValue)
			{
				Vector3 delta = currentPlatform.transform.position - lastPlatformPos.Value;
				transform.position += delta;
			}
			lastPlatformPos = currentPlatform.transform.position;
		}

		CheckGround();

		int maxJumpCount = 1;
		if (PlayerPrefs.GetInt("pick.dbljump") == 1) maxJumpCount = 2;
		if (!isGrounded && jumpCount == 0) jumpCount += 1;

		if (isGrounded && rb.linearVelocity.y <= 0.01f)
			AnimState = AnimStates.idle;

		if (PlayerPrefs.GetInt("dialog.engine.started") == 1 || PlayerPrefs.GetInt("blackout.finished") == 0)
			return;

		if (Input.GetButton("Horizontal"))
			Run();

		if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount && !tryingJump)
		{
			if (sceneName == "Level1" && jumpTries <= 2)
				tryingJump = true;
			Jump();
		}
	}

	private void Run()
	{
		AnimState = AnimStates.moving;

		float horizontalInput = Input.GetAxis("Horizontal");

		Vector3 dir = transform.right * horizontalInput;
		transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

		sprite.flipX = dir.x < 0.0f;
	}

	private void Jump()
	{
		rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
		rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		jumpCount++;
	}

	int tryPassCount = 0;

	private void CheckGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
		bool wasGrounded = isGrounded;
		isGrounded = false;

		foreach (var col in colliders)
		{
			if (col.CompareTag("Ground") || col.CompareTag("Platform"))
			{
				isGrounded = true;
				break;
			}
		}

		if (isGrounded)
		{
			if (tryingJump && sceneName == "Level1" && jumpTries == 0)
			{
				PlayerPrefs.SetString("dialog.engine.topic", "FirstJumpDialog");
				PlayerPrefs.SetInt("dialog.engine.start", 1);
				PlayerPrefs.Save();
				jumpTries += 1;
				tryingJump = false;
				speed = 1.5f;
				jumpForce = 1.8f;
				jumpCount = 0;
			}
		}

		if (isGrounded && !wasGrounded)
		{
			jumpCount = 0;

			int tp = PlayerPrefs.GetInt("try.pass");
			if (tp == 1)
			{
				if (tryPassCount == 0)
				{
					PlayerPrefs.SetString("dialog.engine.topic", "FirstTryToPassWallDialog");
					PlayerPrefs.SetInt("dialog.engine.start", 1);
					PlayerPrefs.Save();
					transform.position = spawnPoint.position;
				}if (tryPassCount == 1)
				{
					transform.position = spawnPoint.position;
				}
				if (tryPassCount == 2)
				{
					PlayerPrefs.SetString("dialog.engine.topic", "LastTryToPassWallDialog");
					PlayerPrefs.SetInt("dialog.engine.start", 1);
					PlayerPrefs.Save();

					PlayerPrefs.SetInt("glitch.cutscene.start", 1);
					PlayerPrefs.Save();
					transform.position = spawnPoint.position;
				}
				tryPassCount++;
				PlayerPrefs.SetInt("try.pass", 0);
				PlayerPrefs.Save();
			}

			if (tryingJump && sceneName == "Level1" && jumpTries == 1)
			{
				PlayerPrefs.SetString("dialog.engine.topic", "SecondJumpDialog");
				PlayerPrefs.SetInt("dialog.engine.start", 1);
				PlayerPrefs.Save();
				tryingJump = false;
				jumpTries += 1;
				speed = 2f;
				jumpForce = 5f;
			}
			if (tryingJump && sceneName == "Level1" && jumpTries == 2)
			{
				PlayerPrefs.SetString("dialog.engine.topic", "ThirdJumpDialog");
				PlayerPrefs.SetInt("dialog.engine.start", 1);
				PlayerPrefs.Save();
				jumpTries += 1;
				tryingJump = false;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Platform"))
		{
			if (!isOnPlatform)
			{
				isOnPlatform = true;
				currentPlatform = collision.gameObject;
			}
			return;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Platform"))
		{
			isOnPlatform = false;
			currentPlatform = null;
			lastPlatformPos = null;
			return;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Death"))
		{
			transform.position = spawnPoint.position;
			rb.linearVelocity = Vector2.zero;
			deatchSound.Play();
			return;
		}
		if (collision.CompareTag("Finish"))
		{
			SceneManager.LoadScene("Level2");
			return;
		}
		if (collision.CompareTag("ActionTrigger"))
		{
			ActionTrigger trigger = collision.gameObject.GetComponent<ActionTrigger>();
			if (trigger.actionId == "dialog")
			{
				if (trigger.actionPayload != "GlitchFoundDialog")
					collision.gameObject.SetActive(false);
				else collision.gameObject.tag = "Untagged";

				PlayerPrefs.SetString("dialog.engine.topic", trigger.actionPayload);
				PlayerPrefs.SetInt("dialog.engine.start", 1);
				PlayerPrefs.Save();
			}
			return;
		}
	}

	public enum AnimStates
	{
		idle,
		moving
	}

	private IEnumerator DelayedAction(float delay, System.Action action)
	{
		yield return new WaitForSeconds(delay);  // Ожидаем задержку
		action?.Invoke();  // Выполняем действие
	}
}
