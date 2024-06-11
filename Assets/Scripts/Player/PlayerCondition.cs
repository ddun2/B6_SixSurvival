using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition hunger { get { return uiCondition.hunger; } }
    //Condition thirsty { get { return uiCondition.thirsty; } }
    Condition temperature { get { return uiCondition.temperature; } }

    public float HealthDecay;

    public event Action onTakeDamage;

    void Update()
    {
        hunger.Sub(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        //thirsty.Sub(thirsty.passiveValue * Time.deltaTime);

        //if (hunger.curValue == 0f/* && thirsty.curValue == 0f*/)
        //{
        //    health.Sub((HealthDecay * 2.0f) * Time.deltaTime);
        //}
        //else 
        if (hunger.curValue == 0f/* || thirsty.curValue == 0f*/)
        {
            health.Sub(HealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    //public void Drink(float amount)
    //{
    //    thirsty.Add(amount);
    //}

    public void Die()
    {
        Debug.Log("Die");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Sub(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Sub(amount);
        return true;
    }
}
