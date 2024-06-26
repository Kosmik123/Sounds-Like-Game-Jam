using Bipolar.Pooling;
using UnityEngine;

public class BulletsPool : ObjectPool<Bullet> { }

public class PlayerShooting : MonoBehaviour
{
	public const float SmallNumber = 0.001f;

	[SerializeField]
	private Bullet bulletPrototype;

	private BulletsPool bulletsPool;

	[SerializeField]
	private Transform arm;
	[SerializeField]
	private Transform body;

	private void Awake()
	{
		bulletsPool = gameObject.AddComponent<BulletsPool>();
		bulletsPool.hideFlags |= HideFlags.HideInInspector;
		bulletsPool.Prototype = bulletPrototype;
	}

	private void Update()
	{
		var screenMousePosition = Input.mousePosition;
		var worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
		var direction = worldMousePosition - transform.position;
		float armAngle = Vector2.SignedAngle(Vector2.right, direction);
		
		var bodyScale = body.localScale;
		if (direction.x < -SmallNumber)
			bodyScale.x = -1;
		else if (direction.x > SmallNumber)
			bodyScale.x = 1;
		body.localScale = bodyScale;
		
		if (Mathf.Sign(arm.lossyScale.x) < 0)
			armAngle += 180;
				
		arm.rotation = Quaternion.AngleAxis(armAngle, Vector3.forward);
		if (Input.GetMouseButtonDown(0))
		{
			Shoot(direction);
		}
	}

	private void Shoot(Vector2 direction)
	{
		var bullet = bulletsPool.Get();
		bullet.Init(bulletsPool);
		bullet.transform.position = transform.position;
		bullet.Shoot(direction);
	}
}
