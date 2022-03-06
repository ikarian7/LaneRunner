using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 5;
    public int lane = 0;

    void Update()
    {
        if (transform.position.x >= 2)
        {
            lane = 1;
        }
        else if (transform.position.x <= -2)
        {
            lane = -1;
        }

        Vector3 velocity = new Vector3(speed, 0, 0);

        switch (lane)
        {
            case -1:
                velocity = new Vector3(speed, 0, 0);
                break;
            case 1:
                velocity = new Vector3(-speed, 0, 0);
                break;
        }

        transform.position += velocity * Time.deltaTime;
    }
}

