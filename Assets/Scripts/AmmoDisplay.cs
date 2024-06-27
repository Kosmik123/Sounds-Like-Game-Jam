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
	[SerializeField]
	private GameObject selection;

	[SerializeField, ReadOnly]
	private BulletType bulletType;
	public BulletType BulletType
	{
		get => bulletType;
		set
		{
			bulletType = value;
			image.sprite = bulletType.BulletSprite;
			image.color = label.color = bulletType.Color;
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

	[SerializeField, ReadOnly]
	private bool isSelected;
	public bool IsSelected
	{
		get => isSelected;
		set
		{
			isSelected = value;
			selection.SetActive(isSelected);
		}
	}
}
