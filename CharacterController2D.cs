using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private GameObject topLeft;
	[SerializeField] private GameObject bottomRight;
	[SerializeField] private LayerMask whatIsGround;

	[SerializeField] private bool grounded;

	private Rigidbody2D rb;

	private bool direction = true;

	float move = 0f;
	bool jump = false;

	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		move = Input.GetAxisRaw("Horizontal") * 45f;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
	}

	private void FixedUpdate()
	{
		grounded = Physics2D.OverlapArea(topLeft.transform.position, bottomRight.transform.position, whatIsGround);

		Vector3 currentVelocity = rb.velocity;
		Vector3 targetVelocity = new Vector2(move * 10f * Time.fixedDeltaTime, rb.velocity.y);

		rb.velocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref velocity, .05f);

		if (move > 0 && direction == false)
		{
			FlipCharacter();
		} else if (move < 0 && direction == true)
		{
			FlipCharacter();
		}

		if (grounded == true && jump == true)
		{
			rb.AddForce(new Vector2(0f, 750f));
			jump = false;
		}
	}

	private void FlipCharacter()
	{
		direction = !direction;

		transform.localScale = new Vector3(transform.localScale.x * -1,
										   transform.localScale.y,
										   transform.localScale.z);
	}
}