using System.Collections;
using UnityEngine.Networking; 
using UnityEngine.UI; 
using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;

public class LoginManagment : MonoBehaviour
{
    [Header("LOGIN ANIMATION")]
    public string panelAnimParamName;
    private Animator loginAnim;
    public string displayMassageAnimParamName; 
    private Animator massagesAnim;

    [Header("MASSAGES SPRITES")]
    public List<Sprite> massagesSprites = new(); 

    [Header("AUTHENTICATION")]
    public InputField emailInputField;
    private string userEnteredEmail;

    [Space]
    [Header("OTP")]
    public InputField otpInputField;
    private string userEnteredOTP; 

    [Space]
    [Header("USERNAME")]
    public InputField usernameInputField;
    private string userEnteredName;

    [Space]
    [Header("GENDER")]
    public Toggle maleToggle;
    public Toggle femaleToggle;
    private string genderInString; 

    [Space]
    [Header("CHARACTER")]
    public Image characterImage;
    public List<Sprite> maleSprites = new(); 
    public List<Sprite> femaleSprites = new();
    private int spriteNumber = 0; 

    private SceneManagment sceneManager;
    private GameObject LoadingBar;

    // Start is called before the first frame update
    void Start()
    {
        loginAnim = GetComponent<Animator>();

        // Find 
        massagesAnim = GameObject.Find("Massages").GetComponent<Animator>(); 
        sceneManager = GameObject.Find("Scene Manager").GetComponent<SceneManagment>();
        LoadingBar = GameObject.Find("Loading Bar");

        // Show first massage 
        StartCoroutine(DisplayMassages(0)); 
    }


    #region ANIMATION
    public IEnumerator PanelChangingAnimation(int animNumber)
    {
        Enable_Disable_Inputs(false);
        loginAnim.SetInteger(panelAnimParamName, animNumber);

        yield return new WaitForSeconds(0.3f); 
        Enable_Disable_Inputs(true);
    }

    #endregion


    #region EMAIL 

    public IEnumerator EmailRegister()
    {
        // Remove massage panel 
        StartCoroutine(RemoveMassageCourotine());

        // Show Loading 
        Start_Or_Stop_Loading(true);

        //Disable All Inputs 
        Enable_Disable_Inputs(false); 

        // Register 
        userEnteredEmail = emailInputField.text;
        StartCoroutine(VerifyEnteredEmailAddress()); 

        yield return null; 
    }

    private IEnumerator VerifyEnteredEmailAddress()
    {
        yield return new WaitForSeconds(0.6f);
        Debug.Log("Verifaction");
        
        // Email entered 
        if (userEnteredEmail.Length != 0)
        {
            // Email not valid 
            if(!userEnteredEmail.Contains("@") || !userEnteredEmail.Contains("."))
            {
                StartCoroutine(DisplayMassages(2));

                // Stop Loading
                Start_Or_Stop_Loading(false);

                Enable_Disable_Inputs(true);
            }
            // Email vaild 
            else
            {
                StartCoroutine(SentOTPToUserEmail(userEnteredEmail));
            }
        }
        // No email enetered
        else
        {
            Debug.Log("No email entered"); 
            StartCoroutine(DisplayMassages(1));

            // Stop Loading
            Start_Or_Stop_Loading(false);

            Enable_Disable_Inputs(true);
        }
    }
    #endregion


    #region OTP

    private IEnumerator SentOTPToUserEmail(string emailID)
    {
        string sendOtpURL = $"https://app.startuped.ai/api/User/GetOTP?Email={emailID}"; 

        using UnityWebRequest sendOTPWebRequest = UnityWebRequest.Get(sendOtpURL) ;
        yield return sendOTPWebRequest.SendWebRequest(); 

        if(sendOTPWebRequest != null )
        {
            if(sendOTPWebRequest.result == UnityWebRequest.Result.Success)
            {
                string generatedOTP = sendOTPWebRequest.downloadHandler.text; 
                Debug.Log(generatedOTP);
                StartCoroutine(DisplayMassages(3));

                // Stop Loading
                Start_Or_Stop_Loading(false);

                // Email to otp panel 
                StartCoroutine(PanelChangingAnimation(1)); 
            }
            else
            {
                string massageToPrint = "Opss! Error retry please";
                Debug.Log(massageToPrint);

                // Stop Loading
                Start_Or_Stop_Loading(false);

                Enable_Disable_Inputs(true); 
            }
        }
    }

    public void OTPEntered()
    {
        // Disable inputs 
        Enable_Disable_Inputs(false); 

        // OTP
        userEnteredOTP = otpInputField.text; 

        if(userEnteredOTP.Length != 0)
        {
            // Show Loading
            Start_Or_Stop_Loading(true);

            StartCoroutine(VerifyEnteredOTP(userEnteredEmail, userEnteredOTP)); 
        }
        else
        {
            StartCoroutine(DisplayMassages(4));

            Enable_Disable_Inputs(true);
        }
    }

    private IEnumerator VerifyEnteredOTP(string emailID , string OTP)
    {
        string verifyOtpUrl = $"https://app.startuped.ai/api/User/VerifyOTP?EmailOrContactNumber={emailID}&OTP={OTP}"; 

        using UnityWebRequest verifyOTPRequest = UnityWebRequest.Get(verifyOtpUrl);
        yield return verifyOTPRequest.SendWebRequest();

        if(verifyOTPRequest != null)
        {
            if(verifyOTPRequest.result == UnityWebRequest.Result.Success)
            {
                StartCoroutine(DisplayMassages(6));

                //OTP Panel to username panel 
                StartCoroutine(PanelChangingAnimation(2));

                // Stop Loading
                Start_Or_Stop_Loading(false);
            }
            else
            {
                StartCoroutine(DisplayMassages(5));

                Enable_Disable_Inputs(true);

                // Stop Loading
                Start_Or_Stop_Loading(false);
            }
        }
    }

    #endregion

    #region GENDER
    public void GenderAuth()
    {
        Debug.Log("AUth FInish"); 
        Enable_Disable_Inputs(false);

        if (maleToggle.isOn && femaleToggle.isOn)
        {
            Debug.Log("Choose 1");
            Enable_Disable_Inputs(true);
        }
        else if(maleToggle.isOn)
        {
            Debug.Log("Playing");
            sceneManager.SaveUserGender("Male");
            genderInString = "Male"; 

            // Massage :- Playing
            StartCoroutine(DisplayMassages(10));

            // Gender auth to character selection
            StartCoroutine(PanelChangingAnimation(4));
        }
        else if(femaleToggle.isOn)
        {
            sceneManager.SaveUserGender("Female");
            genderInString = "Female"; 

            // Massage :- Playing
            StartCoroutine(DisplayMassages(10));

            // Gender auth to character selection
            StartCoroutine(PanelChangingAnimation(4));
        }

    }

    #endregion

    #region USERNAME

    public void UsernameEntered()
    {
        // Disable all inputs 
        Enable_Disable_Inputs(false); 
        
        // Username
        userEnteredName = usernameInputField.text;  

        if(userEnteredName.Length == 0)
        {
            StartCoroutine(DisplayMassages(7));

            Enable_Disable_Inputs(true);
        }
        else if(userEnteredName.Length != 0 && userEnteredName.Length < 4)
        {
            StartCoroutine(DisplayMassages(8));

            Enable_Disable_Inputs(true);
        }
        else if(userEnteredName.Length >= 10)
        {
            StartCoroutine(DisplayMassages(9));

            Enable_Disable_Inputs(true);
        }
        else
        {
            StartCoroutine(DisplayMassages(10));

            sceneManager.SaveUsername(userEnteredName); 

            // Username panel to gender auth
            StartCoroutine(PanelChangingAnimation(3));
        }
    }

    #endregion

    #region CHARACTER SELECTION 

    public void ChangeAtRight()
    {
        spriteNumber++;

        if (spriteNumber > 2)
        {
            spriteNumber = 0; 
        }

        ChangeCharacterSprite(spriteNumber);    
    }
    public void ChangeAtLeft()
    {
        spriteNumber--;
        if (spriteNumber < 0)
        {
            spriteNumber = 2; 
        }

        ChangeCharacterSprite(spriteNumber);    
    }

    private void ChangeCharacterSprite(int spriteIndex)
    {
        if (genderInString == "Male")
        {
            characterImage.sprite = maleSprites[spriteIndex];
        }
        else
        {
            characterImage.sprite = femaleSprites[spriteIndex];
        }
    }

    public void CharacterSelected()
    {
        // Massage :- Playing
        StartCoroutine(DisplayMassages(10));

        // Start loadig 
        StartCoroutine(LoadingWithWaitTime());
    }

    #endregion
    private IEnumerator DisplayMassages(int massageSpriteNumber)
    {
        Debug.Log("Massage"); 

        // Set Massage Sprite
        Image MassageDisplayer = massagesAnim.gameObject.GetComponent<Image>();
        MassageDisplayer.sprite = massagesSprites[massageSpriteNumber];

        // Play Animation
        massagesAnim.SetBool(displayMassageAnimParamName, true);

        // wait to stop 
        yield return new WaitForSeconds(0.25f);
        massagesAnim.speed = 0;
    }

    public void RemoveMassages()
    {
        Debug.Log("Removing Massage"); 
        StartCoroutine(RemoveMassageCourotine()); 
    }

    private IEnumerator RemoveMassageCourotine()
    {
        // Play Animation
        Debug.Log("Removed Massage"); 
        massagesAnim.speed = 1;

        yield return new WaitForSeconds(0.12f);

        massagesAnim.SetBool(displayMassageAnimParamName, false); 
    }

    private void Enable_Disable_Inputs(bool enableorDisable)
    {
        emailInputField.interactable = enableorDisable;
        usernameInputField.interactable = enableorDisable;
        otpInputField.interactable = enableorDisable;  
        maleToggle.interactable = enableorDisable;
        femaleToggle.interactable = enableorDisable;    
    }

    private IEnumerator LoadingWithWaitTime()
    {
        Start_Or_Stop_Loading(true);

        yield return new WaitForSeconds(2f);
        
        Start_Or_Stop_Loading(false);
        sceneManager.LoadAnyScene(2); 
    }

    private void Start_Or_Stop_Loading(bool start_or_Stop)
    {
        LoadingBar.transform.GetChild(0).gameObject.SetActive(start_or_Stop);
    }
}