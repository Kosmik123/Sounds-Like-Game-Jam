using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerShooting : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem bulletsParticleSystem;

	[SerializeField]
	private float bulletsSpeed = 10;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
	}

	public void Shoot()
	{
		var mouseScreenPosition = Input.mousePosition;
		var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

		var direction = mouseWorldPosition - transform.position;
		direction.z = 0;
		var emitParams = new EmitParams()
		{
			velocity = direction.normalized * bulletsSpeed,
		};
		bulletsParticleSystem.Emit(emitParams, 1);
	}
}
