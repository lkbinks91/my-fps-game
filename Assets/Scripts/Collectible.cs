using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject collectibleObject;
    public Light emitLight;
    void Start()
    {
        
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
            Destroy(gameObject);
        }   
    }
}
