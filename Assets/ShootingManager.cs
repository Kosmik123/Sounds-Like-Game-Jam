using Bipolar.Pooling;
using UnityEngine;

public class Bullet : MonoBehaviour
{

}

public class BulletsPool : ComponentPool<Bullet> { }

public class ShootingManager : MonoBehaviour
{
	private BulletsPool bulletsPool;

	private void Awake()
	{
		bulletsPool = gameObject.AddComponent<BulletsPool>();
	}

	private void Update()
	{
		var bullet = bulletsPool.Get();
		Debug.Log(bulletsPool.Count);
		bulletsPool.Release(bullet);
	}
}
