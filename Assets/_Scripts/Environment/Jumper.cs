using UnityEngine;

public class Jumper : MonoBehaviour
{

    [SerializeField] float jumpForce;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetYVelocity(jumpForce);
        }
    }
}
