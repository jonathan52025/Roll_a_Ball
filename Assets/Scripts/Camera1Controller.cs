using UnityEngine;

public class Camera1Controller : MonoBehaviour
{
    public GameObject player;
    public float cameraOffset = 10f;
    private bool toggle = false;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        // Toggle between two viewing angles
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggle = !toggle;
        }

        if (toggle)
        {
            transform.position = new Vector3(playerPos.x, playerPos.y + cameraOffset + 5, playerPos.z - 5);
            transform.eulerAngles = new Vector3(75, 0, 0);
        }
        else
        {
            transform.position = new Vector3(playerPos.x, playerPos.y + cameraOffset, playerPos.z - cameraOffset);
            transform.eulerAngles = new Vector3(45, 0, 0);
        }
    }
}
