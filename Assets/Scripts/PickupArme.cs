using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupArme : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject arme;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ArmeController armeController = other.GetComponent<ArmeController>();
            if (armeController != null)
            {
              armeController.AjouterArme(arme.GetComponent<Arme>());
                Debug.Log("Arme ramassée");
            }   
        }
    }
}
