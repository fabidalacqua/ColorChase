using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<char> colors = new List<char>();
    private List<string> items = new List<string>();
    private List<string> weapons = new List<string>();
    private List<Vector2> spawnPositions = new List<Vector2>();

    public GameObject knifePrefab;
    public GameObject buffItem1Prefab;
    public GameObject debuffItem1Prefab;

    private float respawnTime;
    public bool canSpawn;
    public int i;
    
    void Start()
    {
        i = 0;
        canSpawn = true;
        respawnTime = 4.0f;

        //Possible colors for a weapon
        colors.Add('b'); //Blue
        colors.Add('p'); //Purple
        colors.Add('r'); //Red
        colors.Add('y'); //Yellow

        //Name of weapons that can spawn
        weapons.Add("Knife");

        //Name of items that can spawn
        items.Add("BuffItem1");
        items.Add("DebuffItem1");

        //Positions that items can spawn
        spawnPositions.Add(new Vector2(-15.48f, 7.34f));
        spawnPositions.Add(new Vector2(-15.38f, -7.79f));
        spawnPositions.Add(new Vector2(-15.34f, -2.69f));
        spawnPositions.Add(new Vector2(-10.45f, 7.34f));
        spawnPositions.Add(new Vector2(-6.37f, -0.72f));
        spawnPositions.Add(new Vector2(-3.52f, 6.09f));
        spawnPositions.Add(new Vector2(-2.70f, -0.68f));
        spawnPositions.Add(new Vector2(3.54f, 3.29f));
        spawnPositions.Add(new Vector2(8.52f, 3.19f));
        spawnPositions.Add(new Vector2(12.84f, 7.34f));
        spawnPositions.Add(new Vector2(12.50f, -2.61f));
        spawnPositions.Add(new Vector2(15.67f, -7.79f));
        spawnPositions.Add(new Vector2(15.67f, -2.69f));
        
        StartCoroutine(itemCoroutine());
    }

    private void spawnWeapon(){
        GameObject weapon;
        //Get random weapon name from list 'weapons'
        //int weaponPos = Random.Range(0, weapons.Count);
        int weaponPos = 0;
        switch(weapons[weaponPos])
        {
        case "Knife":
            weapon = Instantiate(knifePrefab) as GameObject;
            weapon.GetComponent<Weapon>().Name = weapons[weaponPos];
            weapon.GetComponent<Weapon>().Quantity = 3;
            break;
        //If there is an error to get a Weapon, default weapon will be the knife
        default:
            weapon = Instantiate(knifePrefab) as GameObject;
            weapon.GetComponent<Weapon>().Name = weapons[weaponPos];
            weapon.GetComponent<Weapon>().Quantity = 3;
            Debug.Log("Weapon does not exist");
            break;
        }

        //Get random color from list 'colors'
        int nColor = Random.Range(0, colors.Count);
        weapon.GetComponent<Weapon>().Color = colors[nColor];
        
        //Change weapon's color
        switch(colors[nColor])
        {
            case 'b':
                weapon.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 'p':
                weapon.GetComponent<Renderer>().material.color = new Color (0.49f, 0.0f, 1.0f, 1.0f);
                break;
            case 'r':
                weapon.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 'y':
                weapon.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            default:
                Debug.Log("Ivalid Color");
                break;
        }

        //Spawns weapon in a random position
        int nPos = Random.Range(0, spawnPositions.Count);
        Vector2 pos = spawnPositions[nPos];
        weapon.transform.position = pos;
    }

    private void spawnItem(){
        GameObject item;
        //Get random weapon name from list 'items'
        int itemPos = Random.Range(0, items.Count);
        
        switch(items[itemPos])
        {
        case "BuffItem1":
            item = Instantiate(buffItem1Prefab) as GameObject;
            item.GetComponent<Item>().Name = items[itemPos];
            break;
        case "DebuffItem1":
            item = Instantiate(debuffItem1Prefab) as GameObject;
            item.GetComponent<Item>().Name = items[itemPos];
            break;
        //If there is an error to get an item, default item will be buffItem1
        default:
            item = Instantiate(buffItem1Prefab) as GameObject;
            item.GetComponent<Item>().Name = items[itemPos];
            Debug.Log("Item does not exist");
            break;
        }

        //Spawns item in a random position
        //TODO: Define spawn spots in the map
        float x = Random.Range(-1.3f, 1.3f);
        item.transform.position = new Vector2(x, -0.28f);
    }

    IEnumerator itemCoroutine(){
        while(canSpawn){
            
            yield return new WaitForSeconds(respawnTime);
                if(i >= 30){
                    i = i%30;
                    spawnItem();
                }
                else{
                    //i += Random.Range(1, 10);
                    spawnWeapon();
                }
        }
    }
}
