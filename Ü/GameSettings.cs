using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public Color positiveColor;
    public Color negativeColor;
    public float playerSpeed;
    public float ballSpeed;  
    public Sprite enemySprite;

    
}
