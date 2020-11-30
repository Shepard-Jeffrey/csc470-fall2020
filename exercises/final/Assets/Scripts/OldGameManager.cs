using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OldGameManager : MonoBehaviour
{
    public UnitScript selectedUnit;

    public GameObject StatPanel;
    public GameObject Unit;
    public GameObject DragonReference;
    public GameObject CameraReference;
    public GameObject Camera;

    public Text NameText;
    public Text HPText;
    public Text TimerText;
    public Text LabelText;
    public Text DragonText;
    public Text NarrateText;
    public Image ATKImage;
    public Image DEFImage;
    public Image MAGImage;

    public MeterScript ATKMeter;
    public MeterScript DEFMeter;
    public MeterScript MAGMeter;

    public CharacterController cc;

    public bool RunOnce = true;
    public Button AttackButton;
    public Button MagicButton;

    public float Timer;
    public float unitHP;
   
    public float DragonHP;

    public Animator UnitAnimator; 
    public Animator DragonAnimator;

    // Start is called before the first frame update
    void Start()
    {
        AttackButton.gameObject.SetActive(false);
        MagicButton.gameObject.SetActive(false);
       DragonText.gameObject.SetActive(false);
       // DragonText.enable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime; // Update timer by time. 
        TimerText.text = ("Time Left: " + (Timer));  // Update the time displayed

        // When the timer reaches zero, move the player and camera to the boss fight, enable the attack buttons, make sure this code only runs once. 

        if (Timer < 0 && RunOnce) { // I only want this to happen once, so i just use the bool runonce to make that happen. 
            cc.Move(DragonReference.transform.position - Unit.transform.position);
            selectedUnit.targetPosition = DragonReference.transform.position;
           // Camera.transform.position = (CameraReference.transform.position - Camera.transform.position);
         //   Camera.transform.rotation = CameraReference.transform.rotation;
            Camera.transform.position = new Vector3 (10, 13, -42);

            AttackButton.gameObject.SetActive(true);
            MagicButton.gameObject.SetActive(true);
            // reveal the combat buttons
            selectedUnit.ATK = Mathf.RoundToInt(selectedUnit.ATK);
            selectedUnit.DEF = Mathf.RoundToInt(selectedUnit.DEF);
            selectedUnit.MAG = Mathf.RoundToInt(selectedUnit.MAG);

            // when it's time for combat, round all the numbers. 

            if (selectedUnit.ATK > 1000)
            {
                selectedUnit.ATK = 1000;
            }
            if (selectedUnit.DEF > 1000)
            {
                selectedUnit.DEF = 1000;
            }
            if (selectedUnit.MAG > 1000)
            {
                selectedUnit.MAG = 1000;
            }

            // Setting cap to 1000, these were all 100, but I changed it to 1000 to make the meter go up slower. 

            TimerText.gameObject.SetActive(false);
            // DragonText.enable = true;
            DragonText.gameObject.SetActive(true);
            NarrateText.text = ("Fight the Dragon!");

            RunOnce = false;

            selectedUnit.DragonFightStarted = true;
            selectedUnit.selected = false;

        }

    
    }
    
    public void GoButtonClicked()
    {
        if (selectedUnit != null) {
            selectedUnit.StartFollowingPath();
            StatPanel.SetActive(true);

        }
    }

    // This function takes a Unit's UnitScript, makes it selected, and deselects any other units that were selected.
    // If null is passed in, it will just deselect everything.
    // This function also populates the nameText UI element with the currently selected unit's name, and also ensures
    // that the namePanel UI element is only active is a unit is selected.
    public void SelectUnit(UnitScript toSelect)
    {
        selectedUnit = toSelect;
    
        // Get an array of all GameObjects that have the tag "Unit".
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        // Loop through all units and make sure they are not selected.
        for (int i = 0; i < units.Length; i++) {
            UnitScript unitScript = units[i].GetComponent<UnitScript>();
            unitScript.selected = false;
            unitScript.UpdateVisuals();
        }
        
        
        if (toSelect != null) {
            // If there is a selected, mark it as selected, update its visuals, and update the UI elements.
            selectedUnit.selected = true;

            UpdateUI(selectedUnit);
            
            selectedUnit.UpdateVisuals();
        } else {
            // If we get in here, it means that the toSelect parameter was null, and that means that we 
            // should deactivate the namePanel.
            StatPanel.SetActive(false);
        }
    }
    
    public void UpdateUI(UnitScript unit)
    {
        ATKMeter.SetMeter(unit.ATK / 100f);
        DEFMeter.SetMeter(unit.DEF / 100f);
        MAGMeter.SetMeter(unit.MAG / 100f);
        NameText.text = unit.unitName;
       // LabelText.SetActive(true);
        StatPanel.SetActive(true);
    }

    public void UseAttack ()
    {
        DragonAnimator.SetBool("dragonDamaged", true);

        DragonHP -= 1 * (selectedUnit.ATK/10); // factors in player unit's attack for the damage

        NarrateText.text = ("The player struck the dragon!");
        DragonHP = Mathf.RoundToInt(DragonHP);


        DragonText.text = ("Dragon Health: " + (DragonHP)); 

        Invoke("DragonAttack", 3f);
        // adds delay for improved gamefeel. 
    }

    public void UseMagic ()
    {
        DragonAnimator.SetBool("dragonDamaged", true);

        if (selectedUnit.MAG >= 100)
        {
            selectedUnit.MAG -= 100;
            DragonHP -= 100;

            MAGMeter.SetMeter(selectedUnit.MAG/1000f); // sets the MAG meter again after using up Magic

            NarrateText.text = ("The player cast a spell, electrocuting the dragon!");
            DragonHP = Mathf.RoundToInt(DragonHP);

            DragonText.text = ("Dragon Health: " + (DragonHP)); 
        }
        else
        {
            NarrateText.text = ("The player tried to cast a spell, but didn't have enough mana!");
        }

        DragonAnimator.SetBool("dragonDamaged", true);


        Invoke("DragonAttack", 3f);

    }

    public void DragonAttack ()
    {
        DragonAnimator.SetBool("dragonDamaged", false);

        UnitAnimator.SetBool("damaged", true);

        if (DragonHP <= 0) // I could do this for the player as well to create a proper lose condition
        {
            NarrateText.text = ("Player slew the dragon! Congratulations!");
            DragonText.text = ("Dragon: Dead"); // gives the player a win condition
        }
        else
        {
            if (selectedUnit.DEF != 0) // can't divide by zero. 
            {
                unitHP -= (15 - (selectedUnit.DEF / 100)); // wanted a range between 15 and 5, uses playerunit's attack
            }
            else
            {
                unitHP -= 15;
            }
            HPText.text = ("HP: " + (unitHP));
            NarrateText.text = ("The dragon fought back, burning the player with its firey breath!");

        }
        Invoke("NotDamaged", 1f); // delaying the playerdamaged animation turning off. 
    }

    public void NotDamaged() // created so that I could delay the playerdamaged animation turning off. 
    {
        UnitAnimator.SetBool("damaged", false);
    }
}