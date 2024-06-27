using NaughtyAttributes;
using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
	public const string AttackParam = "Attack";

	[SerializeField]
	private Animator bossAnimator;

	[Header("Attack")]
	[SerializeField]
	private float minIdleDuration = 4;
	[SerializeField]
	private float maxIdleDuration = 7;

	public float IdleDuration => Random.Range(minIdleDuration, maxIdleDuration);

	[SerializeField, ReadOnly]
	private bool isAttacking;
	public bool IsAttacking => isAttacking;

	[SerializeField]
	private float attackDelay = 1;
	[SerializeField]
	private AudioSource attackAudioSource;
	[SerializeField]
	private AudioClip[] attackSounds;

	[Header("Damage")]
	[SerializeField]
	private BossDamagable[] damagePoints;
	[SerializeField]
	private BulletType weakType;

	private void OnEnable()
	{
		foreach (var damagable in damagePoints)
			damagable.OnDamaged += Damagable_OnDamaged;
	}

	private void Damagable_OnDamaged(BulletType bulletType)
	{
	}

	private void Start()
	{
		Invoke(nameof(PrepareToAttack), IdleDuration);
	}

	private void PrepareToAttack()
	{
		isAttacking = true;
		randomAttack = Random.Range(0, 3) + 1;
		Invoke(nameof(Attack), attackDelay);
		attackAudioSource.PlayOneShot(attackSounds[randomAttack]);
	}

	private int randomAttack = 1;
	private void Attack()
	{
		bossAnimator.SetInteger(AttackParam, randomAttack);
	}

	// called from animation event
	private void FinishAttack()
	{
		isAttacking = false;
		Invoke(nameof(PrepareToAttack), IdleDuration);
	}

	private void OnDisable()
	{
		foreach (var damagable in damagePoints)
			damagable.OnDamaged -= Damagable_OnDamaged;
	}
}
