using Bipolar;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBattleController : MonoBehaviour
{
	public const string AttackParam = "Attack";

	[SerializeField]
	private SpriteRenderer spriteRenderer;
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
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip[] attackSounds;

	[Header("Damage")]
	[SerializeField]
	private BossDamagable[] damagePoints;
	[SerializeField]
	private int maxHealth;
	[SerializeField, ReadOnly]
	private int health;
	[SerializeField]
	private AudioClip damageSound;
	[SerializeField]
	private AudioClip deathSound;

	[Header("Bullet Types")]
	[SerializeField, ReadOnly]
	private BulletType weakType;
	[SerializeField]
	private BulletType[] availableTypes;
	[SerializeField]
	private float minWeakTypeChangeDelay = 2;
	[SerializeField]
	private float maxWeakTypeChangeDelay = 6;
	public float WeakTypeChangeDelay => Random.Range(minWeakTypeChangeDelay, maxWeakTypeChangeDelay);

	[SerializeField]
	private BossMovement bossMovement;

	[SerializeField]
	private ParticleSystemsArray[] particleSystemsByAttack;

	[SerializeField]
	private UnityEvent onDeath;

	[System.Serializable]
	public class ParticleSystemsArray
	{
		public ParticleSystem[] array; 

		public void Start()
		{
			foreach (var system in array)
				system.Play();
		}

		public void Stop()
		{
			foreach (var system in array)
				system.Stop();
		}

	}

	private void OnEnable()
	{
		foreach (var damagable in damagePoints)
			damagable.OnDamaged += Damagable_OnDamaged;

		foreach (var systems in particleSystemsByAttack)
			systems.Stop();
	}

	private void Start()
	{
		health = maxHealth;
		ChangeWeakType();
		Invoke(nameof(PrepareToAttack), IdleDuration);
	}

	private void PrepareToAttack()
	{
		isAttacking = true;
		randomAttack = Random.Range(0, 3) + 1;
		Invoke(nameof(Attack), attackDelay);
		audioSource.PlayOneShot(attackSounds[randomAttack - 1]);
	}

	private int randomAttack = 1;
	private void Attack()
	{
		if (health > 0)
		{
			Invoke(nameof(FinishAttack), 2f);
			bossAnimator.SetInteger(AttackParam, randomAttack);
            for (int i = 0; i < particleSystemsByAttack.Length; i++)
            {
				int attackIndex = i + 1;
				if (attackIndex == randomAttack)
					particleSystemsByAttack[i].Start();
				else
					particleSystemsByAttack[i].Stop();
            }
        }
	}

	// called from animation event
	private void FinishAttack()
	{
		isAttacking = false;
		Invoke(nameof(PrepareToAttack), IdleDuration);
		foreach (var systems in particleSystemsByAttack)
			systems.Stop();
	}

	private void Damagable_OnDamaged(BulletType bulletType)
	{
		if (isAttacking == false)
			bossAnimator.SetTrigger("Damage");

		health -= bulletType == weakType ? 2 : 1;
		if (health <= 0)
		{
			audioSource.PlayOneShot(deathSound);
			bossAnimator.SetTrigger("Death");
			CancelInvoke();
			bossMovement.enabled = false;
			enabled = false;
			foreach (var systems in particleSystemsByAttack)
				systems.Stop();

			Invoke(nameof(InvokeDeathEvent), 2);
		}
		else
		{
			audioSource.PlayOneShot(damageSound);
		}
	}

	private void InvokeDeathEvent()
	{
		onDeath.Invoke();
	}

	private void ChangeWeakType()
	{
		weakType = availableTypes.GetRandom();
		spriteRenderer.color = weakType.Color;
		Invoke(nameof(ChangeWeakType), WeakTypeChangeDelay);
	}

	private void OnDisable()
	{
		foreach (var damagable in damagePoints)
			damagable.OnDamaged -= Damagable_OnDamaged;
	}
}
