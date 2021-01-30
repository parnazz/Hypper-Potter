using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0f;

    [SerializeField]
    private float speedDecrease = 0f;

    [SerializeField]
    private float timeToGainSpeed = 0f;

    private FixedJoystick joystick;
    private InputController inputController;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        inputController = new InputController(joystick);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = inputController.HandleInput() * Time.deltaTime;
        transform.Translate(movementVector.x,
                            movementVector.y,
                            movementSpeed * Time.deltaTime);
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
}
