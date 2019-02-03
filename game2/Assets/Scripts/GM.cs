using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GM : MonoBehaviour {

    public int coins, health, crystals, keys, totalKeys, maxHealth;
    public float shieldDurability, counter;
    public Text healthText,coinsText,crystalText,keyText;
    public Image healthbar,energybar;
    public GameObject gameOver,borderLeft,borderRight,character;
    public float posR, posL, posChar;


    void Awake()
    {
        health = GameObject.Find("damageControll").GetComponent<damageController>().health;
        totalKeys = GameObject.FindGameObjectsWithTag("keys").Length;
        coinsText = GameObject.Find("coinsUI").GetComponent<Text>();
        crystalText = GameObject.Find("crystal").GetComponent<Text>();
        keyText = GameObject.Find("keys").GetComponent<Text>();
        healthbar = GameObject.Find("fullHealth").GetComponent<Image>();
        energybar = GameObject.Find("fullEnergy").GetComponent<Image>();
        maxHealth = GameObject.Find("damageControll").GetComponent<damageController>().health;
        //shieldDurability = GameObject.Find("shield").GetComponent<shieldScript>().shieldDurability;
        borderLeft = GameObject.Find("borderLeft");
        borderRight = GameObject.Find("borderRight");
        character = GameObject.Find("Character1");
    }
    void Start()
    {
        posL = borderLeft.transform.position.x;
        posR = borderRight.transform.position.x;

    }
    void Update ()
    {
        
        health = GameObject.Find("damageControll").GetComponent<damageController>().health;
        //counter = GameObject.Find("shield").GetComponent<shieldScript>().counter;
        healthbar.fillAmount = ((float)health / (float)maxHealth);
        energybar.fillAmount = ((float)counter / (float)shieldDurability);
        coins = PlayerPrefs.GetInt("coins");

        coinsText.text ="COINS : " + coins.ToString();
        crystalText.text = "CRYSTALS : " + crystals.ToString();
        keyText.text = "KEYS: " + keys.ToString()+ "/" + totalKeys.ToString();

        if (health <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        posChar = character.transform.position.x;
        if (posChar <= posL)
            character.transform.position = new Vector2(posL-0.1f,character.transform.position.y);
        if(posChar>= posR)
            character.transform.position = new Vector2(posR - 0.1f, character.transform.position.y);


    }
}
