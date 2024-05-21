using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHud : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage weaponImage;
    public TextMeshProUGUI weaponNameText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        weaponNameText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateWeaponUI(Texture newWeaponTexture, string newWeaponName)
    {
        weaponImage.texture = newWeaponTexture;
        weaponNameText.text = newWeaponName;
    } 
}
