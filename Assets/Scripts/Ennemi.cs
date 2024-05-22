using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{

    private int pointsDeVie = 100;
    public Transform target; 
    public float detectionRange; 
    public float attackRange = 20f;
    public float moveSpeed; 
    public float fireRate = 2f; 
    public float reloadTime = 3f; 
    public int magazineCapacity = 15; 
    public Transform firePoint;


    private float accuracy = 0.33f;
    private float lastFireTime; 
    private int currentAmmo; 
    private bool isReloading; 

    public void SubirDegats(int degats)
    {
        pointsDeVie -= degats;

        if (pointsDeVie <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazineCapacity;
        DifficulteJeu difficulté = FindObjectOfType<DifficulteJeu>();
        if (difficulté != null)
        {
            detectionRange = difficulté.detectionRange;
            moveSpeed = difficulté.movementSpeed;
            accuracy = difficulté.accuracy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Si le personnage est assez proche et visible
        if (distanceToTarget <= detectionRange)
        {
            Debug.Log("Cible détectée!");
         
            if (distanceToTarget <= attackRange)
            {
                FaceTarget();
                Attack();
            }
            else 
            {
                Debug.Log("Se déplacer vers la cible");
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        // Vérifier si le temps écoulé depuis le dernier tir est supérieur à la cadence de tir
        if (Time.time - lastFireTime >= 1f / fireRate)
        {
            // Tirer avec une probabilité de 33% de toucher
            if (Random.value <= accuracy)
            {
                Fire();
            }

            // Mettre à jour le temps du dernier tir
            lastFireTime = Time.time;
        }

        // Si le chargeur est vide, commencer à recharger
        if (currentAmmo <= 0 && !isReloading)
        {
            Debug.Log("Début du rechargement");
            isReloading = true;
            Invoke("Reload", reloadTime);
        }
    }

    private void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit))
        {
            if (hit.transform == target)
            {
                hit.transform.GetComponent<PlayerController>().SubirDegats(3);
                Debug.Log("Tir réussi! Dégâts infligés: 3");
            }
        }

        currentAmmo--;
    }

    private void Reload()
    {
        Debug.Log("Rechargement terminé");
        currentAmmo = magazineCapacity;
        isReloading = false;
    }


}
