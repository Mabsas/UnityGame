using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f; // units per second
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        // Move the projectile
        transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;



        lifetime += Time.deltaTime;
        if (lifetime > 3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;

        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

    }

    // Call via AnimationEvent at end of explode animation
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // Reset projectile when reusing from pool
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Reset animator to idle
        anim.ResetTrigger("explode");
        anim.Play("Fireball_idle", 0, 0);

        // Adjust scale based on direction
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
