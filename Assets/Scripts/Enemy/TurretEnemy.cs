using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class TurretEnemy : BaseEnemy
{
    Shoot shoot;

    [SerializeField] private float fireRate = 2f; // Shots per second
    private float timeSinceLastFire = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        shoot = GetComponent<Shoot>();

        if (fireRate <= 0)
        {
            fireRate = 2f;
            Debug.LogWarning("Fire rate must be greater than 0. Setting fire rate to default value of 2 shots per second.");
        }

        shoot.OnProjectileFired += () => timeSinceLastFire = Time.time;
    }

    //we could add the callback to the shoot script and subscribe to it in the turret enemy script - this ensures that the timing is correct for the time since last fired - however since this is one line of code - we can make a lambda function when we subsribe to the event instead of creating a separate function for it. This is a more concise way to achieve the same result without cluttering the code with an additional method that is only used once.
    //void OnProjectileFiredCallback()
    //{
    //    timeSinceLastFire = Time.time;
    //}

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            if (Time.time >= timeSinceLastFire + fireRate)
            {
                anim.SetTrigger("Fire");
            }
        }
    }
}
