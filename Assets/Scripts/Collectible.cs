using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    public string collectibleType;
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
            other.GetComponent<Inventaire>().AddCollectible(collectibleType);
            emitLight.enabled = false;
            Destroy(gameObject);
        }   
    }
}
