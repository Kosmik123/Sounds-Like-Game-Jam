using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingUI : MonoBehaviour
{
	private PlayerShootingController playerShootingController;

	[SerializeField]
	private AmmoDisplay ammoDisplayPrototype;

	private readonly Dictionary<BulletType, AmmoDisplay> ammoDisplaysDict = new Dictionary<BulletType, AmmoDisplay>();

	private void Awake()
	{
		playerShootingController = FindObjectOfType<PlayerShootingController>();
	}

	private void OnEnable()
	{
		playerShootingController.OnCurrentBulletTypeChanged += PlayerShootingController_OnCurrentBulletTypeChanged;
		playerShootingController.OnBulletsCountChanged += PlayerShootingController_OnBulletsCountChanged;
	}

	private void PlayerShootingController_OnBulletsCountChanged(BulletType bulletType)
	{
		if (ammoDisplaysDict.TryGetValue(bulletType, out var ammoDisplay))
		{
			ammoDisplay.Count = playerShootingController.GetCount(bulletType);
		}
	}

	private void Start()
	{
		foreach (var bulletType in playerShootingController.AvailableTypes)
		{
			var ammoDisplay = Instantiate(ammoDisplayPrototype, transform);
			ammoDisplay.BulletType = bulletType;
			ammoDisplaysDict.Add(bulletType, ammoDisplay);
		}

		var bulletCount = playerShootingController.BulletsCounts[playerShootingController.CurrentBulletTypeIndex];
		foreach (var ammoDisplay in ammoDisplaysDict.Values)
			ammoDisplay.IsSelected = ammoDisplay.BulletType == bulletCount.bulletType;
	}

	private void PlayerShootingController_OnCurrentBulletTypeChanged()
	{
		var bulletCount = playerShootingController.BulletsCounts[playerShootingController.CurrentBulletTypeIndex];
		foreach (var ammoDisplay in ammoDisplaysDict.Values)
			ammoDisplay.IsSelected = ammoDisplay.BulletType == bulletCount.bulletType;
	}

	private void OnDisable()
	{
		playerShootingController.OnCurrentBulletTypeChanged -= PlayerShootingController_OnCurrentBulletTypeChanged;
		playerShootingController.OnBulletsCountChanged -= PlayerShootingController_OnBulletsCountChanged;
	}
}
