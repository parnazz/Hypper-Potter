using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
            playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 
                                        transform.position.y,
                                        playerTransform.position.z - distance);
    }
}
