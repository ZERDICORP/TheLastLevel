using UnityEngine;

public class Hero : MonoBehaviour
{
	[SerializeField] private float speed = 3f;
	[SerializeField] private float jumpForce = 8f;
	[SerializeField] private int maxJumpCount = 2;

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
		CheckGround(); // перенесли сюда

		if (isGrounded && rb.linearVelocity.y <= 0.01f)
			State = States.idle;

		if (Input.GetButton("Horizontal"))
			Run();

		if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
			Jump();
	}

	private void Run()
	{
		if (isGrounded)
			State = States.run;

		Vector3 dir = transform.right * Input.GetAxis("Horizontal");

		transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

		sprite.flipX = dir.x < 0.0f;
	}

	private void Jump()
	{
		rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
		rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		jumpCount++;
		State = States.jump;
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

		if (!isGrounded)
		{
			State = States.jump;
		}
	}


	public enum States
	{
		idle,
		run,
		jump
	}
}
