using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Test
{
    [SetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //test to get all coins
    [UnityTest]
    public IEnumerator Coins()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        Assert.That(player.coins == 0);

        yield return new WaitForSecondsRealtime(4.5f);
        player.ChangeLane(-1);
        player.Crouch();

        yield return new WaitForSecondsRealtime(2f);
        player.Stand();

        yield return new WaitForSecondsRealtime(1f);
        player.ChangeLane(2);

        yield return new WaitForSecondsRealtime(2f);
        player.Jump();

        yield return new WaitForSecondsRealtime(4f);
        player.ChangeLane(-1);

        yield return new WaitForSecondsRealtime(4f);

        Assert.That(player.coins == 10);
    }

    //test to see if he loses all lives when going in a straight line
    [UnityTest]
    public IEnumerator LoseLive()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        Assert.That(player.lives == 3);

        yield return new WaitForSeconds(7f);
        Assert.That(player.lives < 3);
    }

    //test to see if dodge moving obstacle
    [UnityTest]
    public IEnumerator DodgeMoving()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        Assert.That(player.lives == 3);

        yield return new WaitForSecondsRealtime(4.5f);
        player.ChangeLane(-1);
        player.Crouch();
        yield return new WaitForSecondsRealtime(2f);

        Assert.That(player.lives == 3);
    }

    //test if crouching doesn't collide with a higher placed obstacle
    [UnityTest]
    public IEnumerator Crouching()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Assert.That(player.lives == 3);

        player.ChangeLane(-1);
        yield return new WaitForSecondsRealtime(5f);
        player.Crouch();
        yield return new WaitForSecondsRealtime(1f);
        player.Stand();
        yield return new WaitForSecondsRealtime(1f);
        Assert.That(player.lives == 3);
    }

    [UnityTest]
    public IEnumerator WinGame()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Assert.That(player.lives == 3);
        Assert.That(player.coins == 0);

        yield return new WaitForSecondsRealtime(4.5f);
        player.ChangeLane(-1);
        player.Crouch();

        yield return new WaitForSecondsRealtime(2f);
        player.Stand();

        yield return new WaitForSecondsRealtime(1f);
        player.ChangeLane(2);

        yield return new WaitForSecondsRealtime(3f);
        player.Jump();
        

        yield return new WaitForSecondsRealtime(4f);
        player.Jump();
        player.ChangeLane(-1);

        yield return new WaitForSecondsRealtime(2f);
        player.ChangeLane(1);

        yield return new WaitForSecondsRealtime(2f);

        Assert.That(player.lives > 0);
        Assert.That(player.coins == 10);

    }

}
