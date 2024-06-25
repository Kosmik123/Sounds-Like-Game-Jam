using UnityEngine;
using Bipolar;
using Bipolar.Input;
using System.Diagnostics.Contracts;

[RequireComponent(typeof(Rigidbody2D))]
public class IsometricMovement : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public Rigidbody2D Rigidbody => this.GetRequired(ref _rigidbody);

	[SerializeField]
	private float speed = 5;
	[SerializeField]
	private Serialized<IMoveInputProvider> inputProvider;

	private void Update()
	{
		var direction = inputProvider.Value.GetMotion();
		if (direction.sqrMagnitude > 1)
			direction.Normalize();

		var velocity = direction * speed;
		velocity.y /= 2;
		Rigidbody.velocity = velocity;
	}
}
