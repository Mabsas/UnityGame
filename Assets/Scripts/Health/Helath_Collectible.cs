using UnityEngine;

public class Helath_Collectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ///detecting collisiion of player with heart object and calling addhealth to increase health
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false); // deactivating the heart object after collecting it
        }
    }
}
