using Bipolar.Pooling;
using UnityEngine;

public class BulletsPool : ComponentPool<Bullet> { }

public class ShootingManager : MonoBehaviour
{
	[SerializeField]
	public Bullet bulletPrototype;

	private BulletsPool bulletsPool;

	private void Awake()
	{
		bulletsPool = gameObject.AddComponent<BulletsPool>();
		bulletsPool.hideFlags |= HideFlags.HideInInspector;
		bulletsPool.Prototype = bulletPrototype;
	}

	private void Update()
	{
		var bullet = bulletsPool.Get();
		Debug.Log(bulletsPool.Count);
		bulletsPool.Release(bullet);
	}
}
