using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject collectibleObject;
    public Light emitLight;
    public AudioClip pickupSound;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Inventaire>().AddCollectible(collectibleObject);
            emitLight.enabled = false;
        if(audioSource != null && pickupSound != null)
            {
            audioSource.PlayOneShot(pickupSound);
            }
        }  
        Destroy(gameObject, pickupSound.length);
    }
}
