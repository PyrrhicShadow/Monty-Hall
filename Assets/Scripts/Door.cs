using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {
    [SerializeField] bool money; 
    [Header("UI Elements")]
    [SerializeField] Sprite moneySprite; 
    [SerializeField] Sprite goatSprite; 
    [SerializeField] Image selectedImage; 

    // Start is called before the first frame update
    void Start() {
        Reset(); 
    }

    public bool Open() {
        return money; 
    }

    public void Select() {
        selectedImage.enabled = true; 
    }

    public void Reset() {
        selectedImage.enabled = false; 
    }
}
