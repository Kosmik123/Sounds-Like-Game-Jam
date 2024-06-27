using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    public float moveInterval = 3f;
    public float moveRadius = 5f;
    public float maxMoveDistance = 2f;
    public float moveSpeed = 2f;
    public Transform player;
    public float preferredDistanceToPlayer = 3f;

    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(MoveBoss());
    }

    private IEnumerator MoveBoss()
    {
        while (true)
        {
            Vector2 targetPosition = CalculateNewPosition();
            DetermineDirection(targetPosition);
            yield return StartCoroutine(MoveToPosition(targetPosition));
            yield return new WaitForSeconds(moveInterval);
        }
    }

    private Vector2 CalculateNewPosition()
    {
        Vector2 newPosition = (Vector2)transform.position;
        int attempts = 0;
        int maxAttempts = 100;

        do
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(0, moveRadius);
            Vector2 targetPositionAroundPlayer = (Vector2)player.position + randomDirection * randomDistance;

            Vector2 directionToTarget = (targetPositionAroundPlayer - (Vector2)transform.position).normalized;
            newPosition = (Vector2)transform.position + directionToTarget * Mathf.Min(maxMoveDistance, randomDistance);

            attempts++;
            if (attempts > maxAttempts)
            {
                newPosition = initialPosition;

				Debug.LogWarning("CalculateNewPosition: Max attempts reached, using fallback position.");
                break;
            }
        } while (Vector2.Distance(newPosition, player.position) > preferredDistanceToPlayer || Vector2.Distance(newPosition, initialPosition) > moveRadius);

        return newPosition;
    }

    private void DetermineDirection(Vector2 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            Debug.Log("Moving Right");
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Debug.Log("Moving Left");
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator MoveToPosition(Vector2 targetPosition)
    {
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

	private void OnDisable()
	{
        StopAllCoroutines();
	}
}