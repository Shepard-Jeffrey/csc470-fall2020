using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomScript : MonoBehaviour
{
    public static CustomScript instance;

    public InputField NameField;
    public string PlayerName;
    public Text NameHolder;
    public Text InstructionText;
    public Text ExtraText;

    int SpellsChosen = 0; // This keeps track of how many total spells the player has chosen. 

    int Spell1 = 0;
    int Spell2 = 0;
    int Spell3 = 0;

    public void Fireball()
    {
        if (Spell1 != 1 && Spell2 != 1)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 1;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 1;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 1;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }
    public void Heal()
    {
        if (Spell1 != 2 && Spell2 != 2)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 2;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 2;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 2;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void Lightning()
    {
        if (Spell1 != 3 && Spell2 != 3)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 3;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 3;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 3;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void MagicMissile()
    {
        if (Spell1 != 4 && Spell2 != 4)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 4;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 4;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 4;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void Drain()
    {
        if (Spell1 != 5 && Spell2 != 5)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 5;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 5;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 5;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }
    public void LifeBomb()
    {
        if (Spell1 != 6 && Spell2 != 6)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 6;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 6;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 6;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void Reinforce()
    {
        if (Spell1 != 7 && Spell2 != 7)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 7;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 7;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 7;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void Bolster()
    {
        if (Spell1 != 8 && Spell2 != 8)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 8;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 8;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 8;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void Explosion()
    {
        if (Spell1 != 9 && Spell2 != 9)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 9;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 9;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 9;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    public void Splice()
    {
        if (Spell1 != 10 && Spell2 != 10)
        {
            if (SpellsChosen == 0)
            {
                Spell1 = 10;
            }
            if (SpellsChosen == 1)
            {
                Spell2 = 10;
            }
            if (SpellsChosen == 2)
            {
                Spell3 = 10;
            }
            if (SpellsChosen == 3)
            {
                InstructionText.text = "You can't select more than 3 spells!";
            }
            SpellsChosen++;
        }
        else
        {
            ExtraText.text = "You can't select the same spell twice!";
        }
    }

    private void Awake()
    {
     /*   // The Singleton pattern.
        if (instance != null && instance != this)
        {
            // Enforce that there is only one GameManager.
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerName = NameField.text;
        NameHolder.text = NameField.text;

        if (SpellsChosen == 0)
        {
            InstructionText.text = "Select Your First Spell";
        }
        if (SpellsChosen == 1)
        {
            InstructionText.text = "Select Your Second Spell";
        }
        if (SpellsChosen == 2)
        {
            InstructionText.text = "Select Your Third Spell";
        }
        if (SpellsChosen == 3)
        {
            InstructionText.text = "Great! You are now ready to start your adventure!";
            ExtraText.text = "click the button to start";
        }
    }

    public void Continue()
    {
        if (Spell1 != 0 && Spell2 != 0 && Spell3 != 0)
        {
            SceneManager.LoadScene("FightScreen");
        }
        else
        {
            ExtraText.text = "Not yet! You must select your spells!";
        }
    }
}
