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

    public int lives = 3;
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
        Controls();
        Forward();
        Gravity();
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

    public void Jump()
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
            velocity.y = -gravity * 0.1f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            lives -= 1;
            Destroy(other.gameObject);
            if(lives <= 0)
            {
                StartCoroutine(Pause());
            }
        }

        if (other.CompareTag("Coin"))
        {
            coins += 1;
            Destroy(other.gameObject);
        }
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        SceneManager.LoadScene(0);
    }

}
