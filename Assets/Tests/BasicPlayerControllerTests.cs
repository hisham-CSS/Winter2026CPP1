using System.Collections;
using NUnit.Framework;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.TestTools;

public class BasicPlayerControllerTests
{
    private PlayerController controller;
    private GameObject player;

    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<SpriteRenderer>();
        player.AddComponent<Animator>();

        controller = player.AddComponent<PlayerController>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void PlayerController_InitJumpForceIsCorrect()
    {
        float expectedJumpForce = 10f;
        float actualJumpforce = controller.jumpForce;

        Assert.AreEqual(expectedJumpForce, actualJumpforce);
    }

    [Test]
    public void PlayerController_ActivatesJumpForceChange_ChangesJumpForce()
    {
        float expectedJumpForce = controller.powerupJumpForce;

        controller.JumpForceChange();
        float changedJumpForce = controller.jumpForce;

        Assert.AreEqual(expectedJumpForce, changedJumpForce);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerController_JumpForcePowerupTime_WorksCorrectly()
    {

        float initalTimer = 0.5f;
        float jumpForce = controller.jumpForce;

        controller.initialPowerupDuration = initalTimer;

        controller.JumpForceChange();

        float changedJumpForce = controller.jumpForce;

        Assert.AreEqual(initalTimer, controller.PowerupDuration(), 0.2f);
        Assert.AreNotEqual(jumpForce, changedJumpForce);

        controller.JumpForceChange();

        float changedJumpForce2 = controller.jumpForce;

        Assert.AreEqual(initalTimer + initalTimer, controller.PowerupDuration(), 0.4f);
        Assert.AreEqual(changedJumpForce, changedJumpForce2);

        yield return new WaitForSecondsRealtime(controller.PowerupDuration());

        Assert.AreEqual(0, controller.PowerupDuration());
    }
}
