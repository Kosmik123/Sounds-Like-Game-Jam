using UnityEngine;

[CreateAssetMenu]
public class BulletType : ScriptableObject
{
	[SerializeField]
	private Sprite bulletSprite;
	public Sprite BulletSprite => bulletSprite;
}
