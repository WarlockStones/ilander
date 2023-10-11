using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "GameConfiguration", order = 1)]
class GameConfiguration : ScriptableObject {

    [Header("Scenes")]
    public List<string> gameLevelNames; // All the levels that is part of this Game Configuration

    [Header("Other Settings")]
    public bool isGamePaused; // Will we use this? - Rasmus R
}
