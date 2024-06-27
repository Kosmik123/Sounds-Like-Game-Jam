using NaughtyAttributes;
using UnityEngine;

public class AmmoSource : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private BulletType note;

	[SerializeField]
	private int ammoCapacity;
	[SerializeField]
	private int currentAmmoCount;
	[SerializeField, Tag]
	private string playerTag;

	[SerializeField]
	private float oneAmmoRefillDuration;
	private float timer;

	private void Awake()
	{
		currentAmmoCount = ammoCapacity;
		spriteRenderer.sprite = note.BulletSprite;
		spriteRenderer.color = note.Color;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			var shooting = collision.GetComponentInChildren<PlayerShootingController>();
			if (shooting)
			{
				shooting.AddBullets(note, currentAmmoCount);
				currentAmmoCount = 0;
				spriteRenderer.color = Color.black;
			}
		}
	}

	private void Update()
	{
		if (currentAmmoCount < ammoCapacity)
		{
			timer += oneAmmoRefillDuration * Time.deltaTime;
			if (timer > oneAmmoRefillDuration) 
			{
				currentAmmoCount++;
				timer = 0;
				float progress = (float)currentAmmoCount / ammoCapacity;
				spriteRenderer.color = Color.Lerp(Color.black, Color.white, progress);
			}
		}
	}
}
