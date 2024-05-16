using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionSortie : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject porteSortie;
    public string[] objetsNecessaires;
    public Inventaire inventaireJoueur; 
    public TextMeshProUGUI victoireText;
    public Button retourMenuButton;
    public TextMeshProUGUI tempsEcouleText;

    private float tempsEcoule;

    private PlayerController playerController;
    private ArmeController armeController;
    private bool isVictoryDisplayed = false;



    void Start()
    {
        victoireText.gameObject.SetActive(false);
        retourMenuButton.gameObject.SetActive(false);
        tempsEcouleText.enabled = true;
        playerController = GameObject.FindObjectOfType<PlayerController>();
        armeController = GameObject.FindObjectOfType<ArmeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVictoryDisplayed)
        {
             tempsEcoule += Time.deltaTime;
             int minutes = Mathf.FloorToInt(tempsEcoule / 60);
             int secondes = Mathf.FloorToInt(tempsEcoule % 60);
            tempsEcouleText.text = string.Format("Temps: {0:00}:{1:00}", minutes, secondes);

      
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur est sur la porte de sortie");
            if (inventaireJoueur.HasKeyAndFolder())
            {
                Debug.Log("Le joueur possède tous les objets nécessaires");
                OuvrirPorteSortie();
            }
            else
            {
                AfficherMessageManqueObjets();
            }
        }
    }

    private void OuvrirPorteSortie()
    {
        AfficherVictoire();
    }

    private void AfficherMessageManqueObjets()
    {

        string message = "Il vous manque les objets suivants pour sortir: ";

     if (!inventaireJoueur.HasKeyAndFolder())
        {
            if( !inventaireJoueur.HasCollectible("cle"))
            {
                message += "la clé, ";
            }
            if (!inventaireJoueur.HasCollectible("DossierA"))
            {
                message += "le dossier A, ";
            }
            if (!inventaireJoueur.HasCollectible("DossierB"))
            {
                message += "le dossier B, ";
            }
            Debug.Log(message);
        }
    }
    private void AfficherVictoire()
    {
        isVictoryDisplayed = true;
        //Cursor.lockState = CursorLockMode.None;
       // Cursor.visible = true;
        victoireText.gameObject.SetActive(true);
        playerController.canMove = false;
        armeController.EstEntrainDeTirer = false;
        AfficherBoutonRetour();
        AfficherTempsEcoule();
    }
    private void AfficherTempsEcoule()
    {
        tempsEcouleText.enabled = true;
        tempsEcouleText.gameObject.SetActive(true);
    }



    private void RetourMenuPrincipal() {
      SceneManager.LoadScene("MainMenu");
    }

    private void AfficherBoutonRetour()
    {
        retourMenuButton.gameObject.SetActive(true);
        retourMenuButton.onClick.AddListener(RetourMenuPrincipal);

    } 
}
