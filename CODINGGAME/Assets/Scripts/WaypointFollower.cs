using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private bool shouldRotate = false; // Flag to track if sprite should rotate

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

            // Set shouldRotate to true whenever any waypoint is reached by an enemy
            if (gameObject.CompareTag("Enemy"))
            {
                shouldRotate = true;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

        // Rotate sprite if shouldRotate is true
        if (shouldRotate)
        {
            RotateSprite();
            shouldRotate = false; // Reset the flag
        }
    }

    private void RotateSprite()
    {
        // Rotate the sprite by 180 degrees around the y-axis
        transform.Rotate(Vector3.up, 180f);
    }
}
