using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmeController : MonoBehaviour
{
    public List<Arme> toutesLesArmes; // Toutes les armes disponibles dans le jeu
    public List<Arme> inventaire; // Armes possédées par le joueur
    private int indexArmeActuelle = 0;
    private Arme armeActuelle;
    private int ammoActuel;
    private int totalAmmo;
    private bool estEntrainDeTirer = false;
    private Camera playerCamera;
    public TextMeshProUGUI ammoText;
    public Transform pointDeMontageArme;
    public WeaponHud weaponHud;
    public GameObject flamePrefab;
    public Transform flashPoint;
    public GameObject impactEffectPrefab;

    public bool EstEntrainDeTirer = true;



    // Start is called before the first frame update
    void Start()
    {
        weaponHud = FindObjectOfType<WeaponHud>();
        GameObject pistolet = GameObject.Find("pistol_001");
        if (pistolet != null)
        {
            pistolet.SetActive(true);
        }

        GameObject smg = GameObject.Find("Smg (1)");
        if (smg != null)
        {
            smg.SetActive(false);
        }

        GameObject rifle = GameObject.Find("rifle_001 (1)");
        if (rifle != null)
        {
            rifle.SetActive(false);
        }

        playerCamera = Camera.main;
        Arme[] armesDansScene = FindObjectsOfType<Arme>();
        foreach (Arme arme in armesDansScene)
        {
            toutesLesArmes.Add(arme);
        }

        if (toutesLesArmes.Count > 0)
        {
            AjouterArme(toutesLesArmes[0]);
            EquipArme(0);
        }
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangerArme(-1);
            Debug.Log("Changer d'arme");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangerArme(1);
            Debug.Log("Changer d'arme");
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ChangerArme(1);
            Debug.Log("Changer d'arme");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ChangerArme(-1);
        }

        if (!EstEntrainDeTirer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !estEntrainDeTirer && ammoActuel > 0)
        {
            Tirer(playerCamera);
        }

        if (ammoActuel == 0 && !estEntrainDeTirer)
        {
            ammoText.text = "RECHARGER EN APPUYANT SUR LA TOUCHE R";
        }


        if (Input.GetKeyDown(KeyCode.R) && ammoActuel < armeActuelle.maxMagazineCapacity && totalAmmo > 0)
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
            int ammoNeeded = armeActuelle.maxMagazineCapacity - ammoActuel;
            if (totalAmmo >= ammoNeeded)
            {
                totalAmmo -= ammoNeeded;
                ammoActuel = armeActuelle.maxMagazineCapacity;
                Debug.Log("Rechargement de " + ammoNeeded + " balles");
            }
            else
            {
                ammoActuel += totalAmmo;
                totalAmmo = 0;
                Debug.Log("Rechargement de " + ammoActuel + " balles");
            }
            UpdateAmmoUI();
        }
    }
    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + ammoActuel + " / " + totalAmmo;
    }
    public void Tirer(Camera playerCamera)
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
            if (flamePrefab != null && pointDeMontageArme != null)
            {
                GameObject flame = Instantiate(flamePrefab, flashPoint.position, flashPoint.rotation);
                Destroy(flame, 1.0f );
            }
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

                    int degats = 25;
                    if (hit.collider.CompareTag("Tete"))
                    {
                        // Multiplier les dégâts par 4 si c'est un tir à la tête
                        degats *= 4;
                        Debug.Log("Touche à la tête pour " + degats);
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
                GameObject impactEffect = Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactEffect, 0.5f);
            }
            else
            {
                Debug.Log("Rien touché");
            }
        }
    }

    public void AjouterArme(Arme nouvelleArme)
    {
        foreach (var arme in inventaire)
        {
            if (arme.nom == nouvelleArme.nom)
            {
                return; // Ne pas ajouter de doublons
            }
        }
        nouvelleArme.currentMagazineAmmo = nouvelleArme.maxMagazineCapacity;
        nouvelleArme.currentTotalAmmo = nouvelleArme.maxAmmoCapacity;
        inventaire.Add(nouvelleArme);
        nouvelleArme.gameObject.SetActive(false); 
    }

    void EquipArme(int index)
    {
        if (armeActuelle != null) {
            armeActuelle.currentMagazineAmmo = ammoActuel;
            armeActuelle.currentTotalAmmo = totalAmmo;
        }
        foreach (Arme arme in inventaire)
        {
            if (arme != null && arme.gameObject != null)
            {
                arme.gameObject.SetActive(false);
            }
        }
        armeActuelle = inventaire[index];
        if (armeActuelle != null && armeActuelle.gameObject != null)
        {
            armeActuelle.gameObject.SetActive(true);
            armeActuelle.transform.SetParent(pointDeMontageArme);
            armeActuelle.transform.localPosition = Vector3.zero;
            armeActuelle.transform.localRotation = Quaternion.identity;
            
            weaponHud.UpdateWeaponUI(armeActuelle.imageArme, armeActuelle.nom);

        }
        ammoActuel = armeActuelle.currentMagazineAmmo;
        totalAmmo = armeActuelle.currentTotalAmmo;
        UpdateAmmoUI();
    }
    void ChangerArme(int direction)
    {
        indexArmeActuelle += direction;
        if (indexArmeActuelle < 0)
        {
            indexArmeActuelle = inventaire.Count - 1;
        }
        else if (indexArmeActuelle >= inventaire.Count)
        {
            indexArmeActuelle = 0;
        }
        EquipArme(indexArmeActuelle);
    }
    public void AddAmmo(int amount)
    {
        totalAmmo += amount;
        if (totalAmmo > 100)
        {
            totalAmmo = 100;
        }
        UpdateAmmoUI();
        Debug.Log("Munitions ajoutées: " + amount);
    }
    public int GetTotalAmmo()
    {
        return totalAmmo;
    }
}


