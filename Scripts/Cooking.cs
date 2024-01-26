using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cooking : MonoBehaviour
{
    private CustomEvents cEvents;

    [SerializeField] private TextMeshProUGUI cooldownText;

    private int recipe;
    public bool isCooking;

    [Header("Prices")]
    private int doughPrice;
    private int applePrice;

    [Header("Cooldowns")]
    public float breadCooldown = 5;
    public float applePieCooldown = 20;

    private void Start()
    {
        cEvents = FindObjectOfType<CustomEvents>();
    }

    public void Bread()
    {
        doughPrice = 5;
        recipe = GameManager.instance.bread;

        if (!isCooking && doughPrice <= GameManager.instance.dough)
        {
            GameManager.instance.dough -= doughPrice;
            _ = cEvents.isRecuded ? StartCoroutine(StartCooking(breadCooldown * cEvents.cooldownReduced)) : StartCoroutine(StartCooking(5));
        }
    }

    public void ApplePie()
    {
        doughPrice = 20;
        applePrice = 5;
        recipe = GameManager.instance.applePie;

        if (!isCooking && doughPrice <= GameManager.instance.dough && applePrice <= GameManager.instance.apple)
        {
            GameManager.instance.dough -= doughPrice;
            GameManager.instance.apple -= applePrice;
            _ = cEvents.isRecuded ? StartCoroutine(StartCooking(20 * cEvents.cooldownReduced)) : StartCoroutine(StartCooking(5));
        }
    }

    private IEnumerator StartCooking(float cooldown)
    {
        isCooking = true;
        for (float i = cooldown; i >= 0; i--)
        {
            cooldownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (recipe == GameManager.instance.bread)
        {
            GameManager.instance.bread++;
        }
        else if (recipe == GameManager.instance.applePie)
        {
            GameManager.instance.applePie++;
        }

        isCooking = false;
    }
}
