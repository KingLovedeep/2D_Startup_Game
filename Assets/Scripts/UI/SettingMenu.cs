using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public List<Sprite> maleCharacterSprites = new(); 
    public List<Sprite> femaleCharacterSprites = new();
    private List<Image> characterImages = new(); 

    private Toggle maleToggle;
    private Toggle femaleToggle;
    private string userGender; 

    private InputField usernameInputField; 

    // Start is called before the first frame update
    void Start()
    {
        // gender option references 
        Transform genderOption = transform.Find("Gender").transform; 
        maleToggle = genderOption.GetChild(1).transform.GetChild(0).GetComponent<Toggle>(); 
        femaleToggle = genderOption.GetChild(2).transform.GetChild(0).GetComponent<Toggle>();

        // Character option references 
        Transform characterOption = transform.Find("Character").transform; 
        for(int i =1; i < characterOption.childCount;i++)
        {
            Image character = characterOption.GetChild(i).transform.GetComponent<Image>(); 
            characterImages.Add(character); 
        }

        // Username option reference
        usernameInputField = transform.Find("Username Field").transform.GetComponentInChildren<InputField>();

        // Load Usersaved Data
        GetUserSavedData(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FemaleToggleOff()
    {
        femaleToggle.isOn = false;
        ChangeCharacters(maleCharacterSprites); 
    }

    public void MaleToggleOff()
    {
        maleToggle.isOn = false;
        ChangeCharacters(femaleCharacterSprites); 
    }

    private void ChangeCharacters(List<Sprite> newSprites)
    {
        for(int i =0; i < characterImages.Count; i++)
        {
            Sprite newCharacterSprite = newSprites[i];
            characterImages[i].sprite = newCharacterSprite; 
        }
    }

    private void GetUserSavedData()
    {
        usernameInputField.text = PlayerPrefs.GetString("Username","Raaju");

        userGender = PlayerPrefs.GetString("Gender","Male"); 
        if(userGender == "Male")
        {
            maleToggle.isOn = true; 
        }
        else if(userGender == "Female")
        {
            femaleToggle.isOn = true;   
        }
    }

}
