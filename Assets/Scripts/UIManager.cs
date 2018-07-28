using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour {

    private Transform[] textTransforms;
    private Text[] spellNames;

    private HealthManager playerHealth;

    private Transform displayHPTransform;
    private Text displayHP;

    private Transform healthBarTransform;
    private Slider healthBar;

    private int playerCurrentHP;
    private int playerMaxHP;

    private ManaManager playerMana;

    private Transform displayManaTransform;
    private Text displayMana;

    private Transform manaBarTransform;
    private Slider manaBar;

    private float playerCurrentMana;
    private int playerMaxMana;

    // Use this for initialization
    void Start ()
    {
        playerHealth = GetComponentInParent<HealthManager>();
        playerMana = GetComponentInParent<ManaManager>();

        textTransforms = new Transform[3];
        spellNames = new Text[3];

        //Setting up the HP Bar and HP Text
        playerMaxHP = playerHealth.maxHP;
        playerCurrentHP = playerHealth.currentHP;

        healthBarTransform = this.gameObject.transform.GetChild(3);
        healthBar = healthBarTransform.GetComponent<Slider>();

        healthBar.maxValue = playerMaxHP;

        displayHPTransform = this.gameObject.transform.GetChild(4);
        displayHP = displayHPTransform.GetComponent<Text>();

        //Setting up the Mana Bar and Mana Text
        playerMaxMana = playerMana.maxMana;
        playerCurrentMana = playerMana.currentMana;

        manaBarTransform = this.gameObject.transform.GetChild(5);
        manaBar = manaBarTransform.GetComponent<Slider>();

        manaBar.maxValue = playerMaxMana;

        displayManaTransform = this.gameObject.transform.GetChild(6);
        displayMana = displayManaTransform.GetComponent<Text>();

        //Setting up the Spell Slot display
        //spells = GetComponentInParent<CastSpell>();
        //if (spells == null)
        //{
        //    Debug.Log("Unable to find GameObject 'spells', reference is null");
        //}

        textTransforms[0] = this.gameObject.transform.GetChild(0);
        spellNames[0] = textTransforms[0].GetComponent<Text>();
        if (spellNames[0] == null)
        {
            Debug.Log("Unable to find GameObject 'spellNames', reference is null");
        }

        textTransforms[1] = this.gameObject.transform.GetChild(1);
        spellNames[1] = textTransforms[1].GetComponent<Text>();
        if (spellNames[1] == null)
        {
            Debug.Log("Unable to find GameObject 'spellNames', reference is null");
        }
    }
	
	// Update is called once per frame
	void Update () {
        

        //spellNames[0].text = "Spell 1: " + spells.actionNames[0] + " " + spells.elementNames[0] + " " + spells.formNames[0];
        //spellNames[1].text = "Spell 2: " + spells.actionNames[1] + " " + spells.elementNames[1] + " " + spells.formNames[1];

        playerCurrentHP = playerHealth.currentHP;
        healthBar.value = playerCurrentHP;

        displayHP.text = "HP: " + playerCurrentHP + "/" + playerMaxHP;

        playerMaxMana = playerMana.maxMana;
        playerCurrentMana = playerMana.currentMana;

        manaBar.maxValue = playerMaxMana;
        manaBar.value = playerCurrentMana;

        displayMana.text = "Mana: " + playerCurrentMana.ToString("F1") + "/" + playerMaxMana; //Displays only the first decimal place of the current mana

    }
}
