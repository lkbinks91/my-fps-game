using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    public GameObject checkA;
    public GameObject checkB;
    public GameObject checkCle;
    // Start is called before the first frame update
    private List<GameObject> collectibles = new List<GameObject>();
    void Start()
    {
        checkA.SetActive(false);
        checkB.SetActive(false);
        checkCle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectible(GameObject collectibleObject)
    {
        collectibles.Add(collectibleObject);

        if (collectibleObject.CompareTag("cle"))
        {
            checkCle.SetActive(true);
        }
        else if (collectibleObject.CompareTag("DossierA"))
        {
            checkA.SetActive(true);
        }
        else if (collectibleObject.CompareTag("DossierB"))
        {
            checkB.SetActive(true);
        }
    }

    public bool HasKeyAndFolder()
    {
        bool hasCle = collectibles.Exists(obj => obj.CompareTag("cle"));
        bool hasDossierA = collectibles.Exists(obj => obj.CompareTag("DossierA"));
        bool hasDossierB = collectibles.Exists(obj => obj.CompareTag("DossierB"));

        return hasCle && hasDossierA && hasDossierB;
    }

    public bool HasCollectible(GameObject collectibleObject)
    {
        return collectibles.Contains(collectibleObject);
    }
}
