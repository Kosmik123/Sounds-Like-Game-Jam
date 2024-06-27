using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
	[SerializeField]
	private PlayerShooting playerShooting;

	[SerializeField]
	private List<BulletsCount> bulletsCounts;

	[SerializeField]
	private BulletType currentBulletType;
	private BulletType CurrentBulletType => currentBulletType;

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
		if (Input.GetMouseButtonDown(0))
		{
			playerShooting.Shoot(CurrentBulletType);
		}
	}

}
