using UnityEngine;
using TMPro;

public class Player1Controller : PlayerBase
{
    public float speed = 10f;
    public TextMeshProUGUI uiText;
    public float tagCooldown = 1f;
    private float nextTagTime = 0f;
    public Camera playerCamera;

    private Rigidbody rb;
    private Player2Controller player2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player2 = GameObject.Find("Player 2").GetComponent<Player2Controller>();
    }

    void FixedUpdate()
    {
        // Movement (WASD)
        float h = 0f;
        float v = 0f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;
        if (Input.GetKey(KeyCode.A)) h = -1f;
        if (Input.GetKey(KeyCode.D)) h = 1f;

        Vector3 move = new Vector3(h, 0, v).normalized * speed;
        rb.AddForce(move);

        // Update label
        if (uiText != null)
        {
            uiText.text = "Player 1 - " + (isTagger ? "Tagger" : "Runner");

            if (playerCamera != null)
            {
                uiText.transform.LookAt(playerCamera.transform);
                uiText.transform.Rotate(0, 180, 0);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player 2" && isTagger && Time.time >= nextTagTime)
        {
            // Swap roles
            isTagger = false;
            player2.isTagger = true;

            // Apply cooldown to both players
            nextTagTime = Time.time + tagCooldown;
            player2.SetNextTagTime(Time.time + tagCooldown);

            Debug.Log("Player 1 tagged Player 2!");
        }
    }

    public void SetNextTagTime(float time)
    {
        nextTagTime = time;
    }
}
