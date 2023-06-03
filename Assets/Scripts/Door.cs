using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {
    public string identifier; 
    [SerializeField] bool money; 
    [SerializeField] bool selected = false;
    [Header("UI Elements")]
    [SerializeField] Sprite moneySprite; 
    [SerializeField] Sprite goatSprite; 
    [SerializeField] Image selectedImage; 

    // Start is called before the first frame update
    void Start() {
        Reset(); 
    }

    public bool Open() {
        if (money) {
            gameObject.GetComponent<Image>().sprite = moneySprite; 
        }
        else {
            gameObject.GetComponent<Image>().sprite = goatSprite; 
        }
        return money; 
    }

    public void Select() {
        selected = true; 
        selectedImage.enabled = true; 
    }

    public void Reset() {
        selectedImage.enabled = false; 
    }

    public bool Prize() {
        return money; 
    }

    public bool Selected() {
        return selected; 
    }

    public void EnableButton(bool val) {
        gameObject.GetComponent<Button>().enabled = val; 
    }
}
