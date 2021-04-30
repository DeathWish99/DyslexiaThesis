using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public List<LevelDict> levelsToSpawn;
    public Vector2 iconSpacing;
    private int numberOfLevels;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;

    // Start is called before the first frame update
    void Start()
    {
        numberOfLevels = levelsToSpawn.Count;
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        LoadPanels(totalPages);

        void LoadPanels(int numberOfPanels)
        {
            GameObject panelClone = Instantiate(levelHolder) as GameObject;
            PageSwiper swiper = levelHolder.AddComponent<PageSwiper>();
            swiper.totalPages = numberOfPanels;

            for (int i = 1; i <= numberOfPanels; i++)
            {
                GameObject panel = Instantiate(panelClone) as GameObject;
                panel.transform.SetParent(thisCanvas.transform, false);
                panel.transform.SetParent(levelHolder.transform);
                panel.name = "Page-" + i;
                panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
                SetUpGrid(panel);
                int numberOfIcons = i == numberOfPanels ? numberOfLevels - currentLevelCount : amountPerPage;
                LoadIcons(numberOfIcons, panel);
            }
            Destroy(panelClone);
        }
        void SetUpGrid(GameObject panel)
        {
            GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
            grid.childAlignment = TextAnchor.MiddleCenter;
            grid.spacing = iconSpacing;
        }
        void LoadIcons(int numberOfIcons, GameObject parentObject)
        {
            foreach(LevelDict level in levelsToSpawn)
            {
                currentLevelCount++;
                GameObject icon = Instantiate(levelIcon) as GameObject;
                icon.transform.SetParent(thisCanvas.transform, false);
                icon.transform.SetParent(parentObject.transform);
                icon.name = level.name;
                icon.GetComponentInChildren<TextMeshProUGUI>().SetText(level.name);
            }
        }
    }
}
