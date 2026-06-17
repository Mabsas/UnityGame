using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; } // get ensure other file can access it  & private set ensure it can be updated only in this file
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;



    private void Awake()
    {
        currentHealth= startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        //decreasing health on collision
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            //player alive
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }

        else
        {
            if (!dead)
            {
                //player =dead
                anim.SetTrigger("die");

                //Deactivates all attached components
              foreach(Behaviour component in components)
                    component.enabled = false;

                dead = true;
            }
  
        }
    }

    //For increasing health
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    // On collision player gets a temporary invisibility
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);  // after first hit ignore collision btwn player and trap  to show the invisibilty effect so player doesnt lose helath during invisibility

        for(int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));  // Assigning time to each flas & multiply 2 beaces two flashes red and white 
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

        }

        Physics2D.IgnoreLayerCollision(8, 9, false);  // activates collision again
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);

    }


}   
