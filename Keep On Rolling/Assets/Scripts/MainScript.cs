using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainScript : MonoBehaviour {

    [Header("Player Info")]
    public Vector3 startPosion;
    public string mode = "menu";
    public string version;
    public int bestScore;
    public int score;
    public float currency;
    public float maxMovementSpeed;
    public float movementSpeed;
    public float slowDownMovementSpeed;
    public float touchMovementSpeed;
    public float touchSlowDownMovementSpeed;
    public float rollSpeed;
    public float maxRollSpeed;
    public float platformSpeed;
    public float maxPlatformSpeed;
    public float jumpSpeed;
    public int level = 1;
    public int maxLevel = 1;

    [Header("UI Refs")]
    public GameObject newGameMenuRef;
    public GameObject startMenuRef;
    public GameObject playMenuRef;
    public GameObject pauseMenuRef;
    public GameObject levelPickerMenuRef;
    public GameObject infiniteGameMenuRef;
    public GameObject leveledGameMenuRef;
    public GameObject infiniteGameOverMenuRef;
    public GameObject leveledGameOverMenuRef;
    public GameObject leveledWinMenuRef;
    public GameObject playerColourMenuRef;
    public GameObject leaderboardMenuRef;
    public GameObject settingMenuRef;
    public GameObject changeNameMenuRef;
    public GameObject resetGameMenuRef;
    public GameObject easterEggMenuRef;
    public InputField newGameNameInputField;
    public Button newGameStartButton;
    public Text mainMenuCurrecyText;
    public Text mainMenuBestScoreText;
    public Text mainMenuBestLevelText;
    public Text mainMenuVersionText;
    public Text playMenuCurrecyText;
    public Text playMenuBestScoreText;
    public Text playMenuBestLevelText;
    public Text playMenuVersionText;
    public Text levelPickerLevelText;
    public Text infiniteGameScoreText;
    public Text infiniteGameBestScoreText;
    public Button infiniteGameLeftMovementButton;
    public Button infiniteGameRightMovementButton;
    public Text leveledGameLevelText;
    public Text leveledGamePercentageText;
    public Button leveledGameLeftMovementButton;
    public Button leveledGameRightMovementButton;
    public Text infiniteGameOverScoreText;
    public Text infiniteGameOverBestScoreText;
    public Text infiniteGameOverCoinsCollectedText;
    public Text infiniteGameOverMoneyEarntText;
    public Text leveledGameOverLevelText;
    public Text leveledGameOverPercentageText;
    public Text leveledGameOverCoinsCollectedText;
    public Text leveledGameOverMoneyEarntText;
    public Text leveledWinLevelText;
    public Text leveledWinPercentageText;
    public Text leveledWinCoinsCollectedText;
    public Text leveledWinMoneyEarntText;
    public Button leveledWinNextGameButton;
    public Text colourMenuCurrecyText;
    public Text playerColourMenuSelectedColourText;
    public Button playerColourMenuPickColourButton;
    public Button playerColourMenuBuyColour;
    public Text playerColourMenuBuyColourPriceText;
    public GameObject playerColourMenuCurrentText;
    public Button settingsParticlesButton;
    public Button settingsMusicButton;
    public Button settingsFullScreenButton;
    public Button settingsTouchScreenModeButton;
    public Text settingsCurrentNameText;
    public InputField settingsNameChangeInputField;
    public Text settingsVersionText;

    [Header("Currency Info")]
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
    public KeyCode pickBeforeLevelKey;
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
    public bool touchScreenModeEnabled;
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

    [Header("Rooms")]
    public GameObject easterEggRoomRef;

    [Header("Refs")]
    public GameObject playerModel;
    public GameObject colourDisplayBall;
    public Light mainLight;
    public GameObject audioListenerRef;
    public PlayerScript playerScriptRef;
    public GameDataController gameDataControllerRef;

    // Private variables
    float levelPercentage;
    int infinitePlatformNumber;
    int leveledPlatformNumber;
    bool newGame = true;
    int pickingPlayerMaterial = 7;
    bool changingName = false;
    bool resettingGame = false;
    int musicCounter = -1;
    int infiniteCoinsCollected;
    float infiniteMoneyEarnt;
    int leveledCoinsCollected;
    float leveledMoneyEarnt;
    string systemPlatformType;


    // Start is called before the first frame update
    void Start() {
        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor)) {
            systemPlatformType = "PC";
        } else if (Application.platform == RuntimePlatform.WebGLPlayer) {
            systemPlatformType = "WGL";
        } else {
            systemPlatformType = "Unknown";
        }
    }

    // Update is called once per frame
    void Update() {
        HideAllScreens();
        HideAllCameras();
        RoomControl();  
        if (mode == "newGame") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (newGameMenuRef.activeSelf == false) {
                newGameMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            if (newGameNameInputField.text == null || newGameNameInputField.text == "") {
                newGameStartButton.GetComponent<Animator>().SetBool("anythingInInputBox", false);
                newGameNameInputField.Select();
            } else {
                newGameStartButton.GetComponent<Animator>().SetBool("anythingInInputBox", true);
                if (Input.GetKeyDown(newGameSubmitNameKey) || Input.GetKeyDown(newGameSubmitNameAltKey)) {
                    StartNewGame();
                    gameDataControllerRef.LoadGameData();
                }
            }

        } else if (mode == "mainMenu") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (startMenuRef.activeSelf == false) {
                startMenuRef.SetActive(true);
            }
            mainMenuRoomCamera.SetActive(true);

            if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.B)) {
                SwitchScreensWithDelay("easterEgg");
            }

            //Keys
            if (Input.GetKeyDown(openPlayMenu)) {
                SwitchScreensWithDelay("playMenu");
            }

        } else if (mode == "playMenu") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (playMenuRef.activeSelf == false) {
                playMenuRef.SetActive(true);
            }
            mainMenuRoomCamera.SetActive(true);

            if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.B)) {
                SwitchScreensWithDelay("easterEgg");
            }

            //Keys
            if (Input.GetKeyDown(startLeveledKey)) {
                SwitchScreensWithDelay("levelPicker");
            }
            if (Input.GetKeyDown(startInfiniteKey)) {
                SwitchScreensWithDelay("infinite");
            }
            if (Input.GetKeyDown(backButtonKey)) {
                SwitchScreensWithDelay("mainMenu");
            }


        } else if (mode == "infinitePaused") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (pauseMenuRef.activeSelf == false) {
                pauseMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);

            if (Input.GetKeyDown(returnToGameKey)) {
                BackToGame();
            }

        } else if (mode == "leveledPaused") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (pauseMenuRef.activeSelf == false) {
                pauseMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);

            if (Input.GetKeyDown(returnToGameKey)) {
                BackToGame();
            }

        } else if (mode == "levelPicker") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (levelPickerMenuRef.activeSelf == false) {
                levelPickerMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(pickNextLevelKey)) {
                LevelPicker(1);
            }
            if (Input.GetKeyDown(pickBeforeLevelKey)) {
                LevelPicker(-1);
            }
            if (Input.GetKeyDown(pickLevelKey)) {
                StartGame("leveled");
            }
            if (Input.GetKeyDown(backButtonKey)) {
                mode = "mainMenu";
            }

        } else if (mode == "infinite") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = false;
            if (infiniteGameMenuRef.activeSelf == false) {
                infiniteGameMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);
            score = Mathf.RoundToInt(playerScriptRef.transform.position.z + 25);

            //Keys
            if (Input.GetKeyDown(pauseKey)) {
                mode = "infinitePaused";
            }

            playerScriptRef.Movement();
            Effects();
            PlatformSpeedControl();
            TouchScreenMode("infinite");

        } else if (mode == "leveled") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = false;
            if (leveledGameMenuRef.activeSelf == false) {
                leveledGameMenuRef.SetActive(true);
            }
            gameCamera.SetActive(true);
            GameObject endZoneRef;
            endZoneRef = GameObject.FindGameObjectWithTag(endZoneTag);

            levelPercentage = (playerScriptRef.transform.position.z + 25) / (endZoneRef.transform.position.z + 20) * 100;

            //Keys
            if (Input.GetKeyDown(pauseKey)) {
                mode = "leveledPaused";
            }

            playerScriptRef.Movement();
            Effects();
            TouchScreenMode("leveled");

        } else if (mode == "infiniteGameOver") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (infiniteGameOverMenuRef.activeSelf == false) {
                infiniteGameOverMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(pickLevelKey)) {
                StartGame("infinite");
            }
            if (Input.GetKeyDown(backButtonKey)) {
                mode = "mainMenu";
            }

        } else if (mode == "leveledGameOver") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (leveledGameOverMenuRef.activeSelf == false) {
                leveledGameOverMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(tryAgainKey)) {
                StartGame("leveled");
            }
            if (Input.GetKeyDown(backButtonKey)) {
                mode = "mainMenu";
            }

        } else if (mode == "leveledWin") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (leveledWinMenuRef.activeSelf == false) {
                leveledWinMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(nextLevelKey)) {
                NextLevel();
            }
            if (Input.GetKeyDown(backButtonKey)) {
                mode = "mainMenu";
            }

        } else if (mode == "playerColourMenu") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (playerColourMenuRef.activeSelf == false) {
                playerColourMenuRef.SetActive(true);
            }
            playerColourRoomCamera.SetActive(true);

            //Update material name
            playerColourMenuSelectedColourText.text = playerMaterialNames[pickingPlayerMaterial];
            //Update display material for display screen
            colourDisplayBall.GetComponent<Renderer>().material = playerMaterials[pickingPlayerMaterial];
            //Hides the current material text
            playerColourMenuCurrentText.gameObject.SetActive(false);

            //Checks if material is unlocked or not (true means it is, false means it isn't)
            if (playerMaterialUnlocked[pickingPlayerMaterial] == true) {
                //Checks if it's the current material and if so display current text and hide the pick button
                if (currentPlayerMaterial == pickingPlayerMaterial) {
                    playerColourMenuCurrentText.gameObject.SetActive(true);
                    playerColourMenuPickColourButton.gameObject.SetActive(false);
                    //Play with material key
                    if (Input.GetKeyDown(playWithMaterialKey)) {
                        PlayWithPlayerMaterial();
                    }
                } else {
                    playerColourMenuPickColourButton.gameObject.SetActive(true);
                    //Pick material key
                    if (Input.GetKeyDown(pickColourKey)) {
                        PickPlayerMaterial();
                    }
                }
                //Hide buy button
                playerColourMenuBuyColour.gameObject.SetActive(false);

            } else {
                //Display material price
                playerColourMenuBuyColourPriceText.text = "BUY FOR $" + playerMaterialCost[pickingPlayerMaterial];
                //Hide pick button
                playerColourMenuPickColourButton.gameObject.SetActive(false);
                //Show buy button
                playerColourMenuBuyColour.gameObject.SetActive(true);
                //Buy material key
                if (Input.GetKeyDown(buyColourKey)) {
                    BuyPlayerMaterial();
                }
            }

            //Next material key
            if (Input.GetKeyDown(nextColourKey)) {
                ChangePlayerColour(1);
            }
            //Previous material key
            if (Input.GetKeyDown(beforeColourKey)) {
                ChangePlayerColour(-1);
            }
            //Back key
            if (Input.GetKeyDown(backButtonKey)) {
                PlayWithPlayerMaterial();
            }

        } else if (mode == "leaderboard") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            leaderboardMenuRef.SetActive(true);
            menuCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(backButtonKey)) {
                mode = "mainMenu";
            }

        } else if (mode == "settings") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (settingMenuRef.activeSelf == false) {
                settingMenuRef.SetActive(true);
            }
            menuCamera.SetActive(true);

            if (particlesEnabled == true) {
                settingsParticlesButton.GetComponent<Image>().color = settingsEnabledColor;
            } else {
                settingsParticlesButton.GetComponent<Image>().color = settingsDisabledColor;
            }

            if (musicEnabled == true) {
                settingsMusicButton.GetComponent<Image>().color = settingsEnabledColor;
            } else {
                settingsMusicButton.GetComponent<Image>().color = settingsDisabledColor;
            }

            if (fullScreenEnabled == true) {
                settingsFullScreenButton.GetComponent<Image>().color = settingsEnabledColor;
                Screen.fullScreen = true;
            } else {
                settingsFullScreenButton.GetComponent<Image>().color = settingsDisabledColor;
                Screen.fullScreen = false;
            }

            if (touchScreenModeEnabled == true) {
                settingsTouchScreenModeButton.GetComponent<Image>().color = settingsEnabledColor;
            } else {
                settingsTouchScreenModeButton.GetComponent<Image>().color = settingsDisabledColor;
            }

            if (changingName == true) {
                if (settingsNameChangeInputField.text == null || settingsNameChangeInputField.text == "") {
                    changeNameMenuRef.GetComponent<Animator>().SetBool("anythingInInputBox", false);
                    settingsNameChangeInputField.Select();
                } else {
                    changeNameMenuRef.GetComponent<Animator>().SetBool("anythingInInputBox", true);
                }
                if (Input.GetKeyDown(settingsSubmitNewNameKey) || Input.GetKeyDown(settingsSubmitNewNameAltKey)) {
                    ChangeSetting("changeNameSubmitted");
                }
            } else {
                //Keys
                if (Input.GetKeyDown(backButtonKey)) {
                    mode = "mainMenu";
                }
            }

        } else if (mode == "easterEgg") {
            playerScriptRef.GetComponent<Rigidbody>().isKinematic = true;
            if (easterEggMenuRef.activeSelf == false) {
                easterEggMenuRef.SetActive(true);
            }
            easterEggCamera.SetActive(true);

            //Keys
            if (Input.GetKeyDown(backButtonKey)) {
                mode = "mainMenu";
            }

        }
        UpdateUI();
        AudioControl();
        MusicControl();
    }

    void HideAllScreens() {
        if (mode != "newGame") {
            newGameMenuRef.SetActive(false);
        }
        if (mode != "mainMenu") {
            startMenuRef.SetActive(false);
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
        if (mode != "easterEgg") {
            easterEggMenuRef.SetActive(false);
        }
    }

    void HideAllCameras() {
        mainMenuRoomCamera.SetActive(false);
        playerColourRoomCamera.SetActive(false);
        gameCamera.SetActive(false);
        easterEggCamera.SetActive(false);
    }

    void Effects() {
        mainLight.color = new Color(Mathf.Lerp(mainLight.color.r, newLightColor.r, lightColourChangeTime * 0.1f * Time.deltaTime), Mathf.Lerp(mainLight.color.g, newLightColor.g, lightColourChangeTime * 0.1f * Time.deltaTime), Mathf.Lerp(mainLight.color.b, newLightColor.b, lightColourChangeTime * 0.1f * Time.deltaTime), Mathf.Lerp(mainLight.color.a, newLightColor.a, lightColourChangeTime * 0.1f * Time.deltaTime));
    }

    void UpdateUI() {
        mainMenuCurrecyText.text = "CURRENCY: $" + Mathf.RoundToInt(currency).ToString();
        mainMenuBestScoreText.text = "BEST SCORE: " + bestScore.ToString();
        mainMenuBestLevelText.text = "BEST LEVEL: " + maxLevel.ToString();
        mainMenuVersionText.text = "VERSION: " + systemPlatformType + version;
        playMenuCurrecyText.text = "CURRENCY: $" + Mathf.RoundToInt(currency).ToString();
        playMenuBestScoreText.text = "BEST SCORE: " + bestScore.ToString();
        playMenuBestLevelText.text = "BEST LEVEL: " + maxLevel.ToString();
        playMenuVersionText.text = "VERSION: " + systemPlatformType + version;
        levelPickerLevelText.text = level.ToString();
        infiniteGameScoreText.text = score.ToString();
        infiniteGameBestScoreText.text = bestScore.ToString();
        leveledGameLevelText.text = level.ToString();
        leveledGamePercentageText.text = Mathf.Round(levelPercentage).ToString() + "%";
        infiniteGameOverScoreText.text = score.ToString();
        infiniteGameOverBestScoreText.text = bestScore.ToString();
        infiniteGameOverCoinsCollectedText.text = infiniteCoinsCollected.ToString();
        infiniteGameOverMoneyEarntText.text = "$" + Mathf.RoundToInt(infiniteMoneyEarnt).ToString();
        leveledGameOverLevelText.text = level.ToString();
        leveledGameOverCoinsCollectedText.text = leveledCoinsCollected.ToString();
        leveledGameOverMoneyEarntText.text = "$" + Mathf.RoundToInt(leveledMoneyEarnt).ToString();
        leveledWinLevelText.text = level.ToString();
        leveledWinCoinsCollectedText.text = leveledCoinsCollected.ToString();
        leveledWinMoneyEarntText.text = "$" + Mathf.RoundToInt(leveledMoneyEarnt).ToString();

        colourMenuCurrecyText.text = "CURRENCY: $" + Mathf.RoundToInt(currency).ToString();
        if (level >= maxLevel) {
            leveledWinNextGameButton.gameObject.SetActive(false);
        } else {
            leveledWinNextGameButton.gameObject.SetActive(true);
        }
        settingsCurrentNameText.text = playerName;
        settingsVersionText.text = systemPlatformType + version;
    }

    void RoomControl() {
        if (mode == "easterEgg") {
            easterEggRoomRef.SetActive(true);
        } else {
            easterEggRoomRef.SetActive(false);
        }
    }

    void PlatformSpeedControl() {
        if (platformSpeed > maxPlatformSpeed) {
            platformSpeed = maxPlatformSpeed;
        }
    }

    void AudioControl() {
        if ((mode == "newGame") || (mode == "levelPicker") || (mode == "leveledGameOver") || (mode == "infiniteGameOver") || (mode == "leveledWin") || (mode == "playerColourMenu") || (mode == "leaderboard") || (mode == "settings")) {
            audioListenerRef.transform.position = menuCamera.transform.position;
        } else if (mode == "mainMenu") {
            audioListenerRef.transform.position = mainMenuRoomCamera.transform.position;
        } else if ((mode == "infinite") || (mode == "leveled") || (mode == "infinitePaused") || (mode == "leveledPaused")) {
            audioListenerRef.transform.position = gameCamera.transform.position;
        } else if (mode == "easterEgg") {
            audioListenerRef.transform.position = easterEggCamera.transform.position;
        }
    }

    public void StartGame(string type) {
        if (newGame == true) {
            playerScriptRef.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (type == "infinite" && mode != "infinite") {
                mode = "infinite";
                newGame = false;
                infiniteCoinsCollected = 0;
                infiniteMoneyEarnt = 0;
                InfinitePlatformStartSpawn();
            } else if (type == "leveled" && mode != "leveled") {
                mode = "leveled";
                newGame = false;
                LeveledPlatformStartSpawn();
                leveledCoinsCollected = 0;
                leveledMoneyEarnt = 0;
            }
        }
    }

    public void SwitchScreens(string newScreen) {
        mode = newScreen;
    }

    public void SwitchScreensWithDelay(string newScreen) {
        if (mode == "mainMenu") {
            startMenuRef.GetComponent<Animator>().SetBool("switchScreens", true);
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

    IEnumerator SwitchScreensWithDelayController(float t, string newScreen, string function) {
        yield return new WaitForSeconds(t);
        if ((newScreen != "") || (newScreen != null)) {
            mode = newScreen;
        }
        if ((function != "") || (function != null)) {
            if (function == "startInfinite") {
                StartGame("infinite");
            }
        }
    }

    public void BackToGame() {
        if (mode == "leveledPaused") {
            SwitchScreens("leveled");
        } else if (mode == "infinitePaused") {
            SwitchScreens("infinite");
        }
    }

    public void PauseToMainMenu() {
        DeleteAllPlatform();
        playerScriptRef.transform.position = startPosion;
        newGame = true;
        mode = "mainMenu";
    }

    public void NextLevel() {
        level = level + 1;
        StartGame("leveled");
    }

    public void LevelPicker(int num) {
        if((num < 0) && (level != 1)) {
            level = level + num;
        }else if ((num> 0) && (level != maxLevel)) {
            level = level + num;
        }
    }

     void InfinitePlatformStartSpawn() {
        infinitePlatformNumber = 0;
        newGame = false;
        GameObject instantiatedPlatform;
        for (int i = 0; i < infinitePlatformStartAmount; i++) {
            instantiatedPlatform = Instantiate(platforms[Mathf.RoundToInt(Random.Range(0, platforms.Length))], new Vector3(0, -4.5f * infinitePlatformNumber, 25 * infinitePlatformNumber), Quaternion.Euler(10.204f, 0, 0));
            instantiatedPlatform.tag = platformTag;
            SpawnBackgroundObjects(infinitePlatformNumber, instantiatedPlatform);
            SpawnCoins(infinitePlatformNumber, instantiatedPlatform);
            int numOfGroundDetails = Random.Range(1, groundDetailSpawnRate);
            for (int j = 0; j < numOfGroundDetails; j++) {
                SpawnGroundDetails(infinitePlatformNumber, instantiatedPlatform);
            }
            infinitePlatformNumber++;
        }
    }

    void LeveledPlatformStartSpawn() {
        newGame = false;
        leveledPlatformNumber = 0;
        GameObject instantiatedPlatform;
        for (int i = 0; i < level; i++) {
            instantiatedPlatform = Instantiate(platforms[Mathf.RoundToInt(Random.Range(0, platforms.Length))], new Vector3(0, -4.5f * leveledPlatformNumber, 25 * leveledPlatformNumber), Quaternion.Euler(10.204f, 0, 0));
            instantiatedPlatform.tag = platformTag;
            SpawnCoins(leveledPlatformNumber, instantiatedPlatform);
            int numOfGroundDetails = Random.Range(1, groundDetailSpawnRate);
            for (int j = 0; j < numOfGroundDetails; j++) {
                SpawnGroundDetails(leveledPlatformNumber, instantiatedPlatform);
            }
            leveledPlatformNumber++;
        }
        instantiatedPlatform = Instantiate(endPlatform[Mathf.RoundToInt(Random.Range(0, endPlatform.Length))], new Vector3(0, -4.5f * leveledPlatformNumber, 25 * leveledPlatformNumber), Quaternion.Euler(10.204f, 0, 0));
        instantiatedPlatform.tag = platformTag;
    }

    void SpawnBackgroundObjects(int num, GameObject newParent) {
        int rng = Mathf.RoundToInt(Random.Range(0, 3));
        if (rng == 0) {
            GameObject instantiatedBackground;
            instantiatedBackground = Instantiate(backgrounds[Mathf.RoundToInt(Random.Range(0, backgrounds.Length))], new Vector3(0, -4.5f * num, 25 * num), Quaternion.Euler(0, 0, 0));
            instantiatedBackground.tag = backgroundTag;
            instantiatedBackground.transform.parent = newParent.transform;
        }
    }

    void SpawnCoins(int num, GameObject newParent) {
        int rng = Mathf.RoundToInt(Random.Range(0, coinSpawnRate));
        float rndYZ = Random.Range(-0.5f, 0.5f);
        if (rng == 0) {
            GameObject instantiatedCoin;
            instantiatedCoin = Instantiate(coinPrefab, new Vector3(Random.Range(-3.5f, 3.5f), -4.5f * (num + rndYZ), 25 * (num + rndYZ)), Quaternion.Euler(10.204f, 0, 0));
            instantiatedCoin.tag = coinTag;
            instantiatedCoin.transform.parent = newParent.transform;
        }
    }

    void SpawnGroundDetails(int platformNumber, GameObject newParent) {
        float rndYZ = Random.Range(-0.5f, 0.5f);
            GameObject instantiatedDetail;
            instantiatedDetail = Instantiate(groundDetails[Random.Range(0, groundDetails.Length)], new Vector3(Random.Range(-3.8f, 3.8f), -4.5f * (platformNumber + rndYZ), 25 * (platformNumber + rndYZ)), Quaternion.Euler(10.204f, 0, 0));
            instantiatedDetail.transform.parent = newParent.transform;
    }

    void DeleteAllPlatform() {
        GameObject[] needToBeDeletedObjects;
        needToBeDeletedObjects = GameObject.FindGameObjectsWithTag(platformTag);
        for (int i = 0; i < needToBeDeletedObjects.Length; i++) {
            Destroy(needToBeDeletedObjects[i]);
        }
        needToBeDeletedObjects = GameObject.FindGameObjectsWithTag(backgroundTag);
        for (int j = 0; j < needToBeDeletedObjects.Length; j++) {
            Destroy(needToBeDeletedObjects[j]);
        }
    }

    void SpawnNewPlatform() {
        GameObject instantiatedPlatform;
        instantiatedPlatform = Instantiate(platforms[Mathf.RoundToInt(Random.Range(0, platforms.Length))], new Vector3(0, -4.5f * infinitePlatformNumber, 25 * infinitePlatformNumber), Quaternion.Euler(10.204f, 0, 0));
        instantiatedPlatform.tag = platformTag;
        SpawnBackgroundObjects(infinitePlatformNumber, instantiatedPlatform);
        SpawnCoins(infinitePlatformNumber, instantiatedPlatform);
        int numOfGroundDetails = Random.Range(1, groundDetailSpawnRate);
        for (int j = 0; j < numOfGroundDetails; j++) {
            SpawnGroundDetails(infinitePlatformNumber, instantiatedPlatform);
        }
        infinitePlatformNumber++;
    }

    IEnumerator DespawnPlatform(float t, GameObject objectRef) {
        yield return new WaitForSeconds(t);
        if (objectRef == null) {
            yield break;
        }
        if (Vector3.Distance(objectRef.transform.position, playerModel.transform.position) < 30) {
            StartCoroutine(DespawnPlatform(3 / platformSpeed, objectRef));
            yield break;
        }
        if(mode == "infinite") {
            if(objectRef != null) {
                Destroy(objectRef.transform.parent.parent.gameObject);
                SpawnNewPlatform();
            } else {
                yield break;
            }
            if(newGame == true) {
                yield break;
            }
        } else {
            yield break;
        }
    }

    public void ChangePlayerColour(int amount) {
        if (amount < 0 && pickingPlayerMaterial == 0) {
            pickingPlayerMaterial = playerMaterialNames.Length - 1;
        } else if (amount < 0) {
            pickingPlayerMaterial = pickingPlayerMaterial - 1;
        }
        if (amount > 0 && pickingPlayerMaterial == playerMaterialNames.Length - 1) {
            pickingPlayerMaterial = 0;
        } else if (amount > 0) {
            pickingPlayerMaterial = pickingPlayerMaterial + 1;
        }
    }

    public void PickPlayerMaterial() {
        currentPlayerMaterial = pickingPlayerMaterial;
    }

    public void PlayWithPlayerMaterial() {
        playerModel.GetComponent<Renderer>().material = playerMaterials[currentPlayerMaterial];
        mode = "mainMenu";
    }

    public void BuyPlayerMaterial() {
        for (int i = 0; i < playerMaterialNames.Length; i++) {
            if (i == pickingPlayerMaterial) {
                if (currency >= playerMaterialCost[i]) {
                    currency = currency - playerMaterialCost[i];
                    playerMaterialUnlocked[i] = true;
                }
            }
        }
    }

    public void ExitPlayerColourMenu() {
        SwitchScreens("mainMenu");
    }

    public void ChangeSetting(string setting) {
        if (setting == "particles") {
            particlesEnabled = !particlesEnabled;
        } else if (setting == "music") {
            musicEnabled = !musicEnabled;
        } else if (setting == "fullScreen") {
            fullScreenEnabled = !fullScreenEnabled;
        } else if (setting == "touchScreenMode") {
            touchScreenModeEnabled = !touchScreenModeEnabled;
        } else if (setting == "changeName") {
            if (changingName == false) {
                changingName = true;
                changeNameMenuRef.SetActive(true);
                settingsNameChangeInputField.readOnly = false;
            }
        } else if (setting == "changeNameSubmitted") {
            if (settingsNameChangeInputField.text != null && settingsNameChangeInputField.text != "") {
                if (changeNameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                    settingsNameChangeInputField.readOnly = true;
                    changeNameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                    Invoke("HideChangeNameMenu", changeNameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
                    playerName = settingsNameChangeInputField.text;
                }
            }
        } else if(setting == "changeNameCanceled") {
            if (changeNameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                settingsNameChangeInputField.readOnly = true;
                changeNameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                Invoke("HideChangeNameMenu", changeNameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
            }
        } else if (setting == "resetGame") {
            if (resettingGame == false) {
                resettingGame = true;
                resetGameMenuRef.SetActive(true);
            }
        } else if (setting == "resetGameSubmitted") {
            if (resetGameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                resetGameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                Invoke("HideResetGameMenu", resetGameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
                gameDataControllerRef.ResetGameData();
            }
        } else if (setting == "resetGameCanceled") {
            if (resetGameMenuRef.GetComponent<Animator>().GetBool("closeMenu") == false) {
                resetGameMenuRef.GetComponent<Animator>().SetBool("closeMenu", true);
                Invoke("HideResetGameMenu", resetGameMenuRef.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
            }
        }
        gameDataControllerRef.SaveGameData();
    }

    void HideChangeNameMenu() {
        changingName = false;
        settingsNameChangeInputField.text = null;
    }

    void HideResetGameMenu() {
        resettingGame = false;
    }

    public void StartNewGame() {
        newGameNameInputField.readOnly = true;
        playerName = newGameNameInputField.text;
        newGameNameInputField.text = null;
        mode = "mainMenu";
    }

    void MusicControl() {
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

    public void NextSong() {
        musicCounter++;
        if (musicCounter >= music.Length) {
            musicCounter = 0;
        }
        musicSource.clip = music[musicCounter];
        musicSource.Play();
    }

    void TouchScreenMode(string gameMode) {
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
    }

    public void CollisionEnter(Collision collision) {
        // Check for gameover
        // Hit an obstacle
        if (mode == "infinite") {
            if (collision.gameObject.tag == obstacleTag) {
                mode = "infiniteGameOver";
                if (score >= bestScore) {
                    bestScore = score;
                }
                currency = currency + (score * infiniteDistantCurrencyAmount);
                infiniteMoneyEarnt = infiniteMoneyEarnt + (score * infiniteDistantCurrencyAmount);
                playerScriptRef.transform.position = startPosion;
                newGame = true;
                DeleteAllPlatform();
                gameDataControllerRef.SaveGameData();
            }
        } else if (mode == "leveled") {
            if (collision.gameObject.tag == obstacleTag) {
                mode = "leveledGameOver";
                leveledGameOverPercentageText.text = Mathf.Round(levelPercentage).ToString() + "%";
                playerScriptRef.transform.position = startPosion;
                newGame = true;
                DeleteAllPlatform();
                gameDataControllerRef.SaveGameData();
            }
        }
        // Fall out of area
        if (mode == "infinite") {
            if (collision.gameObject.tag == outOfAreaTag) {
                mode = "infiniteGameOver";
                if (score >= bestScore) {
                    bestScore = score;
                }
                currency = currency + (score * infiniteDistantCurrencyAmount);
                infiniteMoneyEarnt = infiniteMoneyEarnt + (score * infiniteDistantCurrencyAmount);
                playerScriptRef.transform.position = startPosion;
                newGame = true;
                DeleteAllPlatform();
                gameDataControllerRef.SaveGameData();
            }
        } else if (mode == "leveled") {
            if (collision.gameObject.tag == outOfAreaTag) {
                mode = "leveledGameOver";
                leveledGameOverPercentageText.text = Mathf.Round(levelPercentage).ToString() + "%";
                playerScriptRef.transform.position = startPosion;
                newGame = true;
                DeleteAllPlatform();
                gameDataControllerRef.SaveGameData();
            }
        }

        //despawn and spawn platform
        if (mode == "infinite") {
            if (collision.gameObject.tag == groundTag) {
                StartCoroutine(DespawnPlatform(3 / platformSpeed, collision.gameObject));
            }
        }
    }

    public void TriggerEnter(Collider other) {
        // Check for win
        if (mode == "leveled") {
            if (other.gameObject.tag == endZoneTag) {
		        maxLevel = maxLevel + 1;
                mode = "leveledWin";
                currency = currency + (leveledPlatformCurrencyAmount * level);
                leveledMoneyEarnt = leveledPlatformCurrencyAmount * level;
                playerScriptRef.transform.position = startPosion;
                newGame = true;
                DeleteAllPlatform();
                gameDataControllerRef.SaveGameData();
            }
        }

        // Check for coin pick up
        if (mode == "infinite") {
            if (other.gameObject.tag == coinTag) {
                currency = currency + coinCurrencyAmount;
                infiniteCoinsCollected++;
                infiniteMoneyEarnt = infiniteMoneyEarnt + coinCurrencyAmount;
                Destroy(other.gameObject);
            }
        } else if (mode == "leveled") {
            if (other.gameObject.tag == coinTag) {
                currency = currency + coinCurrencyAmount;
                leveledCoinsCollected++;
                leveledMoneyEarnt = leveledMoneyEarnt + coinCurrencyAmount;
                Destroy(other.gameObject);
            }
        }
    }
}