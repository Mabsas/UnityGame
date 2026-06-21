using System.Security.Cryptography;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;

    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip ArrowSound;


    private void Attack()
    {
        SoundManager.instance.PlaySound(ArrowSound);
        cooldownTimer = 0;
        arrows[Findarrows()].transform.position = firepoint.position;
        arrows[Findarrows()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private int Findarrows()
    {
        for(int i = 0; i < arrows.Length; i++) 
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
