﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	// This was made a lot easier by setting and manipulating player & enemy stats entirely in game manager, rather than having to worry about enemy scripts
	float PlayerHP = 100;
	float PlayerMP = 100;
	float PlayerATK = 10;
	float PlayerDEF = 10;
	float EnemyHP = 1000;
	// default values
	float FadeRate = 0.001f; // for some reason this needed to be set extremely low for one, and high for the other, ended up being wonky in general
	bool NeedsToFade = false; // this is to control whether fade or unfade runs. 

	int Enemy = 1; // This is to keep track of which enemy the player faces for coding purposes. 
	int Button1 = 1;
	int Button2 = 2;
	int Button3 = 3;
	// The above are so that each button can keep track of it's value. 

	public string PlayerName = "Jeffrey"; // Default Value, should be set in CustomScreen by Player
	string EnemyName = "Slime"; // Default Value, will be changed after each fight

	public Text NarrateText;
	public Text ATKText;
	public Text DEFText;
	public Text NameText;
	public Text HPNum;
	public Text MPNum;
	public Text EnemyNameText;
	public Text EnemyHPNum;

	public InputField NameField;

	public MeterScript HPMeter;
	public MeterScript MPMeter;
	public MeterScript EnemyHPMeter;

	public bool Lost = false;

	public Image FadeBlack;

	public ParticleSystem FireBallEffect;
	public ParticleSystem SlimeEffect;
	public ParticleSystem LightningEffect;
	public ParticleSystem HealEffect;
	public ParticleSystem BasicAttackEffect;



	private void Awake()
	{
		// The Singleton pattern.
		if (instance != null && instance != this)
		{
			// Enforce that there is only one GameManager.
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			//DontDestroyOnLoad(this.gameObject);
		}

	}

	public void ReturnToTitle()
	{
		SceneManager.LoadScene("TitleScreen");

	}

	// Start is called before the first frame update
	void Start()
	{

		NameText.text = (PlayerName);
		EnemyNameText.text = (EnemyName);

		UpdateUI();
	/*	StartCoroutine();*/
	}

	// Update is called once per frame
	void Update()
	{
		if (EnemyHP < 1) // general check for if the boss has been defeated. 
		{
			BattleWon();
		}
		if (PlayerHP < 1) // general check for if the player has lost
		{
			BattleLost();
		}

		//PlayerName = NameField.text;

	}

	// I decided to be liberal with functions in this project, feels like they keep it neat. 

	public void Button1Clicked()
	{
		if (Lost == true) // This just lets the player return to the title screen. 
		{
			ReturnToTitle();
			Lost = false;
		}
		switch (Button1)
		{
			case 1:
				Fireball();
				break;
			case 2:
				Heal();
				break;
			case 3:
				LightningBolt();
				break;
			case 4:
				MagicMissile();
				break;
			case 5:
				LifeDrain();
				break;
			case 6:
				LifeForceBomb();
				break;
			case 7:
				Reinforce();
				break;
			case 8:
				Bolster();
				break;
			case 9:
				LastResort();
				break;
			case 10:
				Splice();
				break;
		}
		UpdateUI();
	}

	public void Button2Clicked()
	{
		if (Lost == true)
		{
			ReturnToTitle();
			Lost = false;
		}
		switch (Button2)
		{
			case 1:
				Fireball();
				break;
			case 2:
				Heal();
				break;
			case 3:
				LightningBolt();
				break;
			case 4:
				MagicMissile();
				break;
			case 5:
				LifeDrain();
				break;
			case 6:
				LifeForceBomb();
				break;
			case 7:
				Reinforce();
				break;
			case 8:
				Bolster();
				break;
			case 9:
				LastResort();
				break;
			case 10:
				Splice();
				break;
		}
		UpdateUI();
	}

	public void Button3Clicked()
	{
		if (Lost == true)
		{
			ReturnToTitle();
			Lost = false;
		}
		switch (Button3)
		{
			case 1:
				Fireball();
				break;
			case 2:
				Heal();
				break;
			case 3:
				LightningBolt();
				break;
			case 4:
				MagicMissile();
				break;
			case 5:
				LifeDrain();
				break;
			case 6:
				LifeForceBomb();
				break;
			case 7:
				Reinforce();
				break;
			case 8:
				Bolster();
				break;
			case 9:
				LastResort();
				break;
			case 10:
				Splice();
				break;
		}
		UpdateUI();
	}


	// These allow me to allow each button to call any spell depending on the value the player will assign at start, and to not have to repeat the UpdateUI and ChooseEnemyAction functions calls
	// In every player attack option. 

	public void UpdateUI()
	{
		HPMeter.SetMeter(PlayerHP / 100f);
		MPMeter.SetMeter(PlayerMP / 100f);
		EnemyHPMeter.SetMeter(EnemyHP / 1000f);
		ATKText.text = ("ATK: " + (PlayerATK));
		DEFText.text = ("DEF: " + (PlayerDEF));
		NameText.text = PlayerName;
		
		HPNum.text = ("" + PlayerHP); // it didn't like me just using a float
		MPNum.text = ("" + PlayerMP);
		if (EnemyHP > 0)
		{
			EnemyHPNum.text = ("" + EnemyHP);
		}
		else
        {
			EnemyHPNum.text = ("Dead");
        }
		}

	public void BasicAttack()
	{
		if (Lost == true) // lets player restart the fight if they lose. 
		{
			RestoreDefaults();
			Lost = false;
		}
		EnemyHP -= 5 * (PlayerATK); // factors in player unit's attack for the damage
		NarrateText.text = ("The player hit the " + EnemyName + " with basic magic, causing it to recoil.");
		BasicAttackEffect.Play();
		UpdateUI();
		ChooseEnemyAction();
	}

	public void Fireball()
	{
		if (PlayerMP < 15)
		{
			OutOfMana();
		}
		else
		{
			NarrateText.text = ("The player launches a Fireball at the " + EnemyName + ", scorching its flesh.");
			FireBallEffect.Play();
			PlayerMP -= 15;
			EnemyHP -= 100;
			ChooseEnemyAction();
		}
	}

	public void Heal()
	{
		if (PlayerMP < 15)
		{
			OutOfMana();
		}
		else
		{
			PlayerMP -= 15;
			PlayerHP += 10;
			NarrateText.text = ("The player heals themselves, erasing grievous wounds.");
			HealEffect.Play();
			ChooseEnemyAction();
		}
	}

	public void LightningBolt()
	{
		if (PlayerMP < 40)
		{
			OutOfMana();
		}
		else
		{
			NarrateText.text = ("The player sends bolts of lightning at the " + EnemyName + ", electrocuting it.");
			LightningEffect.Play();
			PlayerMP -= 40;
			EnemyHP -= 200;
			ChooseEnemyAction();
		}
	}

	public void MagicMissile()
	{
		if (PlayerMP < 5)
		{
			OutOfMana();
		}
		else
		{
			PlayerMP -= 5;
			EnemyHP -= 50;
			NarrateText.text = ("The player sends a missile of energy at the " + EnemyName + ", forcing it back.");
			ChooseEnemyAction();
		}
	}

	public void LifeDrain()
	{
		if (PlayerMP < 10)
		{
			OutOfMana();
		}
		else
		{
			PlayerMP -= 10;
			EnemyHP -= 50;
			PlayerHP += 50;
			NarrateText.text = ("The player steals life energy from their foe, reinforcing their own life force.");
			ChooseEnemyAction();
		}
	}

	public void LifeForceBomb()
	{
		PlayerHP -= 10;
		EnemyHP -= 300;
		NarrateText.text = ("The player uses their own life force in place of mana to attack their opponent");
		ChooseEnemyAction();
	}

	public void Reinforce()
	{
		if (PlayerMP < 10)
		{
			OutOfMana();
		}
		else
		{
			PlayerDEF += 10;
			PlayerMP -= 10;
			NarrateText.text = ("The player reinforces their body with magic, enabling them to withstand more hits");
			ChooseEnemyAction();
		}
	}

	public void Bolster()
	{
		if (PlayerMP < 10)
		{
			OutOfMana();
		}
		else
		{
			PlayerATK += 10;
			PlayerMP -= 10;
			NarrateText.text = ("The player strengthens their basic magic attacks, enabling them to do more damage without using mana");
			ChooseEnemyAction();
		}
	}

	public void LastResort()
	{
		if (PlayerMP < 1)
		{
			OutOfMana();
		}
		else
		{
			EnemyHP -= ((PlayerMP * 5) + (PlayerHP * 5));
			PlayerMP = 0;
			PlayerHP = 1;
			NarrateText.text = ("The player uses every last ounce of their magical and physical energy in an all-out attack against their enemy");
			ChooseEnemyAction();
		}
	}

	public void Splice()
	{
		EnemyHP = (EnemyHP / 2);
		PlayerMP = (PlayerMP / 2);
		NarrateText.text = ("The player cuts the " + EnemyName + "'s HP in half, using half of their own mana");
		ChooseEnemyAction();
	}

	public void SlimeAttack()
	{
		NarrateText.text = ("The Slime fires aciding goop at the player, burning them.");
		SlimeEffect.Play();
		PlayerHP -= 5 / (PlayerDEF / 10);
		UpdateUI();
	}

	public void BattleWon()
	{
		UpdateUI();
		if (EnemyHPNum.text == "Dead") {
			NarrateText.text = ("The player defeats their enemy after a hard-fought battle! The player rests for awhile, regaining their strength...");
		}

		if (FadeBlack.color.a <= 0)
        {
			NeedsToFade = true;
		}
		StartCoroutine(Fade());
		if (FadeBlack.color.a >= 1.3) {
			RestoreDefaults();
			NeedsToFade = false;
			if (Enemy == 1)
			{
				NarrateText.text = ("A New Enemy Appears!");
			}
		}
		StartCoroutine(UnFade());
		
	}

	public void BattleLost()
	{
		HPNum.text = "0";
		UpdateUI();
		NarrateText.text = ("The player was unable to overcome their enemy. Click the Attack Button to restart the fight. Click any of the Spell Buttons to return to the title screen");
		Lost = true;
	}

	public void RestoreDefaults() // making this to refill the player and enemy for fight. 
	{
		PlayerHP = 100;
		PlayerMP = 100;
		PlayerATK = 50;
		PlayerDEF = 1;
		EnemyHP = 1000;
		UpdateUI();
	}

	public void ChooseEnemyAction() // this lets each player action call it, which in turns calls the right enemy action based on the situation
	{
		if (Enemy == 1)
		{
			Invoke("SlimeAttack", 3f);
		}
	}

	public void OutOfMana() {
		NarrateText.text = ("You don't have enough mana to use this spell. Choose a different action.");
	}

/*	public void FadeToBlack() {
		while (FadeBlack.color.a <= 255)
		{
			FadeBlack.color = new Color(FadeBlack.color.r, FadeBlack.color.g, FadeBlack.color.b, FadeBlack.color.a + FadeRate); // FadeRate * Time.deltaTime
		}
    }*/
/*
	public void UnFade()
    {
		while (FadeBlack.color.a >= 0)
        {
			FadeBlack.color = new Color(FadeBlack.color.r, FadeBlack.color.g, FadeBlack.color.b, FadeBlack.color.a - FadeRate * Time.deltaTime);
				Invoke("FadeToBlack", 1f);
		}
	}*/

    IEnumerator Fade()
    {
        while (FadeBlack.color.a <= 255 && NeedsToFade == true)
        {
            FadeBlack.color = new Color(FadeBlack.color.r, FadeBlack.color.g, FadeBlack.color.b, FadeBlack.color.a + FadeRate * Time.deltaTime);
			//Debug.Log(FadeBlack.color.a); test to see what was going wrong
			yield return null;
        }
    }

	IEnumerator UnFade()
	{
		while (FadeBlack.color.a >= 0 && NeedsToFade == false)
		{
			FadeBlack.color = new Color(FadeBlack.color.r, FadeBlack.color.g, FadeBlack.color.b, FadeBlack.color.a - FadeRate * 500 * Time.deltaTime);
			//Debug.Log(FadeBlack.color.a); test to see what was going wrong
			yield return null;
		}
		
	}
}