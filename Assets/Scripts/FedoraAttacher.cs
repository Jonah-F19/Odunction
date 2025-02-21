using UnityEngine;

public class FedoraFollow : MonoBehaviour
{
    public Transform player; // Assign the ball object in the Inspector
    public Vector3 offset; // Adjust the offset to position the hat properly

    void Update()
    {
        if (player != null)
        {
            // Follow the ball’s position but do NOT rotate with it
            transform.position = player.position + offset;
        }
    }
}