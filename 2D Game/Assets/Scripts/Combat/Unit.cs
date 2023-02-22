using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Faction
{
    Player,
    Enemy
}

public class Unit : MonoBehaviour
{
    public Faction faction;

    public SpriteRenderer spriteRenderer;

    public Animator spriteAnimator;
    public Animator weaponAnimator;

    public Slider healthSlider;

    void Start()
    {
        if (faction == Faction.Enemy)
        {
            spriteRenderer.flipX = true;
        }
    }
}