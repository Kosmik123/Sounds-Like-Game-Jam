using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class BulletType : ScriptableObject
{
	[field: SerializeField, FormerlySerializedAs("bulletType")]
	public Sprite BulletSprite { get; private set; }

	[field: SerializeField]
	public AudioClip Sound { get; private set; }

	[field: SerializeField]
	public Color Color { get; private set; }
}
