using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkerEnemy : BaseEnemy
{
    public float xVel = 2f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Walk"))
        {
            //if (sr.flipX) rb.linearVelocityX = -xVel; 
            //else rb.linearVelocityX = xVel;

            //Ternary operator is a shorthand for an if-else statement. It takes three operands: a condition, a value to return if the condition is true, and a value to return if the condition is false. The syntax is: condition ? value_if_true : value_if_false;

            rb.linearVelocityX = (sr.flipX) ? -xVel : xVel;
        }
    }

    public override void TakeDamage(int damage, DamageType damageType = DamageType.Default)
    {
        AnimatorStateInfo stateInfo = anim.GetNextAnimatorStateInfo(0);

        if (stateInfo.IsName("Death") || stateInfo.IsName("Squish")) return;

        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.parent.gameObject, 0.5f);
            return;
        }

        base.TakeDamage(damage, damageType);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            anim.SetTrigger("Turn");
            sr.flipX = !sr.flipX;
        }
    }
}
