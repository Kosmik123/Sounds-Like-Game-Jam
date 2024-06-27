using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossDamagable : MonoBehaviour
{
	public event System.Action<BulletType> OnDamaged;

	[SerializeField, Tag]
	private string bulletTag;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag(bulletTag))
		{
			if (collision.gameObject.TryGetComponent<Bullet>(out var bullet))
			{
				OnDamaged?.Invoke(bullet.BulletType);
			}
		}
	}
}
