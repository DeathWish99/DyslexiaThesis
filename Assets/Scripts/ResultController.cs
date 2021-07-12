using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    [System.Serializable]
    public struct Items
    {
        public GameObject obj;
        public string name;
    };

    public List<Items> spawnableItems;
    public List<Sprite> stars;
    public Transform spawnLocation;

    [SerializeField]private Image starContainer;

    private void Start()
    {
        //PlayerPrefs.SetString("ObjName", "Glob");
        //PlayerPrefs.SetFloat("WordScore", 79);
        SpawnItem(PlayerPrefs.GetString("ObjName"), new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 15));
        SetStars();
    }

    public bool SpawnItem(string itemName, Vector3 pos)
    {
        Debug.Log(itemName);
        int itemIndex = spawnableItems.FindIndex(x => x.name == itemName.Trim());
        Debug.Log(itemIndex);
        if(itemIndex > -1)
        {
            Items spawnThis = spawnableItems[itemIndex];
            Debug.Log(spawnThis.name);
            Instantiate(spawnThis.obj, pos, spawnThis.obj.transform.rotation);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetStars()
    {
        float score = PlayerPrefs.GetFloat("WordScore");

        //Nanti masukin play sound di tiap tempat
        if(score < 60)
        {
            starContainer.sprite = stars[0];
        }
        else if (score >= 60 && score < 79)
        {
            starContainer.sprite = stars[1];
            SoundControl.PlayOneStar();
        }
        else if (score >= 79 && score < 87)
        {
            starContainer.sprite = stars[2];
            SoundControl.PlayTwoStar();
        }
        else if (score >= 87 && score <= 100)
        {
            starContainer.sprite = stars[3];
            SoundControl.PlayThreeStar();
        }
    }
}
