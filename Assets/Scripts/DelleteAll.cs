using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelleteAll : MonoBehaviour {

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteKey("Experience");
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Score");
    }
   
}
