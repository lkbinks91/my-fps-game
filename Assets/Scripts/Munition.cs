using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision avec " + other.name);
        if (other.CompareTag("Player"))
        {
            ArmeController armeController = other.GetComponent<ArmeController>();
            if (armeController != null && armeController.GetTotalAmmo() < 100)
            {
                armeController.AddAmmo(amount);
                Debug.Log("Munitions ramassées" + amount);
                Destroy(gameObject);
            }
            else { 
                Debug.Log("Le joueur a déjà trop de munitions");
            }
        }
    }
}
