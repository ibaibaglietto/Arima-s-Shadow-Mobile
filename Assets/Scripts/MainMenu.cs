using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //The main menu
    private GameObject mainMenu;
    //The select character menu
    private GameObject selectCharacterMenu;
    //The settings menu
    private GameObject settingsMenu;
    //The controls menu
    private GameObject controlsMenu;
    //The change buttons position submenu
    private GameObject changeButtonPosMenu;
    //The credits menu
    private GameObject creditsMenu;
    //The how to play menu
    private GameObject howToPlayMenu;
    //The pages of the explanation
    [SerializeField] private GameObject[] howToPlayExplanation;
    //The buttons to change the explanation text
    private Button howToPlayPrevButton;
    private Button howToPlayNextButton;
    //the screen mode
    private bool fullScreen;
    //The framerate selector dropdown
    private TMP_Dropdown framerate;
    //The gameobjects that mark that a character has been selected
    private GameObject[] characterSelected;
    //The previous screen configuration
    private int prevW;
    private int prevH;
    private bool prevFS;
    private int prevFR;
    private float prevMaster;
    private float prevMusic;
    private float prevEffect;
    //The previous language
    private int prevLanguage;
    //The return configuration text
    private TextMeshProUGUI returnText;
    //The time the resolution changes were made
    private float resolutionTime;
    //The confirmation window
    private GameObject confirmationMenu;
    //The language selection menu
    private GameObject languageSelectionMenu;
    //The sound sliders
    private Slider masterSlider;
    private Slider musicSlider;
    private Slider effectsSlider;
    //The texts we need to translate
    private TextMeshProUGUI playText;
    private TextMeshProUGUI settingsText;
    private TextMeshProUGUI creditsText;
    private TextMeshProUGUI exitText;
    private TextMeshProUGUI howToPlayText;
    private TextMeshProUGUI settingsTitle;
    private TextMeshProUGUI frameRateText;
    private TextMeshProUGUI mainVolumeText;
    private TextMeshProUGUI musicText;
    private TextMeshProUGUI effectsText;
    private TextMeshProUGUI languageText;
    private TextMeshProUGUI changeControlsText;
    private TextMeshProUGUI returnSaveText;
    private TextMeshProUGUI returnNoSaveText;
    private TextMeshProUGUI confirmationText;
    private TextMeshProUGUI saveConfirmText;
    private TextMeshProUGUI languageChooseText;
    private TextMeshProUGUI saveLanguageText;
    private TextMeshProUGUI controlsTitleText;
    private TextMeshProUGUI returnToSettingsText;
    private TextMeshProUGUI creditsTitleText;
    private TextMeshProUGUI creditsReturnText;
    private TextMeshProUGUI howToPlayTitleText;
    private TextMeshProUGUI howToPlayReturnText;
    private TextMeshProUGUI howToPlayPage1;
    private TextMeshProUGUI howToPlayPage2;
    private TextMeshProUGUI howToPlayPage3;
    private TextMeshProUGUI howToPlayPage4;
    private TextMeshProUGUI howToPlayPage5;
    private TextMeshProUGUI howToPlayPage6;
    //The gameobjects of the buttons(0-> moveLeft, 1-> moveRight, 2-> joystick, 3-> jump, 4-> dash, 5-> pause)
    private GameObject[] buttons;
    private GameObject buttonPos;
    //An array to know the positions of the buttons
    private Vector2[] buttonsAnchoredPos;
    //The music source
    private AudioSource musicSource;
    //The language dropdowns
    private TMP_Dropdown languageDropdown;
    private TMP_Dropdown languageChooseDropdown;
    //An integer to know what page of the explanation menu we have opened
    private int howToPlayNumb = 0;
    //The controls 
    private TMP_Dropdown movementDropdown;
    //An int to know if the buttons are being changed and what button is being changed. 0-> no changes, 1-> moveLeft, 2-> moveRight, 3-> joystick, 4-> jump, 5-> dash, 6-> pause
    private int changingButtonPos = 0;
    //A vector 2 to know the new position of the button
    private Vector2 newButtonPos;
    private float newX;
    private float newY;
    //A matrix to know what buttons have another button in less than 362
    private int[][] neighbour;
    private int[] neighbourNumb;

    void Start()
    {
        //We put the resolution time to 0
        resolutionTime = 0.0f;
        //We find all the UI gameobjects
        mainMenu = GameObject.Find("Main");
        settingsMenu = GameObject.Find("Settings");
        selectCharacterMenu = GameObject.Find("SelectCharacter");
        controlsMenu = GameObject.Find("Controls");
        changeButtonPosMenu = GameObject.Find("ChangeButtonsPosition");
        creditsMenu = GameObject.Find("Credits");
        howToPlayMenu = GameObject.Find("HowToPlay");
        framerate = GameObject.Find("DropdownFramerate").GetComponent<TMP_Dropdown>();
        confirmationMenu = GameObject.Find("ConfirmMenu");
        languageSelectionMenu = GameObject.Find("LanguageMenu");
        returnText = GameObject.Find("ReturnText").GetComponent<TextMeshProUGUI>();
        playText = GameObject.Find("PlayButtonText").GetComponent<TextMeshProUGUI>();
        settingsText = GameObject.Find("SettingsButtonText").GetComponent<TextMeshProUGUI>();
        creditsText = GameObject.Find("CreditsButtonText").GetComponent<TextMeshProUGUI>();
        exitText = GameObject.Find("ExitButtonText").GetComponent<TextMeshProUGUI>();
        howToPlayText = GameObject.Find("HowToPlayButtonText").GetComponent<TextMeshProUGUI>();
        settingsTitle = GameObject.Find("SettingsTitleText").GetComponent<TextMeshProUGUI>();
        frameRateText = GameObject.Find("FramerateText").GetComponent<TextMeshProUGUI>();
        mainVolumeText = GameObject.Find("MainVolumeText").GetComponent<TextMeshProUGUI>();
        musicText = GameObject.Find("MusicText").GetComponent<TextMeshProUGUI>();
        effectsText = GameObject.Find("EffectsText").GetComponent<TextMeshProUGUI>();
        languageText = GameObject.Find("LanguageText").GetComponent<TextMeshProUGUI>();
        languageDropdown = GameObject.Find("DropdownLanguage").GetComponent<TMP_Dropdown>();
        changeControlsText = GameObject.Find("ChangeControlsText").GetComponent<TextMeshProUGUI>();
        returnSaveText = GameObject.Find("ReturnSaveText").GetComponent<TextMeshProUGUI>();
        returnNoSaveText = GameObject.Find("ReturnNoSaveText").GetComponent<TextMeshProUGUI>();
        confirmationText = GameObject.Find("ConfirmationText").GetComponent<TextMeshProUGUI>();
        saveConfirmText = GameObject.Find("SaveConfirmText").GetComponent<TextMeshProUGUI>();
        languageChooseText = GameObject.Find("LanguageChooseText").GetComponent<TextMeshProUGUI>();
        languageChooseDropdown = GameObject.Find("DropdownChooseLanguage").GetComponent<TMP_Dropdown>();
        saveLanguageText = GameObject.Find("SaveLanguageText").GetComponent<TextMeshProUGUI>();
        controlsTitleText = GameObject.Find("ControlsTitleText").GetComponent<TextMeshProUGUI>();
        returnToSettingsText = GameObject.Find("ReturnControlText").GetComponent<TextMeshProUGUI>();
        creditsTitleText = GameObject.Find("CreditsTitleText").GetComponent<TextMeshProUGUI>();
        creditsReturnText = GameObject.Find("CreditsReturnText").GetComponent<TextMeshProUGUI>();
        howToPlayTitleText = GameObject.Find("HowToPlayTitleText").GetComponent<TextMeshProUGUI>();
        howToPlayReturnText = GameObject.Find("HowToPlayReturnText").GetComponent<TextMeshProUGUI>();
        howToPlayPage1 = GameObject.Find("Page1Text").GetComponent<TextMeshProUGUI>();
        howToPlayPage2 = GameObject.Find("Page2Text").GetComponent<TextMeshProUGUI>();
        howToPlayPage3 = GameObject.Find("Page3Text").GetComponent<TextMeshProUGUI>();
        howToPlayPage4 = GameObject.Find("Page4Text").GetComponent<TextMeshProUGUI>();
        howToPlayPage5 = GameObject.Find("Page5Text").GetComponent<TextMeshProUGUI>();
        howToPlayPage6 = GameObject.Find("Page6Text").GetComponent<TextMeshProUGUI>();
        buttons = new[] { GameObject.Find("LeftArrow"), GameObject.Find("RightArrow"), GameObject.Find("Joystick"), GameObject.Find("Jump"), GameObject.Find("Dash"), GameObject.Find("Pause")};
        buttonPos = GameObject.Find("ButtonPos");
        howToPlayPrevButton = GameObject.Find("HowToPlayPrev").GetComponent<Button>();
        howToPlayNextButton = GameObject.Find("HowToPlayNext").GetComponent<Button>();
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        masterSlider = GameObject.Find("MainVolumeSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
        movementDropdown = GameObject.Find("DropdownMovement").GetComponent<TMP_Dropdown>();
        characterSelected = new[] {GameObject.Find("Character1Selected"), GameObject.Find("Character2Selected"), GameObject.Find("Character3Selected"), GameObject.Find("Character4Selected"), GameObject.Find("Character5Selected") };
        //We deactivate the parts of the menu that are not shown at the beginning
        settingsMenu.SetActive(false);
        selectCharacterMenu.SetActive(false);
        changeButtonPosMenu.SetActive(false);
        controlsMenu.SetActive(false); 
        creditsMenu.SetActive(false);
        for(int i=1; i<howToPlayExplanation.Length;i++) howToPlayExplanation[i].SetActive(false);
        howToPlayPrevButton.interactable = false;
        howToPlayMenu.SetActive(false);
        confirmationMenu.SetActive(false);
        for (int i = 0; i < characterSelected.Length; i++) characterSelected[i].GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.1144984f, 0.0f);
        //We initialize all the playerprefs
        //The selected character
        if (!PlayerPrefs.HasKey("selectedCharacter")) PlayerPrefs.SetInt("selectedCharacter", 0);
        //The resolution width   
        if (!PlayerPrefs.HasKey("resolutionW")) PlayerPrefs.SetInt("resolutionW", Screen.width);
        //The resolution height
        if (!PlayerPrefs.HasKey("resolutionH")) PlayerPrefs.SetInt("resolutionH", Screen.height);
        //The full screen mode: 0-> windowed, 1-> full screen
        if (!PlayerPrefs.HasKey("fullScreen")) PlayerPrefs.SetInt("fullScreen", 1);
        //The preferred refresh rate
        if (!PlayerPrefs.HasKey("framerate")) PlayerPrefs.SetInt("framerate", 0);
        //An int to save the language
        if (!PlayerPrefs.HasKey("language")) PlayerPrefs.SetInt("language", 0);
        //An int to save if the player has choosen any language
        if (!PlayerPrefs.HasKey("languageChoosen")) PlayerPrefs.SetInt("languageChoosen", 0);
        //A float to set the Master audio mixer
        if (!PlayerPrefs.HasKey("masterAudio")) PlayerPrefs.SetFloat("masterAudio", 1.0f);
        //A float to set the music audio mixer
        if (!PlayerPrefs.HasKey("musicAudio")) PlayerPrefs.SetFloat("musicAudio", 1.0f);
        //A float to set the effects audio mixer
        if (!PlayerPrefs.HasKey("effectsAudio")) PlayerPrefs.SetFloat("effectsAudio", 1.0f);
        //The controls
        if (!PlayerPrefs.HasKey("MovementMode")) PlayerPrefs.SetInt("MovementMode", 0);
        //The positions of the buttons
        if (!PlayerPrefs.HasKey("LeftButtonX")) PlayerPrefs.SetFloat("LeftButtonX", 0.08f);
        if (!PlayerPrefs.HasKey("LeftButtonY")) PlayerPrefs.SetFloat("LeftButtonY", 0.143f);
        if (!PlayerPrefs.HasKey("RightButtonX")) PlayerPrefs.SetFloat("RightButtonX", 0.2273906f);
        if (!PlayerPrefs.HasKey("RightButtonY")) PlayerPrefs.SetFloat("RightButtonY", 0.143f);
        PlayerPrefs.SetFloat("DashButtonX", 0.9f);
        PlayerPrefs.SetFloat("DashButtonY", 0.15f);
        PlayerPrefs.SetFloat("PauseButtonX", 0.77f);
        PlayerPrefs.SetFloat("PauseButtonY", 0.42f);
        PlayerPrefs.SetFloat("LeftButtonX", 0.6f);
        PlayerPrefs.SetFloat("LeftButtonY", 0.15f);
        PlayerPrefs.SetFloat("RightButtonX", 0.75f);
        PlayerPrefs.SetFloat("RightButtonY", 0.15f);
        if (!PlayerPrefs.HasKey("JoystickX")) PlayerPrefs.SetFloat("JoystickX", 0.1174375f);
        if (!PlayerPrefs.HasKey("JoystickY")) PlayerPrefs.SetFloat("JoystickY", 0.2088333f);
        if (!PlayerPrefs.HasKey("JumpButtonX")) PlayerPrefs.SetFloat("JumpButtonX", 0.7942656f);
        if (!PlayerPrefs.HasKey("JumpButtonY")) PlayerPrefs.SetFloat("JumpButtonY", 0.1291945f);
        if (!PlayerPrefs.HasKey("DashButtonX")) PlayerPrefs.SetFloat("DashButtonX", 0.9236094f);
        if (!PlayerPrefs.HasKey("DashButtonY")) PlayerPrefs.SetFloat("DashButtonY", 0.272f);
        //if (!PlayerPrefs.HasKey("PauseButtonX")) PlayerPrefs.SetFloat("PauseButtonX", 0.9236094f);
        //if (!PlayerPrefs.HasKey("PauseButtonY")) PlayerPrefs.SetFloat("PauseButtonY", 0.8933056f);  
        buttons[0].GetComponent<RectTransform>().anchorMin = new Vector2(PlayerPrefs.GetFloat("LeftButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("LeftButtonY") * 0.9144895f + 0.039f);
        buttons[0].GetComponent<RectTransform>().anchorMax = new Vector2(PlayerPrefs.GetFloat("LeftButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("LeftButtonY") * 0.9144895f + 0.039f);
        buttons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        buttons[1].GetComponent<RectTransform>().anchorMin = new Vector2(PlayerPrefs.GetFloat("RightButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("RightButtonY") * 0.9144895f + 0.039f);
        buttons[1].GetComponent<RectTransform>().anchorMax = new Vector2(PlayerPrefs.GetFloat("RightButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("RightButtonY") * 0.9144895f + 0.039f);
        buttons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        buttons[2].GetComponent<RectTransform>().anchorMin = new Vector2(PlayerPrefs.GetFloat("JoystickX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("JoystickY") * 0.9144895f + 0.039f);
        buttons[2].GetComponent<RectTransform>().anchorMax = new Vector2(PlayerPrefs.GetFloat("JoystickX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("JoystickY") * 0.9144895f + 0.039f);
        buttons[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        buttons[3].GetComponent<RectTransform>().anchorMin = new Vector2(PlayerPrefs.GetFloat("JumpButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("JumpButtonY") * 0.9144895f + 0.039f);
        buttons[3].GetComponent<RectTransform>().anchorMax = new Vector2(PlayerPrefs.GetFloat("JumpButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("JumpButtonY") * 0.9144895f + 0.039f);
        buttons[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        buttons[4].GetComponent<RectTransform>().anchorMin = new Vector2(PlayerPrefs.GetFloat("DashButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("DashButtonY") * 0.9144895f + 0.039f);
        buttons[4].GetComponent<RectTransform>().anchorMax = new Vector2(PlayerPrefs.GetFloat("DashButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("DashButtonY") * 0.9144895f + 0.039f);
        buttons[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);        
        buttons[5].GetComponent<RectTransform>().anchorMin = new Vector2(PlayerPrefs.GetFloat("PauseButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("PauseButtonY") * 0.9144895f + 0.039f);
        buttons[5].GetComponent<RectTransform>().anchorMax = new Vector2(PlayerPrefs.GetFloat("PauseButtonX") * 0.9314612f + 0.021f, PlayerPrefs.GetFloat("PauseButtonY") * 0.9144895f + 0.039f);
        buttons[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        buttonsAnchoredPos = new[] { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
        for (int i = 0; i < 6; i++)
        {
            buttonsAnchoredPos[i] = new Vector2(buttons[i].GetComponent<RectTransform>().anchorMin.x * 1856.0f, buttons[i].GetComponent<RectTransform>().anchorMin.y * 855.0f);
        }
        if (PlayerPrefs.GetInt("MovementMode") == 0) buttons[2].SetActive(false);
        else
        {
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
        }
        CalculateNeighbours(out neighbour, out neighbourNumb);

        for (int i = 0; i < 6; i++) Debug.Log(neighbourNumb[i]);
        //We put the real value on the sliders
        masterSlider.value = PlayerPrefs.GetFloat("masterAudio");
        musicSlider.value = PlayerPrefs.GetFloat("musicAudio");
        effectsSlider.value = PlayerPrefs.GetFloat("effectsAudio");
        //We check if the player has choosen the language
        if (PlayerPrefs.GetInt("languageChoosen") == 0)
        {
            languageSelectionMenu.SetActive(true);
            PlayerPrefs.SetInt("languageChoosen", 1);
        }
        else languageSelectionMenu.SetActive(false);
        //We check the settings
        if (PlayerPrefs.GetInt("fullScreen") == 0) fullScreen = false;
        else fullScreen = true;
        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreen, PlayerPrefs.GetInt("framerate"));        
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.value = 5;
        movementDropdown.value = PlayerPrefs.GetInt("MovementMode");
        //We write al the texts depending on the choosen language
        WriteTexts();
        //We put the actual value on the language dropdown
        languageDropdown.value = PlayerPrefs.GetInt("language");
        //We play the menu music a bit delayed to wait to the sound settings to apply
        musicSource.PlayDelayed(0.2f);
        //We show what character has the player selected
        characterSelected[PlayerPrefs.GetInt("selectedCharacter")].GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.1144984f, 1.0f);
    }
    //A function to start a new game
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if(changingButtonPos != 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), touch.position, GetComponent<Canvas>().worldCamera, out newButtonPos);
                if (changingButtonPos == 4)
                {
                    Vector2 tempPos;
                    bool found = false;
                    buttonPos.transform.position = GetComponent<Transform>().TransformPoint(newButtonPos);
                    tempPos = buttonPos.GetComponent<RectTransform>().anchoredPosition;
                    if (tempPos.x > 1676.0f || tempPos.x < 130.0f || tempPos.y < 124.0f || tempPos.y > 723.0f) found = true;
                    int i = 0;
                    while (i < 6 && !found)
                    {
                        if (i != changingButtonPos - 1 && !(i == 2 && PlayerPrefs.GetInt("MovementMode") == 0) && !((i == 0 || i == 1) && PlayerPrefs.GetInt("MovementMode") == 1))
                        {
                            if ((buttonsAnchoredPos[i] - tempPos).magnitude < 181.0f)
                                found = true;
                            else i++;
                        }
                        else i++;
                    }
                    if (!found)
                    {
                        Debug.Log("ola");
                        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = tempPos;
                    }
                }

            }
            

                /*
                Vector2 tempPos;
                int limitX;
                int limitY;
                buttonPos.transform.position = GetComponent<Transform>().TransformPoint(newButtonPos);
                if (buttonPos.GetComponent<RectTransform>().anchoredPosition.x > 1676.0f)
                {
                    newX = 1676.0f;
                    limitX = 2;
                }
                else if (buttonPos.GetComponent<RectTransform>().anchoredPosition.x < 130.0f)
                {
                    newX = 130.0f;
                    limitX = 1;
                }
                else
                {
                    newX = buttonPos.GetComponent<RectTransform>().anchoredPosition.x;
                    limitX = 0;
                }
                if (buttonPos.GetComponent<RectTransform>().anchoredPosition.y < 124.0f)
                {
                    newY = 124.0f;
                    limitY = 1;
                }
                else if (buttonPos.GetComponent<RectTransform>().anchoredPosition.y > 723.0f)
                {
                    newY = 723.0f;
                    limitY = 2;
                }
                else
                {
                    newY = buttonPos.GetComponent<RectTransform>().anchoredPosition.y;
                    limitY = 0;
                }
                tempPos = new Vector2(newX, newY);
                int i=0;
                bool found = false;
                while(i<6 && !found)
                {
                    if (i != changingButtonPos - 1 && !(i == 2  && PlayerPrefs.GetInt("MovementMode") == 0) && !((i == 0  || i == 1 ) && PlayerPrefs.GetInt("MovementMode") == 1))
                    {
                        if ((buttonsAnchoredPos[i] - tempPos).magnitude < 181.0f)
                            found = true;
                        else i++;
                    }
                    else i++;
                }
                if (found)
                {
                    float px = tempPos.x - buttonsAnchoredPos[i].x;
                    float py = tempPos.y - buttonsAnchoredPos[i].y;
                    float side;
                    if (limitX == 0 && limitY == 0)
                    {
                        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(181.0f * Mathf.Cos(Mathf.Atan2(py, px)) + buttonsAnchoredPos[i].x, 181.0f * Mathf.Sin(Mathf.Atan2(py, px)) + buttonsAnchoredPos[i].y);
                        tempPos = buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition;
                        if (buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.x > 1676.0f)
                        {
                            newX = 1676.0f;
                            limitX = 2;
                        }
                        else if (buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.x < 130.0f)
                        {
                            newX = 130.0f;
                            limitX = 1;
                        }
                        else newX = tempPos.x;
                        if (buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.y < 124.0f)
                        {
                            newY = 124.0f;
                            limitY = 1;
                        }
                        else if (buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.y > 723.0f)
                        {
                            newY = 723.0f;
                            limitY = 2;
                        }
                        else newY = tempPos.y;
                        tempPos = new Vector2(newX, newY);
                    }
                    if(limitX != 0)
                    {
                        if(limitX == 1) newX = 130.0f;
                        else newX = 1676.0f;
                        if (buttonsAnchoredPos[i].y + 181.0f > 723.0f) side = -1;
                        else if (buttonsAnchoredPos[i].y - 181.0f < 124.0f) side = 1;
                        else side = py / Mathf.Abs(py);
                        newY = (2* buttonsAnchoredPos[i].y + side * Mathf.Sqrt(Mathf.Pow(2* buttonsAnchoredPos[i].y,2)-4.0f*(-(Mathf.Pow(181.0f,2))+ Mathf.Pow(tempPos.x - buttonsAnchoredPos[i].x, 2)+ Mathf.Pow(buttonsAnchoredPos[i].y, 2)))) /2.0f;
                        tempPos = new Vector2(newX, newY);
                        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, newY);
                    }
                    else if(limitY != 0)
                    {
                        if (limitY == 1) newY = 124.0f;
                        else newY = 723.0f;
                        if (buttonsAnchoredPos[i].x + 181.0f > 1676.0f) side = -1;
                        else if (buttonsAnchoredPos[i].x - 181.0f < 130.0f) side = 1;
                        else side = px / Mathf.Abs(px);
                        newX = (2 * buttonsAnchoredPos[i].x + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].x, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(tempPos.y - buttonsAnchoredPos[i].y, 2) + Mathf.Pow(buttonsAnchoredPos[i].x, 2)))) / 2.0f;
                        tempPos = new Vector2(newX, newY);
                        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, newY);
                    }
                    if (neighbourNumb[i] > 1)
                    {
                        int k = -1;
                        for(int j = 0; j < 6; j++)
                        {
                            if(i!=j && neighbour[i][j] != 0)
                            {
                                if ((j != changingButtonPos - 1) && (tempPos - buttonsAnchoredPos[j]).magnitude < 181.0f)
                                {
                                    k = j;
                                }
                            }
                        }
                        if (k != -1)
                        {
                            int neighbourBlockCuant = 0;
                            int[] neighbourBlockNumb = new[] { -1,-1};
                            for (int j = 0; j < 6; j++)
                            {
                                if ((j!=changingButtonPos-1)&&!(j == 2 && PlayerPrefs.GetInt("MovementMode") == 0) && !((j == 0 || j == 1) && PlayerPrefs.GetInt("MovementMode") == 1) && j !=k && j!=i && neighbour[i][j] == neighbour[k][j] && neighbour[i][j]==1)
                                {
                                    neighbourBlockNumb[neighbourBlockCuant] = j;
                                    neighbourBlockCuant ++;
                                }
                            }
                            Vector2 left;
                            Vector2 right;
                            if (buttonsAnchoredPos[k].x > buttonsAnchoredPos[i].x)
                            {
                                left = buttonsAnchoredPos[i];
                                right = buttonsAnchoredPos[k];
                            }
                            else if (buttonsAnchoredPos[k].x < buttonsAnchoredPos[i].x)
                            {
                                right = buttonsAnchoredPos[i];
                                left = buttonsAnchoredPos[k];
                            }
                            else if(buttonsAnchoredPos[k].y > buttonsAnchoredPos[i].y)
                            {
                                right = buttonsAnchoredPos[k];
                                left = buttonsAnchoredPos[i];
                            }
                            else
                            {
                                left = buttonsAnchoredPos[k];
                                right = buttonsAnchoredPos[i];
                            }
                            Vector2 p;
                            //if (buttonsAnchoredPos[k].x > buttonsAnchoredPos[i].x) p = Vector2.Perpendicular((buttonsAnchoredPos[k] - buttonsAnchoredPos[i]).normalized);
                            //else p = Vector2.Perpendicular((buttonsAnchoredPos[i] - buttonsAnchoredPos[k]).normalized);
                            p = Vector2.Perpendicular((right - left).normalized);
                            float diff = (buttonsAnchoredPos[k] - buttonsAnchoredPos[i]).magnitude;
                            if (neighbourBlockCuant == 0)
                            {
                                newX = (buttonsAnchoredPos[k].x + buttonsAnchoredPos[i].x) / 2;
                                newY = (buttonsAnchoredPos[k].y + buttonsAnchoredPos[i].y) / 2;
                                Vector2 positive = new Vector2(newX, newY) + 1.0f * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                Vector2 negative = new Vector2(newX, newY) - 1.0f * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                //Debug.Log(negative.x);
                                //Debug.Log(negative.y);
                                //Debug.Log(positive.x);
                                //Debug.Log(positive.y);
                                if (positive.y > 723.0f || positive.x < 130.0f)
                                {
                                    //Debug.Log("a");
                                    side = -1;
                                }
                                else if (negative.y < 124.0f || negative.x > 1676.0f)
                                {
                                    //Debug.Log("b");
                                    side = 1;
                                }
                                //if (buttonsAnchoredPos[i].y + 181.0f > 723.0f || buttonsAnchoredPos[i].x + 181.0f > 1676.0f) side = -1;
                                //else if (buttonsAnchoredPos[i].y - 181.0f < 124.0f || buttonsAnchoredPos[i].x - 181.0f < 130.0f) side = 1;
                                else
                                {
                                    //Debug.Log("c");
                                    if (((right.x - left.x) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.y - left.y) - (right.y - left.y) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.x - left.x)) > 0)
                                        side = 1;
                                    else side = -1;
                                }
                                if(side ==1) buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = positive;
                                else buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = negative;
                            }
                            else if (neighbourBlockCuant == 1)
                            {
                                //Debug.Log("ola");
                                if (((right.x - left.x) * (buttonsAnchoredPos[neighbourBlockNumb[0]].y - left.y) - (right.y - left.y) * (buttonsAnchoredPos[neighbourBlockNumb[0]].x - left.x)) > 0)
                                    side = -1;
                                else side = 1;
                                newX = (buttonsAnchoredPos[k].x + buttonsAnchoredPos[i].x) / 2;
                                newY = (buttonsAnchoredPos[k].y + buttonsAnchoredPos[i].y) / 2;
                                tempPos = new Vector2(newX, newY) + side * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                if (tempPos.x > 1676.0f)
                                {
                                    newX = 1676.0f;
                                    limitX = 2;
                                }
                                else if (tempPos.x < 130.0f)
                                {
                                    newX = 130.0f;
                                    limitX = 1;
                                }
                                else
                                {
                                    newX = tempPos.x;
                                    limitX = 0;
                                }
                                if (tempPos.y < 124.0f)
                                {
                                    newY = 124.0f;
                                    limitY = 1;
                                }
                                else if (tempPos.y > 723.0f)
                                {
                                    newY = 723.0f;
                                    limitY = 2;
                                }
                                else
                                {
                                    newY = tempPos.y;
                                    limitY = 0;
                                }
                                if(limitX==0 && limitY==0) buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = tempPos;
                                else
                                {
                                    if (limitX != 0)
                                    {
                                        if (limitX == 1) newX = 130.0f;
                                        else newX = 1676.0f;
                                        if (buttonsAnchoredPos[i].y + 181.0f > 723.0f) side = -1;
                                        else if (buttonsAnchoredPos[i].y - 181.0f < 124.0f) side = 1;
                                        else side = py / Mathf.Abs(py);
                                        newY = (2 * buttonsAnchoredPos[i].y + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].y, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(newX - buttonsAnchoredPos[i].x, 2) + Mathf.Pow(buttonsAnchoredPos[i].y, 2)))) / 2.0f;
                                        tempPos = new Vector2(newX, newY);
                                        if ((tempPos - buttonsAnchoredPos[k]).magnitude < 181.0f || (tempPos - buttonsAnchoredPos[i]).magnitude < 181.0f) side = -side;
                                        newY = (2 * buttonsAnchoredPos[i].y + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].y, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(newX - buttonsAnchoredPos[i].x, 2) + Mathf.Pow(buttonsAnchoredPos[i].y, 2)))) / 2.0f;
                                        tempPos = new Vector2(newX, newY);
                                        k = -1;
                                        for (int j = 0; j < 6; j++)
                                        {
                                            if (i != j && neighbour[i][j] != 0)
                                            {
                                                if ((j != changingButtonPos - 1) && (tempPos - buttonsAnchoredPos[j]).magnitude < 181.0f)
                                                {
                                                    k = j;
                                                }
                                            }
                                        }
                                        if (k != -1)
                                        {
                                            if (buttonsAnchoredPos[k].x > buttonsAnchoredPos[i].x)
                                            {
                                                left = buttonsAnchoredPos[i];
                                                right = buttonsAnchoredPos[k];
                                            }
                                            else if (buttonsAnchoredPos[k].x < buttonsAnchoredPos[i].x)
                                            {
                                                right = buttonsAnchoredPos[i];
                                                left = buttonsAnchoredPos[k];
                                            }
                                            else if (buttonsAnchoredPos[k].y > buttonsAnchoredPos[i].y)
                                            {
                                                right = buttonsAnchoredPos[k];
                                                left = buttonsAnchoredPos[i];
                                            }
                                            else
                                            {
                                                left = buttonsAnchoredPos[k];
                                                right = buttonsAnchoredPos[i];
                                            }
                                            p = Vector2.Perpendicular((right - left).normalized);
                                            diff = (buttonsAnchoredPos[k] - buttonsAnchoredPos[i]).magnitude;
                                            newX = (buttonsAnchoredPos[k].x + buttonsAnchoredPos[i].x) / 2;
                                            newY = (buttonsAnchoredPos[k].y + buttonsAnchoredPos[i].y) / 2;
                                            Vector2 positive = new Vector2(newX, newY) + 1.0f * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                            Vector2 negative = new Vector2(newX, newY) - 1.0f * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                            if (positive.y > 723.0f || positive.x > 1676.0f)
                                            {
                                                side = -1;
                                            }
                                            else if (negative.y < 124.0f || negative.x < 130.0f)
                                            {
                                                side = 1;
                                            }
                                            else
                                            {
                                                if (((right.x - left.x) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.y - left.y) - (right.y - left.y) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.x - left.x)) > 0)
                                                    side = 1;
                                                else side = -1;
                                            }
                                            if (side == 1) buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = positive;
                                            else buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = negative;

                                        }
                                        else buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = tempPos;
                                    }
                                    else if (limitY != 0)
                                    {
                                        if (limitY == 1) newY = 124.0f;
                                        else newY = 723.0f;
                                        if (buttonsAnchoredPos[i].x + 181.0f > 1676.0f) side = -1;
                                        else if (buttonsAnchoredPos[i].x - 181.0f < 130.0f) side = 1;
                                        else side = px / Mathf.Abs(px);
                                        newX = (2 * buttonsAnchoredPos[i].x + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].x, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(newY - buttonsAnchoredPos[i].y, 2) + Mathf.Pow(buttonsAnchoredPos[i].x, 2)))) / 2.0f;
                                        tempPos = new Vector2(newX, newY);
                                        if ((tempPos - buttonsAnchoredPos[k]).magnitude < 181.0f || (tempPos - buttonsAnchoredPos[i]).magnitude < 181.0f) side = -side; 
                                        newX = (2 * buttonsAnchoredPos[i].x + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].x, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(newY - buttonsAnchoredPos[i].y, 2) + Mathf.Pow(buttonsAnchoredPos[i].x, 2)))) / 2.0f;
                                        tempPos = new Vector2(newX, newY);
                                        k = -1;
                                        for (int j = 0; j < 6; j++)
                                        {
                                            if (i != j && neighbour[i][j] != 0)
                                            {
                                                if ((j != changingButtonPos - 1) && (tempPos - buttonsAnchoredPos[j]).magnitude < 181.0f)
                                                {
                                                    k = j;
                                                }
                                            }
                                        }
                                        if (k != -1)
                                        {
                                            Debug.Log("ola");
                                            if (buttonsAnchoredPos[k].x > buttonsAnchoredPos[i].x)
                                            {
                                                left = buttonsAnchoredPos[i];
                                                right = buttonsAnchoredPos[k];
                                            }
                                            else if (buttonsAnchoredPos[k].x < buttonsAnchoredPos[i].x)
                                            {
                                                right = buttonsAnchoredPos[i];
                                                left = buttonsAnchoredPos[k];
                                            }
                                            else if (buttonsAnchoredPos[k].y > buttonsAnchoredPos[i].y)
                                            {
                                                right = buttonsAnchoredPos[k];
                                                left = buttonsAnchoredPos[i];
                                            }
                                            else
                                            {
                                                left = buttonsAnchoredPos[k];
                                                right = buttonsAnchoredPos[i];
                                            }
                                            p = Vector2.Perpendicular((right - left).normalized);
                                            diff = (buttonsAnchoredPos[k] - buttonsAnchoredPos[i]).magnitude;
                                            newX = (buttonsAnchoredPos[k].x + buttonsAnchoredPos[i].x) / 2;
                                            newY = (buttonsAnchoredPos[k].y + buttonsAnchoredPos[i].y) / 2;
                                            Vector2 positive = new Vector2(newX, newY) + 1.0f * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                            Vector2 negative = new Vector2(newX, newY) - 1.0f * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                                            if (positive.y > 723.0f || positive.x > 1676.0f)
                                            {
                                                side = -1;
                                            }
                                            else if (negative.y < 124.0f || negative.x < 130.0f)
                                            {
                                                side = 1;
                                            }
                                            else
                                            {
                                                if (((right.x - left.x) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.y - left.y) - (right.y - left.y) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.x - left.x)) > 0)
                                                    side = 1;
                                                else side = -1;
                                            }
                                            if (side == 1) buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = positive;
                                            else buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = negative;

                                        }
                                        else
                                        {
                                            if (tempPos.x > 1676.0f)
                                            {
                                                newX = 1676.0f;
                                                limitX = 2;
                                            }
                                            else if (tempPos.x < 130.0f)
                                            {
                                                newX = 130.0f;
                                                limitX = 1;
                                            }
                                            else newX = tempPos.x;
                                            if (tempPos.y < 124.0f)
                                            {
                                                newY = 124.0f;
                                                limitY = 1;
                                            }
                                            else if (tempPos.y > 723.0f)
                                            {
                                                newY = 723.0f;
                                                limitY = 2;
                                            }
                                            else newY = tempPos.y;
                                            tempPos = new Vector2(newX, newY);                                        
                                            if (limitX != 0)
                                            {
                                                if (limitX == 1) newX = 130.0f;
                                                else newX = 1676.0f;
                                                if (buttonsAnchoredPos[i].y + 181.0f > 723.0f) side = -1;
                                                else if (buttonsAnchoredPos[i].y - 181.0f < 124.0f) side = 1;
                                                else side = py / Mathf.Abs(py);
                                                newY = (2 * buttonsAnchoredPos[i].y + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].y, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(tempPos.x - buttonsAnchoredPos[i].x, 2) + Mathf.Pow(buttonsAnchoredPos[i].y, 2)))) / 2.0f;
                                                tempPos = new Vector2(newX, newY);
                                                buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, newY);
                                            }
                                            else if (limitY != 0)
                                            {
                                                if (limitY == 1) newY = 124.0f;
                                                else newY = 723.0f;
                                                if (buttonsAnchoredPos[i].x + 181.0f > 1676.0f) side = -1;
                                                else if (buttonsAnchoredPos[i].x - 181.0f < 130.0f) side = 1;
                                                else side = px / Mathf.Abs(px);
                                                newX = (2 * buttonsAnchoredPos[i].x + side * Mathf.Sqrt(Mathf.Pow(2 * buttonsAnchoredPos[i].x, 2) - 4.0f * (-(Mathf.Pow(181.0f, 2)) + Mathf.Pow(tempPos.y - buttonsAnchoredPos[i].y, 2) + Mathf.Pow(buttonsAnchoredPos[i].x, 2)))) / 2.0f;
                                                tempPos = new Vector2(newX, newY);
                                                buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, newY);
                                            }
                                            buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = tempPos;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log(buttonsAnchoredPos[neighbourBlockNumb[0]]);
                                Debug.Log((buttonPos.GetComponent<RectTransform>().anchoredPosition - buttonsAnchoredPos[neighbourBlockNumb[0]]).magnitude);
                                Debug.Log(buttonsAnchoredPos[neighbourBlockNumb[1]]);
                                Debug.Log((buttonPos.GetComponent<RectTransform>().anchoredPosition - buttonsAnchoredPos[neighbourBlockNumb[1]]).magnitude);
                                Debug.Log(buttonPos.GetComponent<RectTransform>().anchoredPosition);
                                int newButton;
                                if ((buttonPos.GetComponent<RectTransform>().anchoredPosition - buttonsAnchoredPos[neighbourBlockNumb[0]]).magnitude < (buttonPos.GetComponent<RectTransform>().anchoredPosition - buttonsAnchoredPos[neighbourBlockNumb[1]]).magnitude)
                                {
                                    newButton = 0;
                                }
                                else newButton = 1;
                                if (buttonsAnchoredPos[neighbourBlockNumb[newButton]].x > buttonsAnchoredPos[i].x)
                                {
                                    left = buttonsAnchoredPos[i];
                                    right = buttonsAnchoredPos[neighbourBlockNumb[newButton]];
                                }
                                else
                                {
                                    right = buttonsAnchoredPos[i];
                                    left = buttonsAnchoredPos[neighbourBlockNumb[newButton]];
                                } 
                                p = Vector2.Perpendicular((right - left).normalized);
                                diff = (left - right).magnitude; 
                                newX = (left.x + right.x) / 2;
                                newY = (left.y + right.y) / 2; 
                                if (((right.x - left.x) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.y - left.y) - (right.y - left.y) * (buttonPos.GetComponent<RectTransform>().anchoredPosition.x - left.x)) > 0)
                                    side = -1;
                                else side = 1;
                                buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, newY) + side * Mathf.Sqrt(Mathf.Pow(181.0f, 2) - Mathf.Pow(diff / 2, 2)) * p;
                            }
                        }                        
                    }
                    
                    //Debug.Log(new Vector2(px,py));
                    //if(Mathf.Abs(buttonsAnchoredPos[i].x - tempPos.x)>= Mathf.Abs(buttonsAnchoredPos[i].y - tempPos.y))
                    //{
                    //    buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(buttonsAnchoredPos[i].x + (181.0f- Mathf.Abs(buttonsAnchoredPos[i].y - tempPos.y)), tempPos.y);
                    //}
                    //else buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempPos.x, buttonsAnchoredPos[i].y + (181.0f - Mathf.Abs(buttonsAnchoredPos[i].x - tempPos.x)));
                }
                else buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = tempPos;
                //buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = tempPos;
                //Debug.Log(buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition);
                //Debug.Log(buttonsAnchoredPos[4]);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    newButtonPos = new Vector2(buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.x / buttons[changingButtonPos - 1].transform.parent.GetComponent<RectTransform>().rect.width, buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.y / buttons[changingButtonPos - 1].transform.parent.GetComponent<RectTransform>().rect.height);
                    buttons[changingButtonPos-1].GetComponent<RectTransform>().anchorMin = newButtonPos;
                    buttons[changingButtonPos-1].GetComponent<RectTransform>().anchorMax = newButtonPos;
                    buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 0.0f);
                    PlayerPrefs.SetFloat("JumpButtonX", (newButtonPos.x - 0.021f) / 0.9314612f);
                    PlayerPrefs.SetFloat("JumpButtonY", (newButtonPos.y - 0.039f) / 0.9144895f);
                    buttonsAnchoredPos[changingButtonPos - 1] = new Vector2(buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMin.x * 1856.0f, buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMin.y * 855.0f);
                    changingButtonPos = 0;
                }*/
                       
        }
    }
    void FixedUpdate()
    {
        //We check how much time has passed from the moment the player saved the resolution, if the player needs more than 10 seconds to confirm we return to the previous resolution
        if (resolutionTime != 0.0f && (Time.fixedTime - resolutionTime) >= 10)
        {
            resolutionTime = 0.0f;
        }
        else if (resolutionTime != 0.0f)
        {
            if (PlayerPrefs.GetInt("language") == 0) returnText.text = "Return (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
            else if (PlayerPrefs.GetInt("language") == 1) returnText.text = "Volver (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
            else if (PlayerPrefs.GetInt("language") == 2) returnText.text = "Itzuli (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
        }
    }


    public void CalculateNeighbours(out int[][] n , out int[] nN )
    {
        int j = 0;
        n = new[] { new[] { 0, 0, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 0, 0 } };
        nN = new[] { 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 6; i++)
        {
            while (j < 6)
            {
                if (((buttonsAnchoredPos[i] - buttonsAnchoredPos[j]).magnitude < 362.0f) && !((i==2 || j==2) && PlayerPrefs.GetInt("MovementMode") == 0) && !(((i==0 || j==0)||(i==1 || j==1)) && PlayerPrefs.GetInt("MovementMode") == 1))
                {
                    n[i][j] = 1;
                    nN[i]++;
                    if (i != j)
                    {
                        n[j][i] = 1;
                        nN[j]++;
                    }
                }
                j++;
            }
            j = i + 1;
        }
    }

    //Function to close the game
    public void CloseGame()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }

    //Function to open and close the select character menu
    public void OpenSelectCharacterMenu()
    {
        mainMenu.SetActive(false);
        selectCharacterMenu.SetActive(true);
    }

    public void CloseSelectCharacterMenu()
    {
        mainMenu.SetActive(true);
        selectCharacterMenu.SetActive(false);
    }
    //Function to set the selected character
    public void SetSelectedCharacter(int c)
    {
        characterSelected[PlayerPrefs.GetInt("selectedCharacter")].GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.1144984f, 0.0f);
        PlayerPrefs.SetInt("selectedCharacter", c);
        characterSelected[PlayerPrefs.GetInt("selectedCharacter")].GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.1144984f, 1.0f);
    }

    //Function to save the settings
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        prevMaster = PlayerPrefs.GetFloat("masterAudio");
        prevMusic = PlayerPrefs.GetFloat("musicAudio");
        prevEffect = PlayerPrefs.GetFloat("effectsAudio");
        prevLanguage = PlayerPrefs.GetInt("language");
    }

    //Function to close the settings without saving. We return the values to their actual value.
    public void CloseNoSave()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.value = 5;
        languageDropdown.value = prevLanguage;
        masterSlider.value = prevMaster;
        musicSlider.value = prevMusic;
        effectsSlider.value = prevEffect;
    }

    //Function to save the changes made on the settings
    public void CloseSave()
    {
        prevW = PlayerPrefs.GetInt("resolutionW");
        prevH = PlayerPrefs.GetInt("resolutionH");
        prevFS = fullScreen;
        prevFR = PlayerPrefs.GetInt("framerate");
        if (framerate.value == 0) PlayerPrefs.SetInt("framerate", 30);
        else if (framerate.value == 1) PlayerPrefs.SetInt("framerate", 60);
        else if (framerate.value == 2) PlayerPrefs.SetInt("framerate", 90);
        else if (framerate.value == 3) PlayerPrefs.SetInt("framerate", 120);
        else if (framerate.value == 4) PlayerPrefs.SetInt("framerate", 144);
        else if (framerate.value == 5) PlayerPrefs.SetInt("framerate", 0);

        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), true, PlayerPrefs.GetInt("framerate"));

        
        //If we change the resolution or the full screen mode we ask the player if them can see the window correctly
        if (prevW == PlayerPrefs.GetInt("resolutionW") && prevH == PlayerPrefs.GetInt("resolutionH") && prevFS == fullScreen)
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            resolutionTime = Time.fixedTime;
            confirmationMenu.SetActive(true);
        }
        PlayerPrefs.SetFloat("masterAudio", masterSlider.value);
        PlayerPrefs.SetFloat("musicAudio", musicSlider.value);
        PlayerPrefs.SetFloat("effectsAudio", effectsSlider.value);
    }

    //Function to confirm that the player can see the resolution changes correctly
    public void ConfirmResolution()
    {
        confirmationMenu.SetActive(false);
        settingsMenu.SetActive(false);
        resolutionTime = 0.0f;
        mainMenu.SetActive(true);
    }
        

    //Function to change the language
    public void ChangeLanguage(bool first)
    {
        if (first) PlayerPrefs.SetInt("language", languageChooseDropdown.value);
        else PlayerPrefs.SetInt("language", languageDropdown.value);
        WriteTexts();
    }

    //Function to write the texts
    public void WriteTexts()
    {
        if (PlayerPrefs.GetInt("language") == 0)
        {
            playText.text = "Play";
            settingsText.text = "Settings";
            creditsText.text = "Credits";
            exitText.text = "Exit";
            howToPlayText.text = "How to play";
            settingsTitle.text = "Settings";
            frameRateText.text = "Framerate";
            mainVolumeText.text = "Main volume";
            musicText.text = "Music";
            effectsText.text = "Effects";
            languageText.text = "Language";
            changeControlsText.text = "Change controls";
            returnSaveText.text = "Save and return";
            returnNoSaveText.text = "Return without saving";
            confirmationText.text = "Confirm if you can see this window correctly. You will return to the previous configuration if you do not confirm.";
            saveConfirmText.text = "Save changes";
            languageChooseText.text = "Select the language you want the game to be. \nYou can change it whenever you want on the settings.";
            saveLanguageText.text = "Save language";
            creditsTitleText.text = "Credits";
            creditsReturnText.text = "Return";
            howToPlayTitleText.text = "How to play";
            howToPlayPage1.text = "In Arima?s Shadow you will have to catch as many coins as you can, but every time you grab one of them a shadow of yourself from one second before will start following you.";
            howToPlayPage2.text = "If a shadow touches Arima the game will end and you will need to start again from the beginning.";
            howToPlayPage3.text = "To avoid the shadows you can use the jump and the dash in addition to the normal movement.";
            howToPlayPage4.text = "If you exit the screen from any side, you will appear on the other one, really useful to find new ways to avoid the shadows.";
            howToPlayPage5.text = "You can stop the game in any moment, then continuing, restarting or returning to the main menu.";
            howToPlayPage6.text = "You can change all the buttons of the controls at the settings menu, use the button combination that is better to you!";
            howToPlayReturnText.text = "Return";
            controlsTitleText.text = "Controls";
            returnToSettingsText.text = "Return to settings";

        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            playText.text = "Jugar";
            settingsText.text = "Ajustes";
            creditsText.text = "Cr?ditos";
            exitText.text = "Salir";
            howToPlayText.text = "C?mo jugar";
            settingsTitle.text = "Ajustes";
            frameRateText.text = "Im?genes por segundo";
            mainVolumeText.text = "Volumen maestro";
            musicText.text = "M?sica";
            effectsText.text = "Efectos";
            languageText.text = "Idioma";
            changeControlsText.text = "Cambiar los controles";
            returnSaveText.text = "Guardar y volver";
            returnNoSaveText.text = "Volver sin guardar";
            confirmationText.text = "Confirma que puedes ver esta ventana correctamente. Volver?s a la configuraci?n previa si no confirmas.";
            saveConfirmText.text = "Guardar los cambios";
            languageChooseText.text = "Selecciona el idioma en el que quieres que est? el juego. \nPuedes cambiarlo cuando quieras en los ajustes.";
            saveLanguageText.text = "Guardar idioma";
            creditsTitleText.text = "Cr?ditos";
            creditsReturnText.text = "Volver";
            howToPlayTitleText.text = "C?mo jugar";
            howToPlayPage1.text = "En Arima?s Shadow tendr?s que intentar recoger el m?ximo n?mero de monedas posible, pero cada vez que cojas una te empezara a perseguir una sombra tuya de hace un segundo.";
            howToPlayPage2.text = "Si una sombra toca a Arima el juego terminar? y tendr?s que volver a empezar desde el principio.";
            howToPlayPage3.text = "Para evitar las sombras podr?s usar los saltos y las embestidas adem?s del movimiento normal.";
            howToPlayPage4.text = "Si sales de la pantalla por cualquiera de los lados, aparecer?s en el otro, muy ?til para encontrar nuevos caminos para evitar las sombras.";
            howToPlayPage5.text = "Puedes parar el juego en cualquier momento, luego continu?ndolo, reiniciando o regresando al men? principal.";
            howToPlayPage6.text = "Puedes cambiar los botones de todos los controles desde el men? de ajustes, ?usa la combinaci?n de botones que m?s te guste!";
            howToPlayReturnText.text = "Volver";
            controlsTitleText.text = "Controles";
            returnToSettingsText.text = "Volver a los ajustes";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            playText.text = "Jolastu";
            settingsText.text = "Ezarpenak";
            creditsText.text = "Kredituak";
            exitText.text = "Irten";
            howToPlayText.text = "Nola jolastu";
            settingsTitle.text = "Ezarpenak";
            frameRateText.text = "Irudiak segunduko";
            mainVolumeText.text = "Bolumen nagusia";
            musicText.text = "Musika";
            effectsText.text = "Efektuak";
            languageText.text = "Hizkuntzak";
            changeControlsText.text = "Kontrolak aldatu";
            returnSaveText.text = "Gorde eta itzuli";
            returnNoSaveText.text = "Itzuli gorde gabe";
            confirmationText.text = "Lehio hau ondo ikus dezakezula ziurtatu. Ez baduzu baieztatzen lehengo konfiguraziora itzuliko zara.";
            saveConfirmText.text = "Aldaketak gorde";
            languageChooseText.text = "Erabaki zein hizkuntzatan izan nahi duzun jolasa. \nNahi duzunean alda dezakezu ezarpenetan.";
            saveLanguageText.text = "Hizkuntza gorde";
            creditsTitleText.text = "Kredituak";
            creditsReturnText.text = "Itzuli";
            howToPlayTitleText.text = "Nola jolastu";
            howToPlayPage1.text = "Arima's Shadow-en ahal duzun txanpon gehien biltzen saiatu behar zara, baina hauetako bat hartzen duzun bakoitzean duela segundo bateko zure itzal batek jarraituko zaitu. ";
            howToPlayPage2.text = "Itzal batek Arima ukitzen badu jokoa amaitu egingo da eta hasieratik hasi beharko zara berriro.";
            howToPlayPage3.text = "Itzalak saihesteko saltoak eta desplazamendu bizkorrak erabil ditzakezu mugimendu normalaz aparte.";
            howToPlayPage4.text = "Pantailatik irteten bazara edozein aldetik, beste aldean agertuko zara, oso erabilgarria itzalak saihesteko bide berriak aurkitzeko.";
            howToPlayPage5.text = "Jokoa edozein momentuan geldi dezakezu, gero jokoa jarraituz, berrabiaraziz edo menu nagusira itzuliz.";
            howToPlayPage6.text = "Kontrol guztien botoiak ezarpenen menutik alda ditzakezu, zuri gehien gustatzen zaizun botoi konbinazioa erabil ezazu!";
            howToPlayReturnText.text = "Itzuli";
            controlsTitleText.text = "Kontrolak";
            returnToSettingsText.text = "Ezarpenetara itzuli";
        }
    }

    //Function to open the controls menu
    public void OpenControls()
    {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    //Function to close the controls menu
    public void CloseControls()
    {
        settingsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    //Functions to open and close the change buttons position menu
    public void OpenChangeButtonPos()
    {
        changeButtonPosMenu.SetActive(true);
    }
    public void CloseChangeButtonPos()
    {
        changeButtonPosMenu.SetActive(false);
    }

    //Function to start changing the button
    public void StartChangeButtonPos(int i)
    {
        changingButtonPos = i;
        Vector2 tempPos = buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMin;
        buttons[changingButtonPos-1].GetComponent<RectTransform>().anchorMin = new Vector2(0.0f,0.0f);
        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 0.0f);
        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempPos.x * 1856.0f, tempPos.y * 855.0f);
    }

    public void EndChangeButtonPos()
    {
        newButtonPos = new Vector2(buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.x / buttons[changingButtonPos - 1].transform.parent.GetComponent<RectTransform>().rect.width, buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition.y / buttons[changingButtonPos - 1].transform.parent.GetComponent<RectTransform>().rect.height);
        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMin = newButtonPos;
        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMax = newButtonPos;
        buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 0.0f);
        PlayerPrefs.SetFloat("JumpButtonX", (newButtonPos.x - 0.021f) / 0.9314612f);
        PlayerPrefs.SetFloat("JumpButtonY", (newButtonPos.y - 0.039f) / 0.9144895f);
        buttonsAnchoredPos[changingButtonPos - 1] = new Vector2(buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMin.x * 1856.0f, buttons[changingButtonPos - 1].GetComponent<RectTransform>().anchorMin.y * 855.0f);
        changingButtonPos = 0;
    }

    //Function to open the credits menu
    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    //Function to close the credits menu
    public void CloseCredits()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    //Function to open the how to play menu
    public void OpenHowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    //Function to close the how to play menu
    public void CloseHowToPlay()
    {
        mainMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
        howToPlayPrevButton.interactable = false;
        howToPlayNextButton.interactable = true;
        howToPlayExplanation[howToPlayNumb].SetActive(false);
        howToPlayNumb = 0;
        howToPlayExplanation[howToPlayNumb].SetActive(true);
    }

    //Functions to change the how to play text
    public void NextHowToPlay()
    {
        if(howToPlayNumb == 0) howToPlayPrevButton.interactable = true; 
        else if(howToPlayNumb == 4) howToPlayNextButton.interactable = false;
        howToPlayExplanation[howToPlayNumb].SetActive(false);
        howToPlayNumb++;
        howToPlayExplanation[howToPlayNumb].SetActive(true);
    }

    public void PrevHowToPlay()
    {
        if (howToPlayNumb == 1) howToPlayPrevButton.interactable = false;
        else if (howToPlayNumb == 5) howToPlayNextButton.interactable = true;
        howToPlayExplanation[howToPlayNumb].SetActive(false);
        howToPlayNumb--;
        howToPlayExplanation[howToPlayNumb].SetActive(true);
    }

    //Function to close the choose language window
    public void CloseChooseLanguage()
    {
        languageSelectionMenu.SetActive(false);
        if (PlayerPrefs.GetInt("language") == 0) languageDropdown.value = 0;
        else if (PlayerPrefs.GetInt("language") == 1) languageDropdown.value = 1;
        else if (PlayerPrefs.GetInt("language") == 2) languageDropdown.value = 2;
    }

    public void ChangeMovement()
    {
        PlayerPrefs.SetInt("MovementMode", movementDropdown.value);
        if (PlayerPrefs.GetInt("MovementMode") == 0)
        {
            buttons[2].SetActive(false);
            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
        }
        else
        {
            buttons[2].SetActive(true);
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
        }
    }
}
