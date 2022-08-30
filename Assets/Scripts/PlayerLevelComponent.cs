using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerLevelComponent : MonoBehaviour
{
    public InputDevice targetController;

    public int level = 1;
    public int exp = 0;
    public int expNext = 0;
    public int skillPoints = 2;
    // Start is called before the first frame update

    public TextMesh text;
    public void AddExp(int addExp)
    {
        exp += addExp;

        if (exp >= expNext)
        {
            level += 1;
            skillPoints += 1;
            exp -= expNext;
            expNext = (int) (50 * Math.Pow(level, 3) - 150 * Math.Pow(level, 2) + 400 * level) / 3;
        }

        UpdateText();
    }

    void UpdateText()
    {
        text.text = string.Format("Level: {0}\nExp: {1}/{2}\nSP: {3}", level, exp, expNext, skillPoints);
    }

    void Start()
    {
        UpdateText();
    } 

    // Update is called once per frame
    void Update()
    {
           
   

      //var inputDevices = new List<InputDevice>();
      //InputDevices.GetDevices(inputDevices);
      //foreach (var device in inputDevices)
      //{
      //  device.TryGetFeatureValue(CommonUsages.primaryButton, out bool xButtonPressed);
      //
      //  if (xButtonPressed)
      //  {
      //    exp += 1;
      //  }
      //} 

      

    }

    private void FixedUpdate()
    {
        
    }
}
