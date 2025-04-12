using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed = 3f;
	[SerializeField] private float jumpForce = 3f;
	[SerializeField] private int maxJumpCount = 2;
	[SerializeField] private Transform spawnPoint; // 👈 добавил позицию спавна

	private bool isGrounded = false;
	private int jumpCount = 0;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	private States State
	{
		get { return (States)anim.GetInteger("state"); }
		set { anim.SetInteger("state", (int)value); }
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		CheckGround();

		if (isGrounded && rb.linearVelocity.y <= 0.01f)
			State = States.idle;

		if (Input.GetButton("Horizontal"))
			Run();

		if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
			Jump();
	}

	private void Run()
	{
		State = States.moving;

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

	private void CheckGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
		bool wasGrounded = isGrounded;
		isGrounded = false;

		foreach (var col in colliders)
		{
			if (col.CompareTag("ground"))
			{
				isGrounded = true;
				break;
			}
		}

		if (isGrounded && !wasGrounded)
		{
			jumpCount = 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("death"))
		{
			transform.position = spawnPoint.position;
			rb.linearVelocity = Vector2.zero;
			return;
		}
		if (collision.CompareTag("Finish"))
		{
			SceneManager.LoadScene("Level2");
			return;
		}
	}

	public enum States
	{
		idle,
		moving
	}
}
