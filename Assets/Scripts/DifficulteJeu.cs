using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficulteJeu : MonoBehaviour
{
    // Start is called before the first frame update
    public float detectionRange = 10f;
    public float accuracy = 0.33f;
    public float movementSpeed = 2f;

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("GameDifficulty", "Normal");
        if (difficulty == "Difficult")
        {
            detectionRange *= 1.3f;
            accuracy = 0.5f;
            movementSpeed *= 1.25f;
        }
    }
}
