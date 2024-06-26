using Bipolar.Pooling;
using UnityEngine;

public class BulletsPool : ObjectPool<Bullet> { }

public class PlayerShooting : MonoBehaviour
{
	[SerializeField]
	private Bullet bulletPrototype;

	private BulletsPool bulletsPool;

	private void Awake()
	{
		bulletsPool = gameObject.AddComponent<BulletsPool>();
		bulletsPool.hideFlags |= HideFlags.HideInInspector;
		bulletsPool.Prototype = bulletPrototype;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		var bullet = bulletsPool.Get();
		bullet.Init(bulletsPool);
		bullet.transform.position = transform.position;

		var screenMousePosition = Input.mousePosition;
		var worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
		var direction = worldMousePosition - transform.position;
		bullet.Shoot(direction);
	}
}
