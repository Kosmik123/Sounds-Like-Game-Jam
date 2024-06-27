using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    [SerializeField]
    private float radius;

    void Start()
    {
        transform.localPosition = Random.insideUnitCircle * radius;       
    }

	private void OnDrawGizmosSelected()
	{
        var center = Vector3.zero;
        if (transform.parent)
            center = transform.parent.position;
        Gizmos.DrawWireSphere(center, radius);
	}
}
