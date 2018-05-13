using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelleteAll : MonoBehaviour {
    // deleta todos os dados 
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteKey("Experience");
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Damage");
        PlayerPrefs.DeleteKey("MaxExperience");
        PlayerPrefs.DeleteKey("MaxHealth");
        PlayerPrefs.DeleteKey("QntPotionHP");
        PlayerPrefs.DeleteKey("QntPotionMana");
        PlayerPrefs.DeleteKey("levelReacher");
    }
   
}
