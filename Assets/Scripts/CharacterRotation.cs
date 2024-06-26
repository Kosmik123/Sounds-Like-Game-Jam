using Bipolar.SpritesetAnimation;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
	public const float SmallSpeed = 0.001f;

	[SerializeField]
	private Rigidbody2D rigidbody;

	[SerializeField]
	private SpriteRenderer spriteRenderer;
	[SerializeField]
	private SpritesetAnimator spritesetAnimator;

	[SerializeField]
	private Sprite frontSprite;
	[SerializeField]
	private Sprite backSprite;

	private void Update()
	{
		var velocity = rigidbody.velocity;
		float xSpeed = velocity.x;
		if (xSpeed < -SmallSpeed)
		{
			spriteRenderer.flipX = true;
		}
		else if (xSpeed > SmallSpeed)
		{
			spriteRenderer.flipX = false;
		}

		float ySpeed = velocity.y;
		if (ySpeed < -SmallSpeed)
		{
			spritesetAnimator.CurrentAnimationIndex = 0;
		}
		else if (ySpeed > SmallSpeed)
		{
			spritesetAnimator.CurrentAnimationIndex = 1;
		}
	}
}
