using UnityEngine;

public class GameDataController : MonoBehaviour {

    public GameData gameDataRef;

    public MainScript mainScriptRef;
    
    public void SaveGameData() {
        gameDataRef.particles = mainScriptRef.particlesEnabled;
        gameDataRef.music = mainScriptRef.musicEnabled;
        gameDataRef.fullscreen = mainScriptRef.fullScreenEnabled;
        gameDataRef.currency = mainScriptRef.currency;
        gameDataRef.bestScore = mainScriptRef.bestScore;
        gameDataRef.bestLevel = mainScriptRef.level;
        SaveSettings(gameDataRef);
    }

    public void LoadGameData() {
        gameDataRef = LoadSettings();
        mainScriptRef.particlesEnabled = gameDataRef.particles;
        mainScriptRef.musicEnabled = gameDataRef.music;
        mainScriptRef.fullScreenEnabled = gameDataRef.fullscreen;
        mainScriptRef.currency = gameDataRef.currency;
        mainScriptRef.bestScore = gameDataRef.bestScore;
        mainScriptRef.maxLevel = gameDataRef.bestLevel;
        if (gameDataRef.bestLevel == 0) {
            mainScriptRef.level = 1;
        } else {
            mainScriptRef.level = gameDataRef.bestLevel;
        }
    }

    public void ResetGameData() {
        ResetSettings();
    }

    static void SaveSettings(GameData data) {

        //Settings
        if (data.particles) {
            PlayerPrefs.SetInt("particles", 1);
        } else {
            PlayerPrefs.SetInt("particles", 0);
        }
        if (data.music) {
            PlayerPrefs.SetInt("music", 1);
        } else {
            PlayerPrefs.SetInt("music", 0);
        }
        if (data.fullscreen) {
            PlayerPrefs.SetInt("fullscreen", 1);
        } else {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        //Stats
        PlayerPrefs.SetFloat("currency", data.currency);
        PlayerPrefs.SetInt("bestScore", data.bestScore);
        PlayerPrefs.SetInt("bestLevel", data.bestLevel);

        //Saves data
        PlayerPrefs.Save();
    }

    static GameData LoadSettings() {

        //Create temp "GameData"
        GameData loadedSettings = new GameData();

        //Settings
        if (PlayerPrefs.GetInt("particles") == 1) {
            loadedSettings.particles = true;
        } else {
            loadedSettings.particles = false;
        }
        if (PlayerPrefs.GetInt("music") == 1) {
            loadedSettings.music = true;
        } else {
            loadedSettings.music = false;
        }
        if (PlayerPrefs.GetInt("fullscreen") == 1) {
            loadedSettings.fullscreen = true;
        } else {
            loadedSettings.fullscreen = false;
        }

        //Stats
        loadedSettings.currency = PlayerPrefs.GetFloat("currency");
        loadedSettings.bestScore = PlayerPrefs.GetInt("bestScore");
        loadedSettings.bestLevel = PlayerPrefs.GetInt("bestLevel");

        //Returns the temp "GameData"
        return loadedSettings;
    }

    static void ResetSettings() {

        //Settings
        PlayerPrefs.SetInt("particles", 1);
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.SetInt("fullscreen", 0);

        //Stats
        PlayerPrefs.SetFloat("currency", 0);
        PlayerPrefs.SetInt("bestScore", 0);
        PlayerPrefs.SetInt("bestLevel", 0);

        //Saves data
        PlayerPrefs.Save();
    }
}