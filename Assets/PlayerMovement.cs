using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float jump = 1f;

    [SerializeField]
    const float Gravity = 20f;

    public GameObject camera;
    public CharacterController controller;

    private int lane = 0;
    Vector3 velocity;

    public int lives = 0;
    public int coins = 0;
    public float distance = 0f;

    void Start()
    {
        StartCoroutine(Timer());
    }

    // Wait 3 seconds before starting the game
    IEnumerator Timer()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        Time.timeScale = 1;
    }

    void Update()
    {
        Forward();

        Controls();
    }

    void Forward()
    {
        Vector3 movement = Vector3.forward * (speed * Time.deltaTime);
        controller.Move(movement);

        distance += (speed * Time.deltaTime);

        camera.transform.position += movement;
    }

    void Controls()
    {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(1);
            }
    }

    public void ChangeLane(int direction)
    {
        if (lane >= -1 && lane <= 0 && direction > 0 || lane >= 0 && lane <= 1 && direction < 0)
        {
            Vector3 LaneMovement = new Vector3(direction * 2, 0, 0);

            controller.Move(LaneMovement);

            lane += direction;
        }
    }
}
