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
        DifficulteJeu difficult� = FindObjectOfType<DifficulteJeu>();
        if (difficult� != null)
        {
            detectionRange = difficult�.detectionRange;
            moveSpeed = difficult�.movementSpeed;
            accuracy = difficult�.accuracy;
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
            Debug.Log("Cible d�tect�e!");
         
            if (distanceToTarget <= attackRange)
            {
                FaceTarget();
                Attack();
            }
            else 
            {
                Debug.Log("Se d�placer vers la cible");
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
        // V�rifier si le temps �coul� depuis le dernier tir est sup�rieur � la cadence de tir
        if (Time.time - lastFireTime >= 1f / fireRate)
        {
            // Tirer avec une probabilit� de 33% de toucher
            if (Random.value <= accuracy)
            {
                Fire();
            }

            // Mettre � jour le temps du dernier tir
            lastFireTime = Time.time;
        }

        // Si le chargeur est vide, commencer � recharger
        if (currentAmmo <= 0 && !isReloading)
        {
            Debug.Log("D�but du rechargement");
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
                Debug.Log("Tir r�ussi! D�g�ts inflig�s: 3");
            }
        }

        currentAmmo--;
    }

    private void Reload()
    {
        Debug.Log("Rechargement termin�");
        currentAmmo = magazineCapacity;
        isReloading = false;
    }


}
