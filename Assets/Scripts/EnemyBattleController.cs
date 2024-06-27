using NaughtyAttributes;
using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
	public const string AttackParam = "Attack";

	[SerializeField]
	private Animator bossAnimator;

	[Header("Attack")]
	[SerializeField]
	private float minAttackDelay;
	[SerializeField]
	private float maxAttackDelay;

	public float AttackDelay => Random.Range(minAttackDelay, maxAttackDelay);

	[SerializeField, ReadOnly]
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
		bossAnimator.SetInteger(AttackParam, randomAttack);
	}

	// called from animation event
	private void FinishAttack()
	{
		isAttacking = false;
		Invoke(nameof(Attack), AttackDelay);
	}
}
