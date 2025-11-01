using UnityEngine;

public class Rotater : MonoBehaviour
{
    public int speed;

    void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 40) * Time.deltaTime * speed);
    }
}
