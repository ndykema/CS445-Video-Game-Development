using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;


using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;

    public string State
    {
        get {  return _state; }
        set { _state = value; }
    }

    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public int maxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;

    public Stack<string> lootStack = new Stack<string>();

    public Button LossButton;
    public Button WinButton;

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    private int _itemsCollected = 0;

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    private void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
        Initialize();

        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }
    public int Items
    {
        get { return _itemsCollected; }
        set {
            _itemsCollected = value;
            ItemText.text = "Items Collected: " + Items;
            if (_itemsCollected >= maxItems)
            {
               UpdateScene("You've found all the items!");
                //WinButton.gameObject.SetActive(true);
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
            
        }
    }
    public void Initialize()
    {
        _state = "Manager initialized...";
        _state.FancyDebug();
        Debug.Log(_state);

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");

        debug(_state);
        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");
        PlayerBehavior_ND playerBehavoir = player.GetComponent<PlayerBehavior_ND>();
        playerBehavoir.playerJump += HandlePlayerJump;
        
    }

    private int _playerHP = 1;

    public int HP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            _playerHP = value;
            HealthText.text = "Health: " + HP;
            
            if(_playerHP <= 0)
            {
                UpdateScene("You want another life with that?");
                //LossButton.gameObject.SetActive(true);
                showLossScreen = true;
    
                
            }
            else
            {
                ProgressText.text = "Ouch...that's got to hurt.";
            }

        }
        

    }

    /* public void RestartScene()
     {
         SceneManager.LoadScene(0);
         Time.timeScale = 1f;
     }
    */

    private void OnGUI()
    {
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }
        GUI.Box(new Rect(20, 20, 150, 25), "PlayerHealth:" + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        // GUI.Label(new Rect(Screen.width / 2-100, Screen.height - 50, 300, 50), labelText);

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted sucessfully...");
                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart Handled...");
                }

            }
        }
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }
    
    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

}

