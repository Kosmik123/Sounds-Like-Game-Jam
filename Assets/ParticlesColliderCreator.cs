using NaughtyAttributes;
using UnityEngine;

public class ParticlesColliderCreator : MonoBehaviour
{
	[SerializeField, Layer]
	private int colliderLayer;

	[SerializeField]
	private Vector3 offset = Vector3.up;

	private void Start()
	{
		ParticlesColliderCreator newCollider = Instantiate(this, transform.position + offset, transform.rotation, transform);
		var newColliderObject = newCollider.gameObject;
		Destroy(newCollider);
		newColliderObject.layer = colliderLayer;
	}
}

