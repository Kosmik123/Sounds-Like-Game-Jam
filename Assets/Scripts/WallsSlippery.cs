using UnityEngine;
using Bipolar;

[RequireComponent(typeof(IsometricMovement))]
public class WallsSlippery : MonoBehaviour
{
	private IsometricMovement _movement;
	public IsometricMovement Movement => this.GetRequired(ref _movement);

	private ContactPoint2D[] contacts = new ContactPoint2D[4];

	private void FixedUpdate()
	{
		int contactsCount = Movement.Rigidbody.GetContacts(contacts);
		if (contactsCount > 0)
		{
			var direction = Movement.Rigidbody.velocity;
			for (int i = 0; i < contactsCount; i++)
			{
				var normal = contacts[i].normal;
				if (Vector2.Dot(direction, normal) < 0)
				{
					var tangent = Vector2.Perpendicular(normal);
					direction = Vector3.Project(direction, tangent);
				}
			}

			direction.y /= 2;
			if (direction.sqrMagnitude > 1)
				direction.Normalize();

			Movement.Rigidbody.velocity = Movement.Speed * direction;
		}
	}
}
