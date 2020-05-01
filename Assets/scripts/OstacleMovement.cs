using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstacleMovement : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.left * FindObjectOfType<obstacleSpawner>().obstacleSpeedMovement * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
