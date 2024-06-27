using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
	[SerializeField]
	private PlayerShooting playerShooting;
	[SerializeField]
	private BulletType[] availableTypes;
	[SerializeField]
	private List<BulletsCount> bulletsCounts;
	[SerializeField]
	private int currentBulletTypeIndex;

	[SerializeField]
	private float shootCooldown;
	private bool canShoot;


	private void Awake()
	{
		canShoot = true;
		bulletsCounts = new List<BulletsCount>();
		foreach(var bulletType in availableTypes)
			bulletsCounts.Add(new BulletsCount(bulletType, 0));
	}

	public void AddBullets(BulletType bulletType, int count)
	{
		int index = bulletsCounts.FindIndex(data => data.bulletType == bulletType);
		if (index >= 0)
			bulletsCounts[index].count += count;
		else
			bulletsCounts.Add(new BulletsCount(bulletType, count));
	}

	public int GetCount(BulletType bulletType)
	{
		int index = bulletsCounts.FindIndex(data => data.bulletType == bulletType);
		return index < 0 ? 0 : bulletsCounts[index].count;
	}

	private void Update()
	{
		if (canShoot && Input.GetMouseButton(0))
		{
			var data = bulletsCounts[currentBulletTypeIndex];
			if (data.count > 0)
			{
				playerShooting.Shoot(data.bulletType);
				canShoot = false;
				Invoke(nameof(EnableShooting), shootCooldown);
			}
			else
			{
				// empty weapon feedback
			}
		}
		else
		{
			float scroll = Input.mouseScrollDelta.y;
			int direction = (int)scroll;
			if (direction != 0)
			{
				currentBulletTypeIndex += direction;
				currentBulletTypeIndex += bulletsCounts.Count;
				currentBulletTypeIndex %= bulletsCounts.Count;
			}
		}
	}

	private void EnableShooting()
	{
		canShoot = true;
	}
}
