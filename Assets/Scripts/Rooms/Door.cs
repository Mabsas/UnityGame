using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x)   //player position<door position
            {
                cam.MoveToNewRoom(nextRoom);   // player coming from left
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);  // player coming from right
            }
        }
    }
}
