using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class GameManager : MonoBehaviour {

    [SerializeField] int money = 1000; 
    [SerializeField] int bet; 
    [SerializeField] int moneyDoor; 
    [SerializeField] int doorsOpened = 0;
    [Header("UI objects")]
    [SerializeField] List<Button> doorButtons; 
    [SerializeField] GameObject start;
    [SerializeField] TMP_Text instructionsText; 
    [SerializeField] TMP_Text scoreText; 
    [SerializeField] TMP_Text betText; 

    // Start is called before the first frame update
    void Start() {
        NewGame(); 
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartGame() {
        // ask for a bet
        bet = money / 2; 
        betText.text = bet.ToString(); 
        if (bet > 0 && bet <= money) {
            // randomly pick a door
            moneyDoor = RandomDoor(); 
            // ask user to pick a door 
            EnableDoor(); 
        }
        else {
            Debug.Log("Bet must be greater than 0 and no more than " + money + ".");
        }
    }

    void EnableDoor() {
        start.SetActive(false); 
        instructionsText.gameObject.SetActive(true); 
        foreach (Button b in doorButtons) {
            b.enabled = true; 
        }
    }

    int RandomDoor() {
        return Random.Range(0, doorButtons.Count); 
    }

    public void PickDoor(int num) {
        OpenDoor(num); 
        if (num == moneyDoor) { 
            WinGame(); 
        }
        else {
            // open a door that isn't the money
            List<Button> dealDoors = new List<Button>(); 
            for (int i = 0; i < doorButtons.Count; i++) {
                if (!(i == num || i == moneyDoor)) {
                    dealDoors.Add(doorButtons[i]);
                }
            }
            if (dealDoors.Count > 0) {
                OpenDoor(Random.Range(0, dealDoors.Count));
                doorButtons[num].enabled = true; 

                instructionsText.text = "Stay with door number " + (num + 1).ToString() + " or change doors.";
            }

            if (doorsOpened >= doorButtons.Count) {
                LoseGame(); 
            }
        }
    }

    void OpenDoor(int num) {
        doorsOpened++; 
        GameObject unveil = doorButtons[num].gameObject; 
        if (num == moneyDoor) {
            // money picture
            Debug.Log("Money at door " + (num + 1) + "!!!");
        }
        else {
            // goat picture
            Debug.Log("Goat at door " + (num + 1) + ":(");
        }
        doorButtons[num].enabled = false; 
        // doorButtons.RemoveAt(num); 
    }

    void WinGame() {
        instructionsText.text = "You Win! +" + bet.ToString(); 
        Debug.Log("You Win! +" + bet); 
        money += bet;
        NewGame(); 
    }

    void LoseGame() {
        instructionsText.text = "You Lose :( +" + bet.ToString(); 
        Debug.Log("You Lose :( +" + bet); 
        money -= bet;
        NewGame(); 
    }

    void NewGame() {
        foreach (Button b in doorButtons) {
            b.enabled = false; 
        }
        start.SetActive(true); 
        instructionsText.gameObject.SetActive(false);
        instructionsText.text = "Pick a Door";
        scoreText.text = "Balance: " + money; 
        betText.text = string.Empty; 
    }
}
