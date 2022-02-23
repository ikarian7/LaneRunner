using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float jump = 10f;

    [SerializeField]
    const float gravity = 20f;

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

    IEnumerator Timer()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        Time.timeScale = 1;
    }

    void Update()
    {
        Gravity();
        Controls();
        Forward();
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
    }

    public void ChangeLane(int direction)
    {
        if (lane >= -1 && lane <= 0 && direction > 0 || lane >= 0 && lane <= 1 && direction < 0)
        {
            Vector3 movement = new Vector3(direction * 2, 0, 0);

            controller.Move(movement);

            lane += direction;
        }
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            velocity.y = jump;
        }
    }

    void Gravity()
    {
        velocity.y -= gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            velocity.y = -gravity;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void NoLives()
    {
        SceneManager.LoadScene(0);
    }
}
