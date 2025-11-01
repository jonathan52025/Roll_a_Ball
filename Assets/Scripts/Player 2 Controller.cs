using UnityEngine;
using TMPro;

public class Player2Controller : PlayerBase
{
    public float speed = 10f;
    public TextMeshProUGUI uiText;
    public float tagCooldown = 1f;
    private float nextTagTime = 0f;
    public Camera playerCamera;

    private Rigidbody rb;
    private Player1Controller player1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player1 = GameObject.Find("Player 1").GetComponent<Player1Controller>();
    }

    void FixedUpdate()
    {
        // Movement (Arrow Keys)
        float h = 0f;
        float v = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) v = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) v = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) h = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) h = 1f;

        Vector3 move = new Vector3(h, 0, v).normalized * speed;
        rb.AddForce(move);

        // Update label
        if (uiText != null)
        {
            uiText.text = "Player 2 - " + (isTagger ? "Tagger" : "Runner");

            if (playerCamera != null)
            {
                uiText.transform.LookAt(playerCamera.transform);
                uiText.transform.Rotate(0, 180, 0);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player 1" && isTagger && Time.time >= nextTagTime)
        {
            // Swap roles
            isTagger = false;
            player1.isTagger = true;

            // Apply cooldown to both players
            nextTagTime = Time.time + tagCooldown;
            player1.SetNextTagTime(Time.time + tagCooldown);

            Debug.Log("Player 2 tagged Player 1!");
        }
    }

    public void SetNextTagTime(float time)
    {
        nextTagTime = time;
    }
}
