using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class GameManager : MonoBehaviour {

    [SerializeField] int money = 1000; 
    [SerializeField] int bet; 
    [SerializeField] Door moneyDoor; 
    [SerializeField] int doorsOpened = 0;
    [Header("UI objects")]
    [SerializeField] List<Door> doors; 
    [SerializeField] GameObject start;
    [SerializeField] TMP_Text instructionsText; 
    [SerializeField] TMP_Text scoreText; 
    [SerializeField] TMP_Text betText; 

    // Start is called before the first frame update
    void Start() {
        NewGame(); 
        instructionsText.gameObject.SetActive(false);
    }

    public void StartGame() {
        // ask for a bet
        bet = money / 2; 
        betText.text = bet.ToString(); 
        if (bet > 0 && bet <= money) {
            // randomly pick a door
            moneyDoor = RandomDoor(doors); 
            // ask user to pick a door 
            EnableDoors(); 
        }
        else {
            Debug.Log("Bet must be greater than 0 and no more than " + money + ".");
        }
    }

    void EnableDoors() {
        start.SetActive(false); 
        instructionsText.text = "Pick a Door";
        instructionsText.gameObject.SetActive(true); 
        foreach (Door b in doors) {
            b.EnableButton(true); 
        }
    }

    Door RandomDoor(List<Door> doors) {
        int randNum = Random.Range(0, doors.Count); 
        return doors[randNum]; 
    }

    public void PickDoor(int num) {
        // doorButtons[num]
        if (doors[num] == moneyDoor) { 
            WinGame(); 
        }
        else {
            // open a door that isn't the money
            List<Door> dealDoors = new List<Door>(); 
            for (int i = 0; i < doors.Count; i++) {
                if (!(dealDoors[i].Selected() || dealDoors[i].Prize())) {
                    dealDoors.Add(doors[i]);
                }
            }
            if (dealDoors.Count > 0) {
                int other = Random.Range(0, dealDoors.Count); 
                OpenDoor(dealDoors[other]);
                doors[num].enabled = true; 

                instructionsText.text = "Stay with door number " + (num + 1).ToString() + " or change doors.";
            }

            if (doorsOpened >= doors.Count) {
                LoseGame(); 
            }
        }
    }

    void OpenDoor(Door unveil) {
        doorsOpened++; 
        bool prize = unveil.Open(); 
        if (prize) {
            // money picture
            Debug.Log("Money at door " + (unveil.identifier) + "!!!");
        }
        else {
            // goat picture
            Debug.Log("Goat at door " + (unveil.identifier) + ":(");
        }
    }

    void WinGame() {
        instructionsText.text = "You Win! +" + bet.ToString(); 
        Debug.Log("You Win! +" + bet); 
        money += bet;
        NewGame(); 
    }

    void LoseGame() {
        instructionsText.text = "You Lose :( -" + bet.ToString(); 
        Debug.Log("You Lose :( -" + bet); 
        money -= bet;
        NewGame(); 
    }

    void NewGame() {
        foreach (Door b in doors) {
            b.Reset(); 
        }
        start.SetActive(true); 
        scoreText.text = "Balance: " + money; 
        betText.text = string.Empty; 
        doorsOpened = 0; 
    }
}
