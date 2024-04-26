using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    enum menuState
    {
        MenuTitre, MenuPrincipal
    }

    [SerializeField]
    private menuState state;
    [SerializeField] 
    private GameObject MenuTitreContainer;
    [SerializeField] 
    private GameObject menuPrincipalContainer;
    // Start is called before the first frame update
    void Start()
    {
        state = menuState.MenuTitre;
        menuPrincipalContainer.SetActive(false);
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
}
