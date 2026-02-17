using UnityEngine;

public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f; // Shots per second
    private float timeSinceLastFire = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        if (fireRate <= 0)
        {
            fireRate = 2f;
            Debug.LogWarning("Fire rate must be greater than 0. Setting fire rate to default value of 2 shots per second.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            if (Time.time >= timeSinceLastFire + fireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }
    }
}
