using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D rigidbody;

	[SerializeField]
	private float visualBreathingSpeedThreashold = 0.03f;
	[SerializeField]
	private float transitionSpeed = 0.1f;

	[Header("Breathing Settings")]
	[SerializeField]
	private float inhaleYScale = 1.1f;
	[SerializeField]
	private float exhaleYScale = 0.9f;
	[SerializeField]
	private float speed = 1;
	[SerializeField]
	private Transform scaledObject;

	private void Update()
	{
		float sqrBreathingSpeed = visualBreathingSpeedThreashold * visualBreathingSpeedThreashold;
		float sqrSpeed = rigidbody.velocity.sqrMagnitude;
		var scale = scaledObject.localScale;
		if (sqrSpeed < sqrBreathingSpeed)
		{
			float airAmount = (Mathf.Sin(Mathf.PI * Time.time * speed) + 1) / 2;
			float breathingPosition = Mathf.Lerp(exhaleYScale, inhaleYScale, airAmount);
			scale.y = Mathf.MoveTowards(scale.y, breathingPosition, TransitionDelta());
		}
		else if (sqrSpeed > sqrBreathingSpeed)
		{
			scale.y = Mathf.MoveTowards(scale.y, 1, TransitionDelta());
		}

		scaledObject.localScale = scale;

		float TransitionDelta() => transitionSpeed * Time.deltaTime;
	}
}
