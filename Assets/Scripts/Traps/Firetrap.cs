using System;
using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{

    [SerializeField] private float damage;


    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend; //changes the trap's color.

    private bool triggered; // when trap gets triggered
    private bool active;  // when trap is active and can hurt the player


    [Header("SFX")]
    [SerializeField] private AudioClip FireTrapSound;


    //Stores a reference to the player's Health component while the player is inside the trap.
    private Health playerHealth;

    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    //Runs when another collider enters the trap's trigger area.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());    //coroutine a special method that allows you to pause execution and resume it later without freezing the game
            }
            if(active) {

                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {

        SoundManager.instance.PlaySound(FireTrapSound); //PLay fire sound
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;  //turn the sprite red to notify the player
            
        //Wait for the delay , activate trap, turn on animation ,return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;  //turn the sprite white back to normal
        active = true;
        anim.SetBool("activate", true);


        //Wait for x seconds , deactivate trap and reset all variables and animator 
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activate", false);
    }

}



