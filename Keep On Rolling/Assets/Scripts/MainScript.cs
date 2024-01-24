using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainScript : MonoBehaviour {

    [Header("Game Info")]
    public string mode = "menu";
    public string version;

    [Header("Player Info")]
    public int bestScore; //Best ever game score
    public int score; //Current game score
    public float currency;
    public int level = 1;
    public int maxLevel = 1;
    public Vector3 startPosition = new(0, 5.6f, -25); //Start location of player
    public float maxMovementSpeed;
    public float movementSpeed;
    public float slowDownMovementSpeed;
    public float rollSpeed;
    public float maxRollSpeed;
    public float platformSpeed;
    public float jumpSpeed;
    //public float touchMovementSpeed = 50;
    //public float touchSlowDownMovementSpeed = 1;

    [Header("Active Game Currency Info")]
    public float leveledPlatformCurrencyAmount;
    public float infiniteDistantCurrencyAmount;
    public float coinCurrencyAmount;

    [Header("Coin Info")]
    public GameObject coinPrefab;
    public int coinSpawnRate;

    [Header("Player Material Info")]
    public int currentPlayerMaterial;
    public string[] playerMaterialNames;
    public Material[] playerMaterials;
    public int[] playerMaterialCost;
    public bool[] playerMaterialUnlocked;

    [Header("Light Info")]
    public Color defaultLightColor;
    public Color newLightColor;
    public float lightColourChangeTime;

    [Header("Platform Info")]
    public int numberOfPlatforms;
    public GameObject[] platforms;
    public GameObject[] endPlatform;
    public int infinitePlatformStartAmount;

    [Header("Background Info")]
    public GameObject[] backgrounds;

    [Header("Ground Detail Info")]
    public GameObject[] groundDetails;
    public int groundDetailSpawnRate;

    [Header("Keys")]
    public KeyCode newGameSubmitNameKey;
    public KeyCode newGameSubmitNameAltKey;
    public KeyCode backButtonKey;
    public KeyCode openPlayMenu;
    public KeyCode startInfiniteKey;
    public KeyCode startLeveledKey;
    public KeyCode pauseKey;
    public KeyCode returnToGameKey;
    public KeyCode pickNextLevelKey;
    public KeyCode pickPreviousLevelKey;
    public KeyCode pickLevelKey;
    public KeyCode tryAgainKey;
    public KeyCode nextLevelKey;
    public KeyCode nextColourKey;
    public KeyCode beforeColourKey;
    public KeyCode pickColourKey;
    public KeyCode buyColourKey;
    public KeyCode playWithMaterialKey;
    public KeyCode settingsSubmitNewNameKey;
    public KeyCode settingsSubmitNewNameAltKey;

    [Header("Music Info")]
    public bool playNextSong = false;
    [Range(0, 1)]
    public float volume;
    public AudioSource musicSource;
    public AudioClip[] music;

    [Header("Settings")]
    public bool particlesEnabled;
    public bool musicEnabled;
    public bool fullScreenEnabled;
    //public bool touchScreenModeEnabled;
    public string playerName;
    public Color settingsEnabledColor;
    public Color settingsDisabledColor;

    [Header("Tags")]
    public string platformTag;
    public string backgroundTag;
    public string wallRideTag;
    public string obstacleTag;
    public string coinTag;
    public string outOfAreaTag;
    public string groundTag;
    public string endZoneTag;

    [Header("Camera Refs")]
    public GameObject mainMenuRoomCamera;
    public GameObject playerColourRoomCamera;
    public GameObject menuCamera;
    public GameObject gameCamera;
    public GameObject easterEggCamera;

    /*
    [Header("Rooms")]
    public GameObject easterEggRoomRef;
    */

    [Header("UI Refs")]
    //New game menu
    public GameObject newGameMenuRef;
    public InputField newGameNameInputField;
    public Button newGameStartButton;
    //Start menu
    public GameObject mainMenuRef;
    public Text mainMenuCurrecyText;
    public Text mainMenuBestScoreText;
    public Text mainMenuBestLevelText;
    //Play menu
    public GameObject playMenuRef;
    public Text playMenuCurrecyText;
    public Text playMenuBestScoreText;
    public Text playMenuBestLevelText;
    //Settings menu & sub-menus
    public GameObject settingMenuRef;
    public GameObject changeNameMenuRef;
    public GameObject resetGameMenuRef;
    public Button settingsParticlesButton;
    public Button settingsMusicButton;
    public Button settingsFullScreenButton;
    public Button settingsTouchScreenModeButton;
    public Text settingsCurrentNameText;
    public InputField settingsNameChangeInputField;
    public Text settingsVersionText;
    //Player colour menu
    public GameObject playerColourMenuRef;
    public Text colourMenuCurrecyText;
    public Text playerColourMenuSelectedColourText;
    public Button playerColourMenuPickColourButton;
    public Button playerColourMenuBuyColour;
    public Text playerColourMenuBuyColourPriceText;
    public GameObject playerColourMenuCurrentText;
    //Leaderboard menu
    public GameObject leaderboardMenuRef;
    //Pause menu
    public GameObject pauseMenuRef;
    //Level picker menu
    public GameObject levelPickerMenuRef;
    public GameObject levelPickerIncreaseLevelButton;
    public GameObject levelPickerDecreaseLevelButton;
    public Text levelPickerLevelText;
    //Infinite game
    public GameObject infiniteGameMenuRef;
    public Text infiniteGameScoreText;
    public Text infiniteGameBestScoreText;
    /*public Button infiniteGameLeftMovementButton;
      public Button infiniteGameRightMovementButton;*/
    //Leveled game
    public GameObject leveledGameMenuRef;
    public Text leveledGameLevelText;
    public Text leveledGamePercentageText;
    public Button leveledGameLeftMovementButton;
    public Button leveledGameRightMovementButton;
    //Infinite game over menu
    public GameObject infiniteGameOverMenuRef;
    public Text infiniteGameOverScoreText;
    public Text infiniteGameOverBestScoreText;
    public Text infiniteGameOverCoinsCollectedText;
    public Text infiniteGameOverMoneyEarntText;
    //Leveled game over menu
    public GameObject leveledGameOverMenuRef;
    public Text leveledGameOverLevelText;
    public Text leveledGameOverPercentageText;
    public Text leveledGameOverCoinsCollectedText;
    public Text leveledGameOverMoneyEarntText;
    //Leveled game win menu
    public GameObject leveledWinMenuRef;
    public Text leveledWinLevelText;
    public Text leveledWinPercentageText;
    public Text leveledWinCoinsCollectedText;
    public Text leveledWinMoneyEarntText;
    public Button leveledWinNextGameButton;
    //Easter egg menu (removed)
    //public GameObject easterEggMenuRef;

    [Header("Refs")]
    public GameObject playerModel;
    public GameObject colourDisplayBall;
    public Light mainLight;
    public GameObject audioListenerRef;
    public PlayerScript playerScriptRef;
    public GameDataController gameDataControllerRef;

    // Private variables
    bool inGame = false;
    int pickingPlayerMaterial = 7;
    bool changingName = false;
    bool resettingGame = false;
    int musicCounter = -1;
    float levelPercentage;
    int infinitePlatformNumber;
    string systemPlatformType;
    int infiniteCoinsCollected;
    float infiniteMoneyEarnt;
    int leveledCoinsCollected;
    float leveledMoneyEarnt;
    GameObject endZoneRef;

    void Start() {
        //Set the platform type for the version label
        SetPlatformType();
        //Make the player not move
        playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
        //Update version label
        settingsVersionText.text = systemPlatformType + version;
    }

    void Update() {
        //Hide all screens and cameras
        HideAllScreens();
        HideAllCameras();
        //RoomControl(); Removed due to easter egg room being the only room

        //Per mode logic
        //A "mode" is a game state
        if (mode == "newGame") {
            //Make sure only the new game menu and camera is shown
            if (newGameMenuRef.activeSelf == false) {
                newGameMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Check if anything in the name input field and if so then allow for the start of the game
            if (newGameNameInputField.text == null || newGameNameInputField.text.Trim() == "") {
                newGameStartButton.GetComponent<Animator>().SetBool("anythingInInputBox", false);
                newGameNameInputField.Select();
            } else {
                newGameStartButton.GetComponent<Animator>().SetBool("anythingInInputBox", true);
                if (Input.GetKeyDown(newGameSubmitNameKey) || Input.GetKeyDown(newGameSubmitNameAltKey)) {
                    //Start game and load data
                    StartNewGame();
                    gameDataControllerRef.LoadGameData();
                }
            }

        } else if (mode == "mainMenu") {
            //Make sure only the main menu and camera is shown
            if (mainMenuRef.activeSelf == false) {
                mainMenuRef.SetActive(true);
            }
            mainMenuRoomCamera.SetActive(true);

            /*if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.B)) {
                SwitchScreensWithDelay("easterEgg");
            }*/

            //Keys
            if (Input.GetKeyDown(openPlayMenu)) { //Open the play menu
                SwitchScreensWithDelay("playMenu");
            }

            //Update UI for currency, best score, and best level
            mainMenuCurrecyText.text = "CURRENCY: $" + Mathf.RoundToInt(currency).ToString();
            mainMenuBestScoreText.text = "BEST SCORE: " + bestScore.ToString();
            mainMenuBestLevelText.text = "BEST LEVEL: " + maxLevel.ToString();

        } else if (mode == "playerColourMenu") {
            //Make sure only the player colour menu and camera is shown
            if (playerColourMenuRef.activeSelf == false) {
                playerColourMenuRef.SetActive(true);
            }
            playerColourRoomCamera.SetActive(true);

            //Update material name and display material for display screen
            playerColourMenuSelectedColourText.text = playerMaterialNames[pickingPlayerMaterial];
            colourDisplayBall.GetComponent<Renderer>().material = playerMaterials[pickingPlayerMaterial];

            //Hides the "current"ly selected material text
            playerColourMenuCurrentText.SetActive(false);

            //Checks if material is unlocked already or not
            if (playerMaterialUnlocked[pickingPlayerMaterial] == true) {
                //Hide the buy button
                playerColourMenuBuyColour.gameObject.SetActive(false);

                //Checks if it's the currently active material and if so then display "current" text and hide the pick button
                if (currentPlayerMaterial == pickingPlayerMaterial) {
                    playerColourMenuCurrentText.SetActive(true);
                    playerColourMenuPickColourButton.gameObject.SetActive(false);

                    //Play with material key
                    //Allow the player to select this material and go back to the main menu
                    if (Input.GetKeyDown(playWithMaterialKey)) { 
                        PlayWithPlayerMaterial();
                    }
                } else {
                    //Show the pick colour button so the player can select the already unlocked colour
                    playerColourMenuPickColourButton.gameObject.SetActive(true);

                    //Pick material key
                    //Allow the player to select this material and go back to the main menu
                    if (Input.GetKeyDown(pickColourKey)) {
                        PickPlayerMaterial();
                    }
                }

            } else {
                //Display material price
                playerColourMenuBuyColourPriceText.text = "BUY FOR $" + playerMaterialCost[pickingPlayerMaterial];
                //Hide pick button
                playerColourMenuPickColourButton.gameObject.SetActive(false);
                //Show buy button
                playerColourMenuBuyColour.gameObject.SetActive(true);
                //Buy material key - allows the player to buy the colour
                if (Input.GetKeyDown(buyColourKey)) {
                    BuyPlayerMaterial();
                }
            }

            //Next material key
            if (Input.GetKeyDown(nextColourKey)) { //Show the next colour
                ChangePlayerColour(1);
            }
            //Previous material key
            if (Input.GetKeyDown(beforeColourKey)) { //Show the previous colour
                ChangePlayerColour(-1);
            }

            //Back key
            if (Input.GetKeyDown(backButtonKey)) { //Play with the already selected colour
                PlayWithPlayerMaterial();
            }

            //Update player currency amount UI
            colourMenuCurrecyText.text = "CURRENCY: $" + Mathf.RoundToInt(currency).ToString();

        } else if (mode == "leaderboard") {
            //Make sure only the leaderboard menu and menu camera is shown
            if (leaderboardMenuRef.activeSelf == false) {
                leaderboardMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(backButtonKey)) { //Goes back to the main menu
                SwitchScreens("mainMenu");
            }

        } else if (mode == "settings") {
            //Make sure only the settings menu and menu camera is shown
            if (settingMenuRef.activeSelf == false) {
                settingMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Set the correct colour for the particle control button
            if (particlesEnabled == true) {
                settingsParticlesButton.GetComponent<Image>().color = settingsEnabledColor;
            } else {
                settingsParticlesButton.GetComponent<Image>().color = settingsDisabledColor;
            }

            //Set the correct colour for the music mute button
            if (musicEnabled == true) {
                settingsMusicButton.GetComponent<Image>().color = settingsEnabledColor;
            } else {
                settingsMusicButton.GetComponent<Image>().color = settingsDisabledColor;
            }

            //Set the correct colour for the full screen control button
            if (fullScreenEnabled == true) {
                settingsFullScreenButton.GetComponent<Image>().color = settingsEnabledColor;
                Screen.fullScreen = true;
            } else {
                settingsFullScreenButton.GetComponent<Image>().color = settingsDisabledColor;
                Screen.fullScreen = false;
            }

            //Set the correct colour for the touch screen control button
            /*if (touchScreenModeEnabled == true) {
                settingsTouchScreenModeButton.GetComponent<Image>().color = settingsEnabledColor;
            } else {
                settingsTouchScreenModeButton.GetComponent<Image>().color = settingsDisabledColor;
            }*/

            //Check if the player is currently changing their name, else allow going back to the main menu
            if (changingName == true) {
                //Enable the change name animation if anything is in the box
                if (settingsNameChangeInputField.text == null || settingsNameChangeInputField.text.Trim() == "") {
                    changeNameMenuRef.GetComponent<Animator>().SetBool("anythingInInputBox", false);
                    settingsNameChangeInputField.Select();
                } else {
                    changeNameMenuRef.GetComponent<Animator>().SetBool("anythingInInputBox", true);
                }

                //If the change name key is pressed then check if the name can be changed
                if (Input.GetKeyDown(settingsSubmitNewNameKey) || Input.GetKeyDown(settingsSubmitNewNameAltKey)) {
                    ChangeSetting("changeNameSubmitted");
                }

            } else {
                //Keys
                if (Input.GetKeyDown(backButtonKey)) { //Go back to the main menu
                    SwitchScreens("mainMenu");
                }
            }

            //Update current player name UI
            settingsCurrentNameText.text = playerName;

            /*} else if (mode == "easterEgg") {
                if (easterEggMenuRef.activeSelf == false) {
                    easterEggMenuRef.SetActive(true);
                }
                easterEggCamera.SetActive(true);

                //Keys
                if (Input.GetKeyDown(backButtonKey)) {
                    SwitchScreens("mainMenu");
                }*/

        } else if (mode == "playMenu") {
            //Make sure only the play menu and main menu camera is shown
            if (playMenuRef.activeSelf == false) {
                playMenuRef.SetActive(true);
            }
            mainMenuRoomCamera.SetActive(true);

            /*if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.B)) {
                SwitchScreensWithDelay("easterEgg");
            }*/

            //Keys
            if (Input.GetKeyDown(startLeveledKey)) { //Opens the level game mode picker menu
                SwitchScreensWithDelay("levelPicker");
            }
            if (Input.GetKeyDown(startInfiniteKey)) { //Opens the infinite game mode menu
                SwitchScreensWithDelay("infinite");
            }
            if (Input.GetKeyDown(backButtonKey)) { //Goes back to the main menu
                SwitchScreensWithDelay("mainMenu");
            }

            //Update UI for currency, best score, and best level
            playMenuCurrecyText.text = "CURRENCY: $" + Mathf.RoundToInt(currency).ToString();
            playMenuBestScoreText.text = "BEST SCORE: " + bestScore.ToString();
            playMenuBestLevelText.text = "BEST LEVEL: " + maxLevel.ToString();

        } else if (mode == "levelPicker") {
            //Make sure only the level picker menu and menu camera is shown
            if (levelPickerMenuRef.activeSelf == false) {
                levelPickerMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Hide or show the increase or decrease buttons depending on if they should be pressed
            if (level > 1) { //Show decrease level button
                levelPickerDecreaseLevelButton.SetActive(true);
            } else {
                levelPickerDecreaseLevelButton.SetActive(false);
            }
            if (level != maxLevel) { //Show next level button
                levelPickerIncreaseLevelButton.SetActive(true);
            } else {
                levelPickerIncreaseLevelButton.SetActive(false);
            }

            //Keys
            if (Input.GetKeyDown(pickNextLevelKey)) { //Increase the level selector
                LevelPicker(1); //Change level logic in its own function so Unity buttons can access it as well
            }
            if (Input.GetKeyDown(pickPreviousLevelKey)) { //Decrease the level selector
                LevelPicker(-1);
            }
            if (Input.GetKeyDown(pickLevelKey)) { //Start the level
                StartGame("leveled");
            }
            if (Input.GetKeyDown(backButtonKey)) { //Go back to the main menu
                SwitchScreens("mainMenu");
            }

            //Update currently selected level UI
            levelPickerLevelText.text = level.ToString();

        } else if (mode == "infinite") {
            //Make sure only the infinite game UI and game camera is shown
            if (infiniteGameMenuRef.activeSelf == false) {
                infiniteGameMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);

            //Update the current score for the player
            score = Mathf.RoundToInt(playerScriptRef.transform.position.z + 25);

            //Keys
            if (Input.GetKeyDown(pauseKey)) {
                SwitchScreens("infinitePaused"); //Pause the game
            }

            //Move the player
            playerScriptRef.Movement();

            //Apply the in game effects
            UpdateEffects();

            //TouchScreenMode("infinite");

            //Update infinite game mode UI for current score and best score
            infiniteGameScoreText.text = score.ToString();
            infiniteGameBestScoreText.text = bestScore.ToString();

        } else if (mode == "leveled") {
            //Make sure only the level game UI and game camera is shown
            if (leveledGameMenuRef.activeSelf == false) {
                leveledGameMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);

            //Calculate how much percentage of distance is left to the end zone
            levelPercentage = (playerScriptRef.transform.position.z + 25) / (endZoneRef.transform.position.z + 20) * 100;

            //Keys
            if (Input.GetKeyDown(pauseKey)) {
                SwitchScreens("leveledPaused"); //Pauses the game
            }

            //Move the player
            playerScriptRef.Movement();

            //Apply the in game effects
            UpdateEffects();

            //TouchScreenMode("leveled");

            //Update leveled game mode UI for current level and completion percentage
            leveledGameLevelText.text = level.ToString();
            leveledGamePercentageText.text = Mathf.Round(levelPercentage).ToString() + "%";

        } else if (mode == "infinitePaused") {
            //Make sure only the pause menu and game camera is shown
            if (pauseMenuRef.activeSelf == false) {
                pauseMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);

            //Return to the game / unpauses the game
            if (Input.GetKeyDown(returnToGameKey)) {
                BackToGame();
            }

        } else if (mode == "leveledPaused") {
            //Make sure only the pause menu and game camera is shown
            if (pauseMenuRef.activeSelf == false) {
                pauseMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);

            //Return to the game / unpauses the game
            if (Input.GetKeyDown(returnToGameKey)) {
                BackToGame();
            }

        } else if (mode == "infiniteGameOver") {
            //Make sure only the infinite game over menu and menu camera is shown
            if (infiniteGameOverMenuRef.activeSelf == false) {
                infiniteGameOverMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(pickLevelKey)) { //Start another infinite game
                StartGame("infinite");
            }
            if (Input.GetKeyDown(backButtonKey)) { //Go back to the main menu
                SwitchScreens("mainMenu");
            }

            //Update UI for the infinite game mode's game over screen
            //Updates score, best score, coins collected, and money earnt during the game
            infiniteGameOverScoreText.text = score.ToString();
            infiniteGameOverBestScoreText.text = bestScore.ToString();
            infiniteGameOverCoinsCollectedText.text = infiniteCoinsCollected.ToString();
            infiniteGameOverMoneyEarntText.text = "$" + Mathf.RoundToInt(infiniteMoneyEarnt).ToString();

        } else if (mode == "leveledGameOver") {
            //Make sure only the leveled game over menu and menu camera is shown
            if (leveledGameOverMenuRef.activeSelf == false) {
                leveledGameOverMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(tryAgainKey)) { //Play another leveled game of the same level
                StartGame("leveled");
            }
            if (Input.GetKeyDown(backButtonKey)) { //Go back to the main menu
                SwitchScreens("mainMenu");
            }

            //Update the leveled game mode's game over UI
            //Updates the current level, coins collected, and money earnt during the game
            leveledGameOverLevelText.text = level.ToString();
            leveledGameOverCoinsCollectedText.text = leveledCoinsCollected.ToString();
            leveledGameOverMoneyEarntText.text = "$" + Mathf.RoundToInt(leveledMoneyEarnt).ToString();

        } else if (mode == "leveledWin") {
            //Make sure only the leveled win menu and menu camera is shown
            if (leveledWinMenuRef.activeSelf == false) {
                leveledWinMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(nextLevelKey)) { //Go to the next level if allowed
                NextLevel();
            }
            if (Input.GetKeyDown(backButtonKey)) { //Go back to the main menu
                SwitchScreens("mainMenu");
            }

            //Update the leveled game mode's win UI
            //Updates the current level, coins collected, and money earnt during the game
            leveledWinLevelText.text = level.ToString();
            leveledWinCoinsCollectedText.text = leveledCoinsCollected.ToString();
            leveledWinMoneyEarntText.text = "$" + Mathf.RoundToInt(leveledMoneyEarnt).ToString();

            //Display the next button if there is a next level
            leveledWinNextGameButton.gameObject.SetActive(level <= maxLevel);
        }

        //Move the audio listener
        MoveAudioListener();
        //Update the music source
        UpdateMusic();
    }

    //Update the system platform the game is being ran on
    void SetPlatformType() {
        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor)) {
            systemPlatformType = "PC";
        } else if (Application.platform == RuntimePlatform.WebGLPlayer) {
            systemPlatformType = "WGL";
        } else {
            systemPlatformType = "Unknown";
        }
    }

    //Hide all UI screens
    void HideAllScreens() {
        if (mode != "newGame") {
            newGameMenuRef.SetActive(false);
        }
        if (mode != "mainMenu") {
            mainMenuRef.SetActive(false);
        }
        if (mode != "playMenu") {
            playMenuRef.SetActive(false);
        }
        if ((mode != "infinitePaused") || (mode != "leveledPaused")) {
            pauseMenuRef.SetActive(false);
        }
        if (mode != "levelPicker") {
            levelPickerMenuRef.SetActive(false);
        }
        if (mode != "infinite") {
            infiniteGameMenuRef.SetActive(false);
        }
        if (mode != "leveled") {
            leveledGameMenuRef.SetActive(false);
        }
        if (mode != "infiniteGameOver") {
            infiniteGameOverMenuRef.SetActive(false);
        }
        if (mode != "leveledGameOver") {
            leveledGameOverMenuRef.SetActive(false);
        }
        if (mode != "leveledWin") {
            leveledWinMenuRef.SetActive(false);
        }
        if (mode != "playerColourMenu") {
            playerColourMenuRef.SetActive(false);
        }
        if (mode != "leaderboard") {
            leaderboardMenuRef.SetActive(false);
        }
        if (mode != "settings") {
            settingMenuRef.SetActive(false);
        }
        if (changingName == false) {
            changeNameMenuRef.SetActive(false);
        }
        if (resettingGame == false) {
            resetGameMenuRef.SetActive(false);
        }
        /*if (mode != "easterEgg") {
            easterEggMenuRef.SetActive(false);
        }*/
    }

    //Hide all cameras
    void HideAllCameras() {
        mainMenuRoomCamera.SetActive(false);
        playerColourRoomCamera.SetActive(false);
        gameCamera.SetActive(false);
        //easterEggCamera.SetActive(false);
    }

    //Update and apply the in game effects
    void UpdateEffects() {
        //mainLight.color = new Color(Mathf.Lerp(mainLight.color.r, newLightColor.r, lightColourChangeTime * 0.1f * Time.deltaTime), Mathf.Lerp(mainLight.color.g, newLightColor.g, lightColourChangeTime * 0.1f * Time.deltaTime), Mathf.Lerp(mainLight.color.b, newLightColor.b, lightColourChangeTime * 0.1f * Time.deltaTime), Mathf.Lerp(mainLight.color.a, newLightColor.a, lightColourChangeTime * 0.1f * Time.deltaTime));
    }

    //Resets the effects so they aren't applied out of game
    void ResetEffects() {
        mainLight.color = defaultLightColor;
    }

    /*
    void RoomControl() {
        if (mode == "easterEgg") {
            easterEggRoomRef.SetActive(true);
        } else {
            easterEggRoomRef.SetActive(false);
        }
    }*/

    //Move the audio listener to where it needs to be in the scene
    //For example in game it should be on the player
    void MoveAudioListener() {
        if ((mode == "newGame") || (mode == "levelPicker") || (mode == "leveledGameOver") || (mode == "infiniteGameOver") || (mode == "leveledWin") || (mode == "playerColourMenu") || (mode == "leaderboard") || (mode == "settings")) {
            audioListenerRef.transform.position = menuCamera.transform.position;
        } else if (mode == "mainMenu") {
            audioListenerRef.transform.position = mainMenuRoomCamera.transform.position;
        } else if ((mode == "infinite") || (mode == "leveled") || (mode == "infinitePaused") || (mode == "leveledPaused")) {
            audioListenerRef.transform.position = gameCamera.transform.position;
        }
        /* else if (mode == "easterEgg") {
            audioListenerRef.transform.position = easterEggCamera.transform.position;
        }*/
    }

    //Function used to start a game
    //Used via Unity buttions and via script
    public void StartGame(string type) {
        //Make sure a game isn't already being started
        if (inGame == false) {
            //Allow the player to move and reset its velocity
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = false;
            playerScriptRef.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //Start the correct type of game
            if (type == "infinite") {
                //Start infinite game
                inGame = true;
                //Reset current game values
                infiniteCoinsCollected = 0;
                infiniteMoneyEarnt = 0;
                //Switch screens and spawn start platforms
                SwitchScreens("infinite");
                InfinitePlatformStartSpawn();

            } else if (type == "leveled") {
                //Start leveled game
                inGame = true;
                //Reset current game values
                leveledCoinsCollected = 0;
                leveledMoneyEarnt = 0;
                //Switch screens and spawn start platforms
                SwitchScreens("leveled");
                LeveledPlatformStartSpawn();
            }
        }
    }

    //Switch UI screens
    public void SwitchScreens(string newScreen) {
        mode = newScreen;
    }

    //Switch UI screens with a delay
    public void SwitchScreensWithDelay(string newScreen) {
        if (mode == "mainMenu") {
            mainMenuRef.GetComponent<Animator>().SetBool("switchScreens", true);
            StartCoroutine(SwitchScreensWithDelayController(0.8f, newScreen, null));
        } else if (mode == "playMenu") {
            playMenuRef.GetComponent<Animator>().SetBool("switchScreens", true);
            if (newScreen == "infinite") {
                StartCoroutine(SwitchScreensWithDelayController(0.8f, null, "startInfinite"));
            } else {
                StartCoroutine(SwitchScreensWithDelayController(0.8f, newScreen, null));
            }
        } else if (mode == "settings") {
            playMenuRef.GetComponent<Animator>().SetBool("switchScreens", true);
            StartCoroutine(SwitchScreensWithDelayController(1f, newScreen, null));
        }
    }

    //Switch UI screens IEnumerator
    IEnumerator SwitchScreensWithDelayController(float t, string newScreen, string function) {
        yield return new WaitForSeconds(t);
        if ((newScreen != "") || (newScreen != null)) {
            SwitchScreens(newScreen);
        }
        if ((function != "") || (function != null)) {
            if (function == "startInfinite") {
                StartGame("infinite");
            }
        }
    }

    //Unpauses the game
    public void BackToGame() {
        if (mode == "leveledPaused") {
            SwitchScreens("leveled");
        } else if (mode == "infinitePaused") {
            SwitchScreens("infinite");
        }
    }

    //Go to main menu from pause menu
    public void PauseToMainMenu() {
        //Reset the player
        playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
        playerScriptRef.transform.position = startPosition;
        inGame = false;
        //Delete all the platforms
        DeleteAllPlatform();
        //Switch screens
        SwitchScreens("mainMenu");
    }

    //Go to the next level
    public void NextLevel() {
        level++;
        StartGame("leveled");
    }

    //Change the currently selected level
    public void LevelPicker(int num) {
        //Make sure new num is valid and doesn't cause an over/underflow
        if ((num < 0) && (level != 1)) {
            level += num;
        } else if ((num > 0) && (level != maxLevel)) {
            level += num;
        }
    }

    //Spawn the start platforms for the infinite game mode
    void InfinitePlatformStartSpawn() {
        GameObject instantiatedPlatform;
        //Spawn x amount of platforms (amount depends on variable called "infinitePlatformStartAmount")
        for (int i = 0; i < infinitePlatformStartAmount; i++) {
            //Spawn platform and tag it
            instantiatedPlatform = Instantiate(platforms[Mathf.RoundToInt(Random.Range(0, platforms.Length))], new Vector3(0, -4.5f * i, 25 * i), Quaternion.Euler(10.204f, 0, 0));
            instantiatedPlatform.tag = platformTag;

            //Spawn background objects on platform
            SpawnBackgroundObjects(i, instantiatedPlatform);
            //Spawn coins that go on the platform
            SpawnCoins(i, instantiatedPlatform);
            //Spawn ground details (little rocks)
            int numOfGroundDetails = Random.Range(1, groundDetailSpawnRate);
            for (int j = 0; j < numOfGroundDetails; j++) {
                SpawnGroundDetails(i, instantiatedPlatform);
            }
        }

        //Set the platform count
        infinitePlatformNumber = infinitePlatformStartAmount;
    }

    //Spawn the start platforms for the leveled game mode
    void LeveledPlatformStartSpawn() {
        //Spawn x amount of platforms (amount depends on the current level)
        GameObject instantiatedPlatform;
        for (int i = 0; i < level; i++) {
            //Spawn platform and tag it
            instantiatedPlatform = Instantiate(platforms[Mathf.RoundToInt(Random.Range(0, platforms.Length))], new Vector3(0, -4.5f * i, 25 * i), Quaternion.Euler(10.204f, 0, 0));
            instantiatedPlatform.tag = platformTag;

            //Spawn background objects on platform
            SpawnBackgroundObjects(i, instantiatedPlatform, 5); //Chance of 1/6 compared to the default of 1/4
            //Spawn coins that go on the platform
            SpawnCoins(i, instantiatedPlatform);
            //Spawn ground details (little rocks)
            int numOfGroundDetails = Random.Range(1, groundDetailSpawnRate);
            for (int j = 0; j < numOfGroundDetails; j++) {
                SpawnGroundDetails(i, instantiatedPlatform);
            }
        }

        //Spawn the end platform then tag it
        instantiatedPlatform = Instantiate(endPlatform[Mathf.RoundToInt(Random.Range(0, endPlatform.Length))], new Vector3(0, -4.5f * level, 25 * level), Quaternion.Euler(10.204f, 0, 0));
        instantiatedPlatform.tag = platformTag;

        //Find the trigger within the end platform and save it
        endZoneRef = GameObject.FindGameObjectWithTag(endZoneTag);
    }

    //Spawn background objects
    void SpawnBackgroundObjects(int platformNumber, GameObject newParent, int chance = 3) {
        if (Mathf.RoundToInt(Random.Range(0, chance)) == 0) { //25% chance of spawning by default
            //Spawn background object and then tag and parent it
            GameObject instantiatedBackground;
            instantiatedBackground = Instantiate(backgrounds[Mathf.RoundToInt(Random.Range(0, backgrounds.Length))], new Vector3(0, -4.5f * platformNumber, 25 * platformNumber), Quaternion.Euler(0, 0, 0));
            instantiatedBackground.tag = backgroundTag;
            instantiatedBackground.transform.parent = newParent.transform;
        }
    }

    //Spawn coins
    void SpawnCoins(int num, GameObject newParent) {
        //Spawn coins depending on coin spawn rate
        if (Mathf.RoundToInt(Random.Range(0, coinSpawnRate)) == 0) {
            //Get a random location on the platform
            float rndYZ = Random.Range(-0.5f, 0.5f);
            //Spawn the coin then tag and parent it
            GameObject instantiatedCoin;
            instantiatedCoin = Instantiate(coinPrefab, new Vector3(Random.Range(-3.5f, 3.5f), -4.5f * (num + rndYZ), 25 * (num + rndYZ)), Quaternion.Euler(10.204f, 0, 0));
            instantiatedCoin.tag = coinTag;
            instantiatedCoin.transform.parent = newParent.transform;
        }
    }

    //Spawn ground details (rocks)
    void SpawnGroundDetails(int platformNumber, GameObject newParent) {
        //Find a random location on the platform
        float rndYZ = Random.Range(-0.5f, 0.5f);
        //Spawn and parent the detail
        GameObject instantiatedDetail;
        instantiatedDetail = Instantiate(groundDetails[Random.Range(0, groundDetails.Length)], new Vector3(Random.Range(-3.8f, 3.8f), -4.5f * (platformNumber + rndYZ), 25 * (platformNumber + rndYZ)), Quaternion.Euler(10.204f, 0, 0));
        instantiatedDetail.transform.parent = newParent.transform;
    }

    //Delete all spawned platforms
    void DeleteAllPlatform() {
        //Get all objects that need to be deleted then delete
        GameObject[] needToBeDeletedObjects;
        needToBeDeletedObjects = GameObject.FindGameObjectsWithTag(platformTag); //Delete platforms
        for (int i = 0; i < needToBeDeletedObjects.Length; i++) {
            Destroy(needToBeDeletedObjects[i]);
        }
        needToBeDeletedObjects = GameObject.FindGameObjectsWithTag(backgroundTag); //Delete background objects
        for (int j = 0; j < needToBeDeletedObjects.Length; j++) {
            Destroy(needToBeDeletedObjects[j]);
        }
    }

    //Spawn random new platform after despawning
    void SpawnNewPlatform() {
        //Spawn platform then tag it
        GameObject instantiatedPlatform;
        instantiatedPlatform = Instantiate(platforms[Mathf.RoundToInt(Random.Range(0, platforms.Length))], new Vector3(0, -4.5f * infinitePlatformNumber, 25 * infinitePlatformNumber), Quaternion.Euler(10.204f, 0, 0));
        instantiatedPlatform.tag = platformTag;

        //Spawn the background objects
        SpawnBackgroundObjects(infinitePlatformNumber, instantiatedPlatform);
        //Spawn coins
        SpawnCoins(infinitePlatformNumber, instantiatedPlatform);
        int numOfGroundDetails = Random.Range(1, groundDetailSpawnRate);
        for (int j = 0; j < numOfGroundDetails; j++) {
            SpawnGroundDetails(infinitePlatformNumber, instantiatedPlatform);
        }

        //Increase the platform count
        infinitePlatformNumber++;
    }

    //Despawn platform
    IEnumerator DespawnPlatform(float t, GameObject objectRef) {
        yield return new WaitForSeconds(t);
        if (objectRef == null) {
            yield break;
        }

        //Check if the distance of the platform is less than 30 units
        if (Vector3.Distance(objectRef.transform.position, playerModel.transform.position) < 30) {
            StartCoroutine(DespawnPlatform(3 / platformSpeed, objectRef));
            yield break;
        }

        //Make sure the player is actually in the game
        if (inGame == false) {
            yield break;
        }

        //Make sure the mode is infinite
        if (mode == "infinite") {
            //Make sure the object still exists then despawn it
            if (objectRef != null) {
                Destroy(objectRef.transform.parent.parent.gameObject);
                SpawnNewPlatform();
            } else {
                yield break;
            }
        } else {
            yield break;
        }
    }

    //Change the player colour
    public void ChangePlayerColour(int amount) {
        if (amount < 0 && pickingPlayerMaterial == 0) {
            pickingPlayerMaterial = playerMaterialNames.Length - 1;
        } else if (amount < 0) {
            pickingPlayerMaterial--;
        }
        if (amount > 0 && pickingPlayerMaterial == playerMaterialNames.Length - 1) {
            pickingPlayerMaterial = 0;
        } else if (amount > 0) {
            pickingPlayerMaterial++;
        }
    }

    //Select the colour from the player colour menu
    public void PickPlayerMaterial() {
        currentPlayerMaterial = pickingPlayerMaterial;
    }

    //Play with the selected colour
    public void PlayWithPlayerMaterial() {
        playerModel.GetComponent<Renderer>().material = playerMaterials[currentPlayerMaterial];
        SwitchScreens("mainMenu");
    }

    //Buy an locked colour
    public void BuyPlayerMaterial() {
        for (int i = 0; i < playerMaterialNames.Length; i++) {
            if (i == pickingPlayerMaterial) {
                //Make sure the player has enough money
                if (currency >= playerMaterialCost[i]) {
                    //Charge the player and unlock the colour
                    currency -= playerMaterialCost[i];
                    playerMaterialUnlocked[i] = true;
                }
            }
        }
    }

    //Return to main menu from colour menu
    public void ExitPlayerColourMenu() {
        SwitchScreens("mainMenu");
    }

    //Function used with Unity buttons and internal code to alow for changing of settings via one function
    public void ChangeSetting(string setting) {
        if (setting == "particles") {
            particlesEnabled = !particlesEnabled;
        } else if (setting == "music") {
            musicEnabled = !musicEnabled;
        } else if (setting == "fullScreen") {
            fullScreenEnabled = !fullScreenEnabled;
            /*}else if (setting == "touchScreenMode") {
                touchScreenModeEnabled = !touchScreenModeEnabled;
            */
        } else if (setting == "changeName") {
            //Enable the changing name screen
            if (changingName == false) {
                changingName = true;
                changeNameMenuRef.SetActive(true);
                settingsNameChangeInputField.readOnly = false;
            }
        } else if (setting == "changeNameSubmitted") {
            //Save the name change is all values are allowed
            if (settingsNameChangeInputField.text != null && settingsNameChangeInputField.text.Trim() != "") {
                if (changeNameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                    settingsNameChangeInputField.readOnly = true;
                    changeNameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                    Invoke(nameof(HideChangeNameMenu), changeNameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
                    playerName = settingsNameChangeInputField.text.Trim();
                }
            }
        } else if (setting == "changeNameCanceled") {
            //Cancel the name change
            if (changeNameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                settingsNameChangeInputField.readOnly = true;
                changeNameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                Invoke(nameof(HideChangeNameMenu), changeNameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
            }
        } else if (setting == "resetGame") {
            //Open the reset game menu
            if (resettingGame == false) {
                resettingGame = true;
                resetGameMenuRef.SetActive(true);
            }
        } else if (setting == "resetGameSubmitted") {
            //Submit the game reset
            if (resetGameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                resetGameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                Invoke(nameof(HideResetGameMenu), resetGameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
                gameDataControllerRef.ResetGameData();
            }
        } else if (setting == "resetGameCanceled") {
            //Cancel the game reset
            if (resetGameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                resetGameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                Invoke(nameof(HideResetGameMenu), resetGameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
            }
        }

        //Save the game
        gameDataControllerRef.SaveGameData();
    }

    //Hide the name change menu
    void HideChangeNameMenu() {
        changingName = false;
        settingsNameChangeInputField.text = null;
    }

    //Hide the game reset menu
    void HideResetGameMenu() {
        resettingGame = false;
    }

    //Start the game (goes to main menu)
    public void StartNewGame() {
        newGameNameInputField.readOnly = true;
        playerName = newGameNameInputField.text.Trim();
        newGameNameInputField.text = null;
        SwitchScreens("mainMenu");
    }

    //Update the music's info (volume, paused, muted)
    void UpdateMusic() {
        if (musicEnabled == true) {
            musicSource.UnPause();
            if (music.Length > 0) {
                musicSource.volume = volume;
                if (musicSource.isPlaying == false) {
                    NextSong();
                }
                if (playNextSong == true) {
                    NextSong();
                    playNextSong = false;
                }
            }
        } else {
            musicSource.Pause();
        }
    }

    //Skip the song
    public void NextSong() {
        musicCounter++;
        if (musicCounter >= music.Length) {
            musicCounter = 0;
        }
        musicSource.clip = music[musicCounter];
        musicSource.Play();
    }

    /*void TouchScreenMode(string gameMode) {
        if (touchScreenModeEnabled == true) {
            if (gameMode == "infinite") {
                infiniteGameLeftMovementButton.gameObject.SetActive(true);
                infiniteGameRightMovementButton.gameObject.SetActive(true);
            } else if (gameMode == "leveled") {
                leveledGameLeftMovementButton.gameObject.SetActive(true);
                leveledGameRightMovementButton.gameObject.SetActive(true);
            }
        } else {
            if (gameMode == "infinite") {
                infiniteGameLeftMovementButton.gameObject.SetActive(false);
                infiniteGameRightMovementButton.gameObject.SetActive(false);
            } else if (gameMode == "leveled") {
                leveledGameLeftMovementButton.gameObject.SetActive(false);
                leveledGameRightMovementButton.gameObject.SetActive(false);
            }
        }
    }*/

    //Check for a collision enter
    public void CollisionEnter(Collision collision) {
        //Check for gameover via hitting an obstacle or falling out of the map
        if (collision.gameObject.CompareTag(obstacleTag) || collision.gameObject.CompareTag(outOfAreaTag)) {
            //Stop the player moving
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            playerScriptRef.transform.position = startPosition;
            inGame = false;

            //Check which mode
            if (mode == "infinite") {
                //Switch screens
                SwitchScreens("infiniteGameOver");

                //Set new best score if better than current
                if (score >= bestScore) {
                    bestScore = score;
                }

                //Add money and update the display money
                currency += score * infiniteDistantCurrencyAmount;
                infiniteMoneyEarnt += score * infiniteDistantCurrencyAmount;

                //Delete all platforms
                DeleteAllPlatform();

                //Save the game
                gameDataControllerRef.SaveGameData();

            } else if (mode == "leveled") {
                //Switch screens
                SwitchScreens("leveledGameOver");
                //Update the game percentage
                leveledGameOverPercentageText.text = Mathf.Round(levelPercentage).ToString() + "%";
                //Delete all platforms
                DeleteAllPlatform();
                //Save the game
                gameDataControllerRef.SaveGameData();
            }
        }

        //Despawn and spawn a new platform
        if (mode == "infinite") {
            if (collision.gameObject.CompareTag(groundTag)) {
                StartCoroutine(DespawnPlatform(3 / platformSpeed, collision.gameObject));
            }
        }
    }

    //Player trigger enter
    public void TriggerEnter(Collider other) {
        //Check for win via the end zone
        if (mode == "leveled") {
            if (other.gameObject.CompareTag(endZoneTag)) {
                //Stop the player moving
                playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
                playerScriptRef.transform.position = startPosition;
                inGame = false;

                //Increase max level if on max level
                if (level == maxLevel) {
                    maxLevel++;
                }

                //Show level win menu
                SwitchScreens("leveledWin");

                //Update money amounts
                currency += leveledPlatformCurrencyAmount * level;
                leveledMoneyEarnt = leveledPlatformCurrencyAmount * level;

                //Delete all platforms
                DeleteAllPlatform();

                //Save the game
                gameDataControllerRef.SaveGameData();
            }
        }

        //Check for coin pick up
        if (other.gameObject.CompareTag(coinTag)) {
            //Increase the currency amount
            currency += coinCurrencyAmount;

            //Add the amount to the correct display variable
            if (mode == "infinite") {
                infiniteCoinsCollected++;
                infiniteMoneyEarnt += coinCurrencyAmount;
            } else if (mode == "leveled") {
                leveledCoinsCollected++;
                leveledMoneyEarnt += coinCurrencyAmount;
            }

            //Destroy the coin
            Destroy(other.gameObject);
        }
    }
}
