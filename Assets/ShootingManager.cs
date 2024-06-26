using Bipolar.Pooling;
using UnityEngine;

public class Bullet : MonoBehaviour
{

}

public class ShootingManager : MonoBehaviour
{
	private ComponentPool<Bullet> bulletsPool;

	private void Awake()
	{
		bulletsPool = gameObject.AddComponent<ComponentPool<Bullet>>();
	}

	private void Update()
	{
		var bullet = bulletsPool.Get();
		Debug.Log(bulletsPool.Count);
		bulletsPool.Release(bullet);
	}
}
