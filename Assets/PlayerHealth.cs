using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TextMesh text;
    [SerializeField] public int hpMax;
    public int hp;
    public double hpDegredation;

    public int staminaMax;
    public int stamina;
    public double staminaDegredation;
    public float staminaRegenTime;
    private float lastRegenTimeStamp;

    public double hunger;
    public double thirst;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
        }
    }

    public bool IsDead()
    {
        if (hp <= 0)
            return true;

        return false;
    }

    public void DecreaseStamina(int value)
    {
        stamina -= value;

        if (stamina <= 0)
            stamina = 0;
    }

    public bool IsOutOfStamina()
    {
        if (stamina <= 0)
            return true;

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        lastRegenTimeStamp = Time.time;
    }

    void UpdateText()
    {
        text.text = string.Format("HP: {1}/{2}\nST: {2}/{3}\nH: {4}\nT: {5}", hp, hpMax, stamina, staminaMax, hunger, thirst);
    }

    // Update is called once per frame
    void Update()
    {
        //Thirst

        //Hunger

        //Degredation

        //Stamina regen


        if(stamina < staminaMax && lastRegenTimeStamp - Time.time >= staminaRegenTime)
        {
            stamina++;
            lastRegenTimeStamp = Time.time;
            Update();
        }
    }
}
