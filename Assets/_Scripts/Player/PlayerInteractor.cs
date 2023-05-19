using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D) , typeof(PlayerInputHandler))]
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] PopoutText inputPrompt;
    [SerializeField] bool canInteract = true;
    PlayerInputHandler input;
    [SerializeField] GameObject interactable;
    
    void Start()
    {
        input = GetComponent<PlayerInputHandler>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Interactable"))
        {
            inputPrompt.Show(input.InteractInputButton);
            interactable = other.gameObject;
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Interactable"))
        {
            inputPrompt.Hide();
            interactable = null;

            canInteract = false;
        }
    }


    void Update()
    {
        
        if(canInteract && input.InteractInput  && interactable!=null) interactable.GetComponent<I_Interactable>().Interact(this);

    }

}
