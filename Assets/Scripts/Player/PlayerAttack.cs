using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.2f; // time between shots
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs; // pool of projectiles

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity; // allow shooting immediately

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Use GetMouseButton for rapid fire while holding the button
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        int fireballIndex = FindFireball();
        GameObject fireball = fireballs[fireballIndex];

        fireball.transform.position = firePoint.position;
        fireball.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy) // find unused projectile
                return i;
        }

        // fallback: overwrite the first one
        return 0;
    }
}
