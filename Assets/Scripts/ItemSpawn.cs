using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [System.Serializable]
    public struct Items
    {
        public GameObject obj;
        public string name;
    };

    public List<Items> spawnableItems;
    public Transform spawnLocation;

    private void Start()
    {
        //PlayerPrefs.SetString("ObjectResult", "cincin");
        SpawnItem(PlayerPrefs.GetString("ObjectResult"), spawnLocation.position);
    }

    public bool SpawnItem(string itemName, Vector3 pos)
    {
        int itemIndex = spawnableItems.FindIndex(x => x.name == itemName.ToLower().Trim());
        if(itemIndex > -1)
        {
            Items spawnThis = spawnableItems[itemIndex];
            spawnThis.obj.transform.localScale = new Vector3(2000, 2000, 2000);
            Instantiate(spawnThis.obj, pos, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }
}
