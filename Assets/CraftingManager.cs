using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public PlayerLevelComponent levelComponent;
    public TextMesh craftingText;

    //Materials
    public Dictionary<string, int> materials;

    public int paper = 0;
    public int wood = 0;
    public int metal = 0;
    public int gunpowder = 0;
    public int brass = 0;
    public int copper = 0;
    public int plastic = 0;
    public int electronics = 0;
    public int glue = 0;
    public int cabling = 0;
    public int bindings = 0;
    public int ceramics = 0;
    public int weapon_parts = 0;
    public int nuts_bolts = 0;
    public int rubber = 0;

    //Food
    public int protein = 0;
    public int sugar = 0;
    public int salt = 0;
    public int fibre = 0;
    public int water = 0;

    //Medical
    public int antiseptics = 0;
    public int alcohol = 0;

    void InitMaterials()
    {
        materials = new Dictionary<string, int>();

        materials["paper"] = paper;
        materials["wood"] = wood;
        materials["metal"] = metal;
        materials["plastic"] = plastic;
        materials["electronics"] = electronics;
        materials["glue"] = glue;
        materials["cabling"] = cabling;
        materials["bindings"] = bindings;
        materials["ceramics"] = ceramics;
        materials["gunpowder"] = gunpowder;
        materials["brass"] = brass;
        materials["copper"] = copper;
        materials["weapon_parts"] = weapon_parts;
        materials["nuts_bolts"] = nuts_bolts;
        materials["rubber"] = rubber;

        materials["protein"] = protein;
        materials["sugar"] = sugar;
        materials["salt"] = salt;
        materials["fibre"] = fibre;
        materials["water"] = water;

        materials["antiseptics"] = antiseptics;
        materials["alcohol"] = alcohol;
    }

    public void Scrap(Craftable craftable)
    {
        if (craftable.materials.Count > 0)
        {
            for (int i = 0; i < craftable.materials.Count; i++)
            {
                materials[craftable.materials[i]] += craftable.amounts[i];
            }
            
            Destroy(craftable.gameObject);

            levelComponent.AddExp(10);

            UpdateText();
        }
        else
        {
            Debug.LogError("Material String is Empty or Wrong! - " + craftable);
        }

        
    }

    private void UpdateText()
    {
        craftingText.text = string.Empty;
        foreach (var item in materials)
        {
            craftingText.text += item.Key + ": " + item.Value + "\n";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMaterials();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
          
    }
}
