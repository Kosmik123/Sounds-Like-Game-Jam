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
	}

	private void Start()
	{
		foreach (var bulletType in playerShootingController.AvailableTypes)
		{
			var ammoDisplay = Instantiate(ammoDisplayPrototype, transform);
			ammoDisplay.BulletType = bulletType;
			ammoDisplaysDict.Add(bulletType, ammoDisplay);
		}
	}

	private void PlayerShootingController_OnCurrentBulletTypeChanged()
	{
		var bulletCount = playerShootingController.BulletsCounts[playerShootingController.CurrentBulletTypeIndex];
		if (ammoDisplaysDict.TryGetValue(bulletCount.bulletType, out var ammoDisplay))
		{
			ammoDisplay.Count = bulletCount.count;
		}
	}

	private void OnDisable()
	{
		playerShootingController.OnCurrentBulletTypeChanged -= PlayerShootingController_OnCurrentBulletTypeChanged;
	}
}
