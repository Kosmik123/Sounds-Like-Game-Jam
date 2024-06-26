using Bipolar;
using Bipolar.Pooling;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public Rigidbody2D Rigidbody => this.GetRequired(ref _rigidbody);

	private IObjectPool<Bullet> sourcePool;

	[SerializeField]
	private float lifeDuration = 10;

	[SerializeField]
	private float speed;
	public float Speed
	{
		get => speed;
		set => speed = value;
	}

	public void Shoot(Vector2 direction)
	{
		gameObject.SetActive(true);
		Rigidbody.velocity = direction.normalized * speed;
		Invoke(nameof(Kill), lifeDuration);
	}

	private void OnDisable()
	{
		Rigidbody.velocity = Vector2.zero;
	}

	public void Init(IObjectPool<Bullet> bulletsPool)
	{
		sourcePool = bulletsPool;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Kill();
	}

	private void Kill()
	{
		gameObject.SetActive(false);
		sourcePool.Release(this);
	}
}
