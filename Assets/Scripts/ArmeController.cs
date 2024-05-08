using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmeController : MonoBehaviour
{
    public int maxAmmoCapacity = 100;
    public int maxMagazineCapacity = 10;
    private int ammoActuel;
    private int totalAmmo;
    private bool estEntrainDeTirer = false;
    private Camera playerCamera;
    public TextMeshProUGUI ammoText;
   



    // Start is called before the first frame update
    void Start()
    {
        ammoActuel = maxMagazineCapacity;
        totalAmmo = maxAmmoCapacity;
        playerCamera = Camera.main;
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !estEntrainDeTirer)
        {
            Tirer(playerCamera);
        }

        if (ammoActuel == 0 && !estEntrainDeTirer)
        {
            ammoText.text = "RECHARGER EN APPUYANT SUR LA TOUCHE R";
        }

        //Vérifie si le joueur appuie sur la touche R pour recharger son arme lorsque le chargeur n'est pas plein et qu'il reste des munitions disponibles.

        if (Input.GetKeyDown(KeyCode.R) && ammoActuel < maxMagazineCapacity && totalAmmo > 0)
        {
            Reload();
        }

        if (totalAmmo == 0)
        {
            ammoText.text = "PLUS DE MUNITIONS";
        }
    }

    public void Reload()
    {
        if (totalAmmo > 0)
        {
            int ammoNeeded = maxMagazineCapacity - ammoActuel;
            if (totalAmmo >= ammoNeeded)
            {
                totalAmmo -= ammoNeeded;
                ammoActuel = maxMagazineCapacity;
            }
            else
            {
                ammoActuel += totalAmmo;
                totalAmmo = 0;
            }
             UpdateAmmoUI();
        }
    }
    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + ammoActuel + " / " + totalAmmo;
    }
    public void Tirer( Camera playerCamera)
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned.");
            return;
        }
        if (ammoActuel > 0)
        {
            ammoActuel--;
            UpdateAmmoUI();
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag);
                Debug.Log(hit.collider.name);

                Rigidbody rb = hit.rigidbody;
                if (rb != null)
                {
                    Debug.Log("rigidbody touche");

                    int degats = 10;
                    if (hit.collider.CompareTag("Tete"))
                    {
                        // Multiplier les dégâts par 4 si c'est un tir à la tête
                        degats *= 4;
                    }
                    else if (hit.collider.CompareTag("Bras") || hit.collider.CompareTag("Jambe") || hit.collider.CompareTag("Corps"))
                    {
                        // Infliger seulement 25% des dégâts de base si c'est un tir dans un membre
                        degats = Mathf.RoundToInt(degats * 0.25f);
                    }
                    // Vérifier si le composant Ennemi est attaché à l'objet touché
                    Ennemi ennemi = rb.GetComponent<Ennemi>();
                    if (ennemi != null)
                    {
                        ennemi.SubirDegats(degats);
                    }
                    else
                    {
                        Debug.Log("L'objet touché n'a pas de composant Ennemi.");
                    }
                }
            }
            else
            {
                Debug.Log("Rien touché");
            }
        }
    }
}
