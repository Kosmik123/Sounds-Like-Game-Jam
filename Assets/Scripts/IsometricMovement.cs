using UnityEngine;
using Bipolar;
using Bipolar.Input;

[RequireComponent(typeof(Rigidbody2D))]
public class IsometricMovement : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public Rigidbody2D Rigidbody => this.GetRequired(ref _rigidbody);

	[SerializeField]
	private float speed = 5;
	public float Speed => speed;

	[SerializeField]
	private Serialized<IMoveInputProvider> inputProvider;

	private Vector2 direction;

	[SerializeField]
	private float verticalMovementModifier = 0.5f;

	private void Update()
	{
		direction = inputProvider.Value.GetMotion();
		if (direction.sqrMagnitude > 1)
			direction.Normalize();
	}

	private void FixedUpdate()
	{
		var velocity = direction * speed;
		velocity.y *= verticalMovementModifier;
		Rigidbody.velocity = velocity;
	}
}
