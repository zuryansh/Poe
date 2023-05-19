using UnityEngine;
using UnityEngine.Events;

public class SkillPickup : MonoBehaviour
{
    [SerializeField] PlayerSkills skillType;
    [SerializeField] UnityEvent<GameObject> OnPickedUp;
    [SerializeField] AudioClip clip;


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            SoundManager.inst.PlaySound(clip);
            other.GetComponent<Player>().Data.AddSkill(skillType);
            OnPickedUp?.Invoke(gameObject);
        }   
    }


}
