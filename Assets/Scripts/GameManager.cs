using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class GameManager : MonoBehaviour {

    [SerializeField] int money = 1000; 
    [SerializeField] int bet; 
    [SerializeField] int moneyDoor; 
    [Header("UI objects")]
    [SerializeField] List<Button> doorButtons; 
    [SerializeField] GameObject start;
    [SerializeField] GameObject doorPick; 

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartGame() {
        // ask for a bet
        bet = money / 2; 
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
        doorPick.SetActive(true); 
    }

    int RandomDoor() {
        return Random.Range(0, doorButtons.Count); 
    }

    public void PickDoor(int num) {
        doorButtons[num].enabled = false; 
        OpenDoor(num); 
        if (num == moneyDoor) {
            Debug.Log("You Win! +" + bet); 
            money += bet; 
        }
        else {
            OpenDoor(RandomDoor()); 
        }
    }

    void OpenDoor(int num) {
        GameObject unveil = doorButtons[num].gameObject; 
        if (num == moneyDoor) {
            // money picture
            Debug.Log("Money!!!");
        }
        else {
            // goat picture
            Debug.Log("Goat :(");
        }
        doorButtons.RemoveAt(num); 
    }
}
