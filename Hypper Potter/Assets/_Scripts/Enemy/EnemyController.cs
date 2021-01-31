using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("General Params")]
    [SerializeField]
    private float movementSpeed = 0f;

    [SerializeField]
    private float speedDecrease = 0f;

    [SerializeField]
    private float timeToGainSpeed = 0f;

    [SerializeField]
    private float horizontalAxisMultiplier = 1.1f;

    [SerializeField]
    private float verticalAxisMultiplier = 1.1f;

    [Space]
    [Header("AI Decisions Params")]
    [SerializeField]
    private float minTimeToStraightMovement = 1f;

    [SerializeField]
    private float maxTimeToStraightMovement = 5f;

    [SerializeField]
    private float minTimeToStrafeMovement = 1f;

    [SerializeField]
    private float maxTimeToStrafeMovement = 5f;

    private Vector3 movementVector;

    private bool bCanMove = false;
    private bool bIsVulnerableToSlow = true;

    // Start is called before the first frame update
    void Start()
    {
        movementVector = Vector3.zero;

        EventManager.onGameStarted += EnableMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bCanMove) { return; }

        transform.Translate(movementVector.x * horizontalAxisMultiplier * Time.deltaTime,
                            movementVector.y * verticalAxisMultiplier * Time.deltaTime,
                            movementSpeed * Time.deltaTime);
    }

    private void EnableMovement()
    {
        bCanMove = !bCanMove;
        StartCoroutine(MoveStraightCoroutine());
    }

    // В течение определенного времени враг летит прямо, затем принимает решение повернуть
    private IEnumerator MoveStraightCoroutine()
    {
        movementVector = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(minTimeToStraightMovement, maxTimeToStraightMovement));
        StartCoroutine(StrafeMovementCoroutine());
    }

    // В течение определенного времени враг поворачивает, затем принимавет решение лететь прямо
    private IEnumerator StrafeMovementCoroutine()
    {
        movementVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        yield return new WaitForSeconds(Random.Range(minTimeToStrafeMovement, maxTimeToStrafeMovement));
        StartCoroutine(MoveStraightCoroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!bIsVulnerableToSlow) { return; }

        movementSpeed -= speedDecrease;
        StartCoroutine(GainSpeedCoroutine());
    }

    private IEnumerator GainSpeedCoroutine()
    {
        bIsVulnerableToSlow = false; // Временно даем неуязвимость к столкновениям
        yield return new WaitForSeconds(timeToGainSpeed);
        movementSpeed += speedDecrease;
        bIsVulnerableToSlow = true;
    }

    private void OnDisable()
    {
        EventManager.onGameStarted -= EnableMovement;
    }
}
