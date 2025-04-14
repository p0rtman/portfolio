using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    private void Update()
    {
        // Use the ballSpeed from the GameMngr's GameSettings
        // This assumes you have a ballSpeed variable in GameSettings that's used for the emoji fall speed.
        // If not, you may need to add it or use a different appropriate variable.
        EmojiFallMovement.globalFallSpeed = GameMngr.Instance.gameSettings.ballSpeed;
    }
}
