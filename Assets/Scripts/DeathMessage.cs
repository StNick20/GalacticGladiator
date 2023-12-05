using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathMessage : MonoBehaviour
{

    public TMP_Text deathMessage; //declare TMP_text variable
    System.Random rnd = new System.Random(); // Use System.Random

    [SerializeField]
    private List<string> deathMessages = new List<string>
    {
        "RIP: Respawn In Peace",
        "Your Character took an unexpected detour to the afterlife",
        "Congratulations you died",
        "Wow, you suck at this game",
        "HAHAHAHAHAHAHA!!!!",
        "Get Good"
    };

    // Start is called before the first frame update
    void Start()
    {
        //pick a random death message
        string message = deathMessages[rnd.Next(deathMessages.Count)];

        //assign the selected death messsage to the text component
        deathMessage.text = message;
    }
}
