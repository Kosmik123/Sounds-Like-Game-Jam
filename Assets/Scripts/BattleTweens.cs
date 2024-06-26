using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BattleTweens : MonoBehaviour
{
	[SerializeField]
	private AudioSource music;
	[SerializeField]
	private RectTransform[] blackScreens;
	public float screensMovingTime = 4;

	public Image enemy;
	public float enemyTargetX;

	public Image player;
	public float playerTargetY;

	public float charactersMoveDuration = 4;

	public GameObject messageWindow;

	private bool hasStarted = false;

	private void Start()
	{
		player.DOFade(0, 0.01f);
		enemy.DOFade(0, 0.01f);
	}

	private void Update()
	{
		if (hasStarted == false && Input.GetKeyDown(KeyCode.Space))
		{
			hasStarted = true;
			StartSequence();
		}
	}

	private void StartSequence()
	{
		music.Play();
		var sequence = DOTween.Sequence();
		foreach (var screen in blackScreens)
		{
			sequence.Join(screen.DOScaleY(0, screensMovingTime));
		}

		sequence.Append(enemy.transform.DOLocalMoveX(enemyTargetX, charactersMoveDuration));
		sequence.Join(enemy.DOFade(1, charactersMoveDuration));

		sequence.Join(player.transform.DOLocalMoveX(playerTargetY, charactersMoveDuration));
		sequence.Join(player.DOFade(1, charactersMoveDuration));
		sequence.onComplete = () => messageWindow.SetActive(true);

		sequence.Play();
	}
}
