﻿using Bipolar.Pooling;
using NaughtyAttributes;
using UnityEngine;

public class BulletsPool : ObjectPool<Bullet> { }

public class PlayerShooting : MonoBehaviour
{
	public const float SmallNumber = 0.001f;

	[SerializeField]
	private Bullet bulletPrototype;
	private BulletsPool bulletsPool;

	[SerializeField]
	private Transform bulletsOrigin;

	[Header("Animation")]
	[SerializeField]
	private Transform arm;
	[SerializeField]
	private Transform body;

	[SerializeField, ReadOnly]
	private Vector2 direction;

	[SerializeField]
	private Transform crosshair;

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
		worldMousePosition.z = transform.position.z;
		direction = worldMousePosition - transform.position;
		float armAngle = Vector2.SignedAngle(Vector2.right, direction);

		if (crosshair)
			crosshair.position = worldMousePosition;

		var bodyScale = body.localScale;
		if (direction.x < -SmallNumber)
			bodyScale.x = -1;
		else if (direction.x > SmallNumber)
			bodyScale.x = 1;
		body.localScale = bodyScale;
		
		if (Mathf.Sign(arm.lossyScale.x) < 0)
			armAngle += 180;
				
		arm.rotation = Quaternion.AngleAxis(armAngle, Vector3.forward);
	}

	public void Shoot(BulletType bulletType)
	{
		var bullet = bulletsPool.Get();
		bullet.Init(bulletsPool);
		bullet.BulletType = bulletType;
		bullet.transform.position = bulletsOrigin.position;
		bullet.Shoot(direction);
	}
}
