using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] List<SpawnableObject> spawnableObjects = new List<SpawnableObject>();
    [SerializeField] Transform itemListParent;
    [SerializeField] GameObject itemButtonPrefab;
    [SerializeField] GameObject buildMenuUI;

    void Start()
    {
        loadBuildableItems();
    }

    private void loadBuildableItems()
    {
        foreach (var obj in spawnableObjects)
        {
            GameObject buttonObj = Instantiate(itemButtonPrefab, itemListParent);
            Button button = buttonObj.GetComponent<Button>();
            Image iconImage = buttonObj.transform.Find("Icon").GetComponent<Image>();
            TMP_Text nameText = buttonObj.transform.Find("Name").GetComponent<TMP_Text>();
            TMP_Text priceText = buttonObj.transform.Find("Price").GetComponent<TMP_Text>();

            iconImage.sprite = obj.icon;
            nameText.text = obj.objectName;

            button.onClick.AddListener(() => OnItemSelected(obj));
        }
    }

    private void OnItemSelected(SpawnableObject obj)
    {
        Builder builder = FindFirstObjectByType<Builder>();
        if (builder != null)
        {
            builder.currentPrefab = obj.prefab;
            builder.PopulatePreview();

        }
    }
}
