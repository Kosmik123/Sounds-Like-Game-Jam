using UnityEngine;
using Bipolar;

[RequireComponent(typeof(Rigidbody2D))]
public class WallsSlippery : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public Rigidbody2D Rigidbody => this.GetRequired(ref _rigidbody);

	private ContactPoint2D[] contacts = new ContactPoint2D[4];

	private void FixedUpdate()
	{
		int contactsCount = Rigidbody.GetContacts(contacts);

	}
}
