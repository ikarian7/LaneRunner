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
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Crouch();
            } 
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Stand();
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

    public void Crouch()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        this.gameObject.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.y);
        controller.height = 1;
    }

    public void Stand()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        this.gameObject.transform.position = new Vector3(this.transform.position.x, 1.08f, this.transform.position.y);
        controller.height = 2;
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
            StartCoroutine(Slowdown());
            if (lives <= 0)
            {
                StartCoroutine(Pause());
            }
            else
            {
                StartCoroutine(Slowdown());
            }
        }

        if (other.CompareTag("MovingObstacle"))
        {
            lives -= 1;
            Destroy(other.gameObject);
            if (lives <= 0)
            {
                StartCoroutine(Pause());
            }
            else 
            { 
                StartCoroutine(Slowdown()); 
            }
        }

        if (other.CompareTag("Coin"))
        {
            coins += 1;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            StartCoroutine(Pause()); 
        }
    }

    IEnumerator Slowdown()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        SceneManager.LoadScene(0);
    }

}
