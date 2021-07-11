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
        //PlayerPrefs.SetString("ObjectResult", "cincin");
        PlayerPrefs.SetFloat("WordScore", 79);
        SpawnItem(PlayerPrefs.GetString("ObjName"), spawnLocation.position);
        SetStars();
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
