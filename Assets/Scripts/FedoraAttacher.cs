using UnityEngine;

public class FedoraFollow : MonoBehaviour
{
    public Transform player; // Assign the ball object in the Inspector
    public Vector3 offset; // Adjust the offset to position the hat properly
    public float paralalx = 1f;
    public bool locky = false;

    void Update()
    {
        if (player != null)
        {
            Vector3 pos = (player.position * paralalx) + offset;
            if (locky) {
                transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            } else {
                transform.position = pos;
            }
        }
    }
}