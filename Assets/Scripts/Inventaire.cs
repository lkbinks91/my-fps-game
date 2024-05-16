using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    // Start is called before the first frame update
    private List<string> collectibles = new List<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectible(string collectibleType)
    {
        collectibles.Add(collectibleType);
    }

    public bool HasKeyAndFolder()
    {
        return collectibles.Contains("cle") && collectibles.Contains("DossierA") && collectibles.Contains("DossierB");
    }

    public bool HasCollectible(string collectibleType)
    {
        return collectibles.Contains(collectibleType);
    }
}
