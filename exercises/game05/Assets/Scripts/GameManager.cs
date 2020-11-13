using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Please Note:
// I had a working title screen, but it's having trouble loading now giving me "Null Reference Exception", so I'm building my game for the web without it. 

public class GameManager : MonoBehaviour
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
    public float unitATK;
    public float unitDEF;
    public float unitMAG;
    public float DragonHP;

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

        if (Timer < 0 && RunOnce) {
            cc.Move(DragonReference.transform.position - Unit.transform.position);
           // Camera.transform.position = (CameraReference.transform.position - Camera.transform.position);
            // Camera.transform.Rotate(CameraReference.transform.rotation - Camera.transform.rotation);
            Camera.transform.position = new Vector3 (10, 13, -42);

            AttackButton.gameObject.SetActive(true);
            MagicButton.gameObject.SetActive(true);
            // reveal the combat buttons
            unitATK = Mathf.RoundToInt(unitATK);
            unitDEF = Mathf.RoundToInt(unitDEF);
            unitATK = Mathf.RoundToInt(unitDEF);
            // when it's time for combat, round all the numbers. 
            RunOnce = false;
            TimerText.gameObject.SetActive(false);
            // DragonText.enable = true;
            DragonText.gameObject.SetActive(true);
            NarrateText.text = ("Fight the Dragon!");

        }

        if (Input.GetMouseButton(0)) { // & ATKMine Selected?
            unitATK += 1 * Time.deltaTime;
        }

        if (Input.GetMouseButton(0)) { // & DEFMine Selected?
            unitDEF += 1 * Time.deltaTime;
        }

        if (Input.GetMouseButton(0)) { // & MagMine Selected?
            unitMAG += 1 * Time.deltaTime;
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

    public void Continue ()
    {
        SceneManager.LoadScene("Game5Scene");

    }

    public void UseAttack ()
    {
        DragonHP -= 1 * unitATK;

        NarrateText.text = ("The player struck the dragon!");
        DragonHP = Mathf.RoundToInt(DragonHP);


        DragonText.text = ("Dragon Health: " + (DragonHP + 1));
        // IDK why but it rounds down so it loses a digit, I add one to compensate

        Invoke("DragonAttack", 3f);

        // play dragondamaged animation
    }

    public void UseMagic ()
    {

        if (unitMAG >= 10)
        {
            unitMAG -= 10;
            DragonHP -= 100;

            NarrateText.text = ("The player cast a spell, electrocuting the dragon!");
            DragonHP = Mathf.RoundToInt(DragonHP);

            DragonText.text = ("Dragon Health: " + (DragonHP + 1));
        }
        else
        {
            NarrateText.text = ("The player tried to cast a spell, but didn't have enough mana!");
        }


    Invoke("DragonAttack", 3f);

    //play dragondamaged animation 
    }

    public void DragonAttack ()
    {
        if (DragonHP <= 0)
        {
            NarrateText.text = ("Player slew the dragon! Congratulations!");
            DragonText.text = ("Dragon: Dead");
        }
        else
        {
            unitHP -= 5;

            HPText.text = ("HP: " + (unitHP));
            NarrateText.text = ("The dragon fought back, burning the player with its firey breath!");

            //play playerdamaged animation
        }
    }
}
