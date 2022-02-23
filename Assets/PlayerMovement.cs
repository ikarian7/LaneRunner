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
    }

    void Forward()
    {
        Vector3 movement = Vector3.forward * (speed * Time.deltaTime);
        controller.Move(movement);

        distance += (speed * Time.deltaTime);

        camera.transform.position += movement;
    }
}
