using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class BulletType : ScriptableObject
{
	[field: SerializeField, FormerlySerializedAs("bulletType")]
	public Sprite BulletSprite { get; private set; }

	[field: SerializeField, FormerlySerializedAs("bulletType")]
	public AudioClip Sound { get; private set; }
}
