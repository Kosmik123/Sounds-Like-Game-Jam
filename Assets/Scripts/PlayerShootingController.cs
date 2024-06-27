using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
	public event System.Action OnCurrentBulletTypeChanged;
	public event System.Action<BulletType> OnBulletsCountChanged;

	[SerializeField]
	private PlayerShooting playerShooting;

	[SerializeField]
	private BulletType[] availableTypes;
	public IReadOnlyList<BulletType> AvailableTypes => availableTypes;

	[SerializeField]
	private List<BulletsCount> bulletsCounts;
	public IReadOnlyList<BulletsCount> BulletsCounts => bulletsCounts;

	[SerializeField]
	private int currentBulletTypeIndex;
	public int CurrentBulletTypeIndex => currentBulletTypeIndex;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private float shootCooldown;
	private bool canShoot;

	[SerializeField]
	private AudioClip emptySound;

	private void Awake()
	{
		canShoot = true;
		bulletsCounts = new List<BulletsCount>();
		foreach(var bulletType in availableTypes)
			bulletsCounts.Add(new BulletsCount(bulletType, 0));
	}

	public void AddBullets(BulletType bulletType, int count)
	{
		if (count < 0)
			return;

		int index = bulletsCounts.FindIndex(data => data.bulletType == bulletType);
		if (index >= 0)
			bulletsCounts[index].count += count;
		else
			bulletsCounts.Add(new BulletsCount(bulletType, count));
		OnBulletsCountChanged?.Invoke(bulletType);
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
				audioSource.PlayOneShot(data.bulletType.Sound);
				data.count--;
				canShoot = false;
				Invoke(nameof(EnableShooting), shootCooldown);
				OnBulletsCountChanged?.Invoke(data.bulletType);
			}
			else
			{
				audioSource.PlayOneShot(emptySound);
			}
		}
		else
		{
			float scroll = Input.mouseScrollDelta.y;
			int direction = (int)scroll;
			if (direction != 0)
			{
				currentBulletTypeIndex -= direction;
				currentBulletTypeIndex += bulletsCounts.Count;
				currentBulletTypeIndex %= bulletsCounts.Count;
				OnCurrentBulletTypeChanged?.Invoke();
			}
		}
	}

	private void EnableShooting()
	{
		canShoot = true;
	}
}
