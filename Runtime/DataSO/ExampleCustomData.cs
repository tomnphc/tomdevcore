using UnityEngine;

namespace TomDev.DataSO
{
    [System.Serializable]
    public class PlayerData
    {
        public string playerName = "Player";
        public int level = 1;
        public float experience = 0f;
        public bool isPremium = false;
        public Vector3 lastPosition = Vector3.zero;
        
        public PlayerData()
        {
            playerName = "Player";
            level = 1;
            experience = 0f;
            isPremium = false;
            lastPosition = Vector3.zero;
        }
        
        public PlayerData(string name, int playerLevel, float exp, bool premium, Vector3 position)
        {
            playerName = name;
            level = playerLevel;
            experience = exp;
            isPremium = premium;
            lastPosition = position;
        }
    }
    
    [System.Serializable]
    public class GameSettings
    {
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        public bool fullscreen = true;
        public int qualityLevel = 2;
        public string language = "en";
        
        public GameSettings()
        {
            musicVolume = 1f;
            sfxVolume = 1f;
            fullscreen = true;
            qualityLevel = 2;
            language = "en";
        }
    }
    
    // Specific ScriptableObject classes for common custom types
    [CreateAssetMenu(fileName = "New ScriptablePlayerData", menuName = "TomDev/DataSO/PlayerData")]
    public class ScriptablePlayerData : ScriptableCustom<PlayerData>
    {
        // Additional methods specific to PlayerData
        public void AddExperience(float exp)
        {
            var data = Value;
            data.experience += exp;
            SetValue(data);
        }
        
        public void LevelUp()
        {
            var data = Value;
            data.level++;
            data.experience = 0f;
            SetValue(data);
        }
        
        public void UpdatePosition(Vector3 position)
        {
            var data = Value;
            data.lastPosition = position;
            SetValue(data);
        }
    }
    
    [CreateAssetMenu(fileName = "New ScriptableGameSettings", menuName = "TomDev/DataSO/GameSettings")]
    public class ScriptableGameSettings : ScriptableCustom<GameSettings>
    {
        // Additional methods specific to GameSettings
        public void SetMusicVolume(float volume)
        {
            var settings = Value;
            settings.musicVolume = Mathf.Clamp01(volume);
            SetValue(settings);
        }
        
        public void SetSFXVolume(float volume)
        {
            var settings = Value;
            settings.sfxVolume = Mathf.Clamp01(volume);
            SetValue(settings);
        }
        
        public void ToggleFullscreen()
        {
            var settings = Value;
            settings.fullscreen = !settings.fullscreen;
            SetValue(settings);
        }
        
        public void SetQualityLevel(int level)
        {
            var settings = Value;
            settings.qualityLevel = Mathf.Clamp(level, 0, QualitySettings.names.Length - 1);
            SetValue(settings);
        }
    }
} 