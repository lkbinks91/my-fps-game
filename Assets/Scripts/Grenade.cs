using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private int grenadeActuelle;
    public int maxGrenadeCapacity = 1;
    public TextMeshProUGUI grenadeText;
    private Rigidbody rb;
    public bool aEteLancee = false;
    public GameObject prefabExplosion;
    public Grenade grenadePrefab;
    public Transform spawnPoint;
    public float grenadeForce = 25f;



    void Start()
    {
        grenadeActuelle = 0;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (grenadeActuelle > 0)
            {
                Grenade grenade = Instantiate(grenadePrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.AddForce(spawnPoint.forward * grenadeForce, ForceMode.Impulse);
                grenade.aEteLancee = true;
                grenadeActuelle--; 
                UpdateGrenadeUI();
            }
            else
            {
                Debug.Log("Vous n'avez pas de grenades à lancer.");
            }
        }

    }

    public void PickupGrenade(Transform playerCameraTransform)
    {
        if (grenadeActuelle < maxGrenadeCapacity)
        {
            grenadeActuelle++;
            UpdateGrenadeUI();
        }
        else
        {
            Debug.Log("Vous avez atteint la capacité maximale de grenades");
        }
    }

    void UpdateGrenadeUI()
    {
        if (grenadeText != null)
        {
            grenadeText.text = grenadeActuelle.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player touched the grenadee");
            PickupGrenade(collision.transform);
        }

       if (!collision.collider.CompareTag("Player") && aEteLancee)
        {
            Debug.Log("Grenade touched something else");
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("Boom");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider hit in colliders)
        {          
            if (hit.CompareTag("Ennemi"))
            {
                // Calculer les dégâts en fonction de la distance
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                int damage = Mathf.Max(Mathf.RoundToInt(-100 * distance + 200), 0);
                

                // Infliger des dégâts à l'ennemi
               hit.GetComponent<Ennemi>().SubirDegats(damage);
                Debug.Log("Ennemi touché pour " + damage + " dégâts.");
            }
        }
        Destroy(gameObject);
    }
}
