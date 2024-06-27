[System.Serializable]
public class BulletsCount
{
	public BulletType bulletType;
	public int count;

	public BulletsCount(BulletType bulletType, int count)
	{
		this.bulletType = bulletType;
		this.count = count;
	}
}
