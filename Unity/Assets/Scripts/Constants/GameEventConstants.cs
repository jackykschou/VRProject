namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameEvent
    {
#if DEBUG
        ExampleEvent,                           // void()
#endif
        OnSectionActivated,                     // void(int)
        OnSectionDeactivated,                   // void(int)
        OnSectionObjectivesCompleted,           // void(int)
        OnSectionEnemySpawnPointActivated,      // void(GameObject, int)
        OnSectionEnemySpawnPointDeactivated,    // void(GameObject, int)
        OnSectionEnemySpawned,                  // void(int)
        OnSectionEnemyDespawned,                // void(GameObject, int)

        OnPlayerSkillCoolDownUpdate,   // void(int, float)
        PlayerHealthUpdate,            // void(float)
        OnLevelStartLoading,           // void()
        OnLevelFinishedLoading,        // void()
        OnLevelStarted,                // void()
        OnLevelEnded,                  // void()

        OnGameEventSent,                 // void(GameEvent)
        
        DisableAbility,                // void(int)
        EnableAbility,                 // void(int)
        EnableHighlightSkill,          // void(int,float)
        DisableHighlightSkill,         // void(int)

        OnPlayerDashButtonPressed,    // void()
        OnPlayerDeath,

        ShakeCamera,                     // void(float, float)
        SetCameraWidth,                  // void(float)
        DisablePlayerCharacter,          // void()
        EnablePlayerCharacter,           // void()
        OnPlayerReset,                   // void()
        OnLoadingScreenFinished,          // void()
        SurvivalSectionStarted,          // void()
        SurvivalSectionEnded,             // void()
        SurvivalDifficultyIncreased,      //void(int)
        OnPlayerSkillPressed,          // void (int)
        WaveCountIncreased              // void (int)
     };
}
