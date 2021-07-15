using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TMP_Text congratsText;
    public TMP_Text objNameText;


    [SerializeField]private Image starContainer;

    private void Start()
    {
        //PlayerPrefs.SetString("ObjName", "Bendera");
        //PlayerPrefs.SetFloat("WordScore", 57);
        SpawnItem(PlayerPrefs.GetString("ObjName"), new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 15));
        objNameText.text = PlayerPrefs.GetString("ObjName");
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
        if(score < 55)
        {
            congratsText.text = "Dicoba Lagi!";
            starContainer.sprite = stars[0];
            SoundControl.PlayCobaLagi();
        }
        else if (score >= 55 && score < 74)
        {
            congratsText.text = "Jangan Menyerah!";
            starContainer.sprite = stars[1];
            SoundControl.PlayJanganMenyerah();
        }
        else if (score >= 74 && score < 82)
        {
            congratsText.text = "Kamu Hebat!";
            starContainer.sprite = stars[2];
            SoundControl.PlayKamuHebat();
        }
        else if (score >= 82 && score <= 100)
        {
            int rand = Random.Range(0, 2);

            if(rand == 0)
            {
                congratsText.text = "Kamu Luar Biasa!";
                SoundControl.PlayKamuLuarBiasa();
            }
            else
            {
                congratsText.text = "Kamu Pintar!";
                SoundControl.PlayKamuPintar();
            }
            starContainer.sprite = stars[3];
        }
    }
}
