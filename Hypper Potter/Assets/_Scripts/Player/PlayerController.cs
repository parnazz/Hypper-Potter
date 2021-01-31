using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Params")]
    [SerializeField]
    private float movementSpeed = 0f;

    [SerializeField]
    private float speedDecrease = 0f;

    [SerializeField]
    private float timeToGainSpeed = 0f;

    [Space]
    [Header("Input Params")]
    [SerializeField]
    private float horizontalAxisMultiplier = 1.1f;

    [SerializeField]
    private float verticalAxisMultiplier = 1.1f;

    private FixedJoystick joystick;
    private InputController inputController;

    private bool bCanMove = false;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        inputController = new InputController(joystick);

        EventManager.onGameStarted += EnableMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bCanMove) { return; }

        Vector3 movementVector = inputController.HandleInput() * Time.deltaTime;
        transform.Translate(movementVector.x * horizontalAxisMultiplier,
                            movementVector.y * verticalAxisMultiplier,
                            movementSpeed * Time.deltaTime);
    }

    private void EnableMovement()
    {
        bCanMove = !bCanMove;
    }

    private void OnCollisionEnter(Collision collision)
    {
        movementSpeed -= speedDecrease;
        StartCoroutine(GainSpeedCoroutine());
    }

    private IEnumerator GainSpeedCoroutine()
    {
        yield return new WaitForSeconds(timeToGainSpeed);
        movementSpeed += speedDecrease;
    }

    private void OnDisable()
    {
        EventManager.onGameStarted -= EnableMovement;
    }
}
