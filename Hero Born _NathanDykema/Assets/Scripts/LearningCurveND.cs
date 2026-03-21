using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LearningCurveND : MonoBehaviour
{
    private Transform camTransform;
    public GameObject directionLight;
    private Transform lightTransform;
    // Start is called before the first frame update
    void Start()
 
        
    {
        
       // directionLight = GameObject.Find("Directional Light");
        lightTransform = directionLight.GetComponent<Transform>();
        Debug.Log(lightTransform.localPosition);
        camTransform = this.GetComponent<Transform>();
        Debug.Log(camTransform.localPosition);
        Character hero = new Character();
        Character hero2 = hero;
        hero2.name = "Sir Nathan the Mighty";
        hero.PrintStatsInfo();
        hero2.PrintStatsInfo();
        Character heroine = new Character("Agatha");
        heroine.PrintStatsInfo();
        Weapon huntingbow = new Weapon("Hunting Bow", 105);
        Weapon warBow = huntingbow;
        warBow.name = "War Bow";
        warBow.damage = 155;
        huntingbow.PrintWeaponStats();
        warBow.PrintWeaponStats();

        Paladin knight = new Paladin ("Sir Arthur",huntingbow );
        knight.PrintStatsInfo ();
        


        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
