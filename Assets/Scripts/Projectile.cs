using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private Animator anim;
    private BoxCollider2D boxCollider;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (hit) return;  //hit true then rest of the code won't run
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;    
        boxCollider.enabled=true;

        float localSclaeX = transform.localScale.x;
        if (Mathf.Sign(localSclaeX) != _direction)
            localSclaeX = -localSclaeX;

        transform.localScale  = new Vector3(localSclaeX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
