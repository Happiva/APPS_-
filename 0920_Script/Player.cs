using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private Inventory inventory;

    public bool isRun;
    public bool isMove;

	[HideInInspector]
	public float startHealth = 100;
	private float health;

	public float startEnergy = 100;
	private float energy;

	[Header("Unity Stuff")]
	public Image healthBar;
	public Image energyBar;

	public Text HPText;
	public Text EnergyText;

    public GameObject WarningText;

	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        health = startHealth;
        energy = startEnergy;

        HPText.text = health + "/" + startHealth;
		EnergyText.text = energy + "/" + startEnergy;
	}

	public void Damage(float damageAmount){
		health -= damageAmount;
        HPText.text = health + "/" + startHealth;
        healthBar.fillAmount = health / startHealth;
		if (health < 0)
			health = 0;
	}

	public void Heal(int healAmount){
		health += healAmount;
        HPText.text = health + "/" + startHealth;
        healthBar.fillAmount = health / startHealth;
		if (health > startHealth)
			health = startHealth;
	}
    /*
	public void LoseEnergy(float energyAmount){
		energy -= energyAmount;

		energyBar.fillAmount = energy / startEnergy;
		
		if (energy < 0)
			energy = 0;
	}
    */

    public void LoseEnergyRunning()
    {
        energy -= 0.1f;
        EnergyText.text = energy + "/" + startEnergy;
        energyBar.fillAmount = energy / startEnergy;

        if (energy < 0)
            energy = 0;
    }

    public void GetEnergy(float energyAmount){
		energy += energyAmount;
        EnergyText.text = energy + "/" + startEnergy;
        energyBar.fillAmount = energy / startEnergy;
		
		if (energy > startEnergy)
			energy = startEnergy;
	}
    
    public void EatFood(int itemID) {
        if (itemID == 0)
        {
            if (inventory.amount[0] > 0)
            {
                GetEnergy(5);
                inventory.amount[0]--;
                inventory.amountText[0].text = inventory.amount[0] + "";
            }
            else Debug.Log("No Potato");
        }
        if (itemID == 1)
        {
            if (inventory.amount[1] > 0)
            {
                Heal(5);
                inventory.amount[1]--;
                inventory.amountText[1].text = inventory.amount[1] + "";
            }
            else Debug.Log("No Crab");
        }
    }

    public void WarningBlink() {
        WarningText.SetActive(false);
    }

	void Update () {
        //HPText.text = health + "/" + startHealth;
        //EnergyText.text = energy + "/" + startEnergy;
        if (energy <= 0)
        {
            WarningText.SetActive(true);
            Invoke("WarningBlink", 3f);
        }
        else WarningText.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (energy < startEnergy)
                EatFood(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (health < startHealth)
                EatFood(1);
            //else 
        }

        if (energy <= 0 && isRun == true)
        {
            Damage(0.2f);
        }

        if (isRun == true && isMove == true)
        {
            Debug.Log("Running");
            LoseEnergyRunning();
        }
        else {
            Debug.Log("Not Running");
        }
        
    }
}