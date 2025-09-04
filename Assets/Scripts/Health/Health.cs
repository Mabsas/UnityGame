using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } // get ensure other file can access it  & private set ensure it can be updated only in this file
    private Animator anim;
    private bool dead;


    private void Awake()
    {
        currentHealth= startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {

        //decreasing health on collision
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            //player alive
            anim.SetTrigger("hurt");
            
        }

        else
        {
            if (!dead)
            {
                //player =dead
                anim.SetTrigger("die");
                //GetComponent<Animator>().enabled = false;
                dead = true;
            }
  
        }
    }

    //For increasing health
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }


}
