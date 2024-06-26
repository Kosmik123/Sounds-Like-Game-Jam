using Bipolar.SpritesetAnimation;
using NaughtyAttributes;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
	public const float SmallSpeed = 0.001f;

	[SerializeField]
	private Rigidbody2D rigidbody;

	[SerializeField]
	private bool flipScale = false;
	public bool FlipScale => flipScale;

	[SerializeField, HideIf(nameof(FlipScale))]
	private SpriteRenderer spriteRenderer;
	[SerializeField, ShowIf(nameof(FlipScale))]
	private Transform graphic;
	
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
			if (flipScale)
			{
				var localScale = graphic.localScale;
				localScale.x = -1;
				graphic.localScale = localScale;
			}
			else
			{
				spriteRenderer.flipX = true;
			}
		}
		else if (xSpeed > SmallSpeed)
		{
			if (flipScale)
			{
				var localScale = graphic.localScale;
				localScale.x = 1;
				graphic.localScale = localScale;
			}
			else
			{
				spriteRenderer.flipX = false;
			}
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
