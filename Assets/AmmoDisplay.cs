using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
	[SerializeField]
	private Image image;
	[SerializeField]
	private TextMeshProUGUI label;

	[SerializeField, ReadOnly]
	private BulletType bulletType;
	public BulletType BulletType
	{
		get => bulletType;
		set
		{
			bulletType = value;
			image.sprite = bulletType.BulletSprite;
		}
	}

	[SerializeField, ReadOnly]
	private int count;
	public int Count
	{
		get => count;
		set
		{
			count = value;
			label.SetText(count.ToString());
		}
	}
}
