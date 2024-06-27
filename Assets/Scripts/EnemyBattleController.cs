using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
	[SerializeField]
	private Animator bossAnimator;

	[Header("Attack")]
	[SerializeField]
	private float minAttackDelay;
	[SerializeField]
	private float maxAttackDelay;

	public float AttackDelay => Random.Range(minAttackDelay, maxAttackDelay);

	private bool isAttacking;
	public bool IsAttacking => isAttacking;

	private void Start()
	{
		Invoke(nameof(Attack), AttackDelay);
	}

	private void Attack()
	{
		isAttacking = true;
		int randomAttack = Random.Range(0, 3) + 1;
	}
}
