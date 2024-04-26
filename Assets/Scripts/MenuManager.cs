using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    enum menuState
    {
        MenuTitre, MenuPrincipal, MenuJouer, MenuOptions
    }

    [SerializeField]
    private menuState state;
    [SerializeField] 
    private GameObject MenuTitreContainer;
    [SerializeField] 
    private GameObject menuPrincipalContainer;
    [SerializeField]
    private GameObject menuJouerContainer;
    [SerializeField]
    private GameObject menuOptionsContainer;
    // Start is called before the first frame update
    void Start()
    {
        state = menuState.MenuTitre;
        menuPrincipalContainer.SetActive(false);
        menuJouerContainer.SetActive(false);
        menuOptionsContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OuvrirMenuPrincipal()
    {
        if (state == menuState.MenuTitre)
        {
            MenuTitreContainer.SetActive(false);
            menuPrincipalContainer.SetActive(true);
            state = menuState.MenuPrincipal;
        }
        else if (state == menuState.MenuPrincipal)
        {
            MenuTitreContainer.SetActive(true);
            menuPrincipalContainer.SetActive(false);
            state = menuState.MenuTitre;
        }
    }
    public void OuvrirMenuJouer()
    {
        if (menuPrincipalContainer.activeSelf)
        {
            menuPrincipalContainer.SetActive(false);
        }
        menuJouerContainer.SetActive(true);
        state = menuState.MenuJouer;
    }

    public void OuvrirMenuOptions()
    {
        menuOptionsContainer.SetActive(true);
        state = menuState.MenuOptions;
        menuPrincipalContainer.SetActive(false);
    }

    public void RetourMenuPrincipal()
    {
        if (menuJouerContainer.activeSelf)
        {
            menuJouerContainer.SetActive(false);
        }
        if (menuOptionsContainer.activeSelf)
        {
            menuOptionsContainer.SetActive(false);
        }
        menuPrincipalContainer.SetActive(true);
        state = menuState.MenuPrincipal;
    }

    // le bouton retour dans menuJouer nous ramene au menu principal

    public void RetourMenuJouer() {  
        if (menuJouerContainer.activeSelf)
        {
            menuJouerContainer.SetActive(false);
        }
        menuPrincipalContainer.SetActive(true);
        state = menuState.MenuPrincipal;
    }


}
