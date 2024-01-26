using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WorkerManager : MonoBehaviour
{
    private Cooking cooking;
    private CustomEvents cEvents;

    [SerializeField] private int[] breadPrice;
    [SerializeField] private int[] applePiePrice;

    public int currentWorkers;
    public int boughtWorkers;

    private bool isDoughBought = false;
    private bool isWoodBought = false;
    private bool isBreadBought = false;
    private bool isApplePieBought = false;

    [SerializeField] private TextMeshProUGUI currentWorkerText;

    [SerializeField] private TextMeshProUGUI breadPriceText;
    [SerializeField] private TextMeshProUGUI applePiePriceText;
    [SerializeField] private Image workerImage;
    [SerializeField] private GameObject priceHolder;

    private void Start()
    {
        cEvents = FindObjectOfType<CustomEvents>();
        cooking = FindObjectOfType<Cooking>();
    }

    private void Update()
    {
        SetWorkerPrice();

        currentWorkerText.text = "Current workers: " + currentWorkers.ToString();
    }

    private void SetWorkerPrice()
    {
        if (boughtWorkers <= 4)
        {
            breadPriceText.text = breadPrice[boughtWorkers].ToString();
            applePiePriceText.text = applePiePrice[boughtWorkers].ToString();
        }
    }

    public void BuyWorker()
    {
        if (boughtWorkers < 5 && breadPrice[boughtWorkers] <= GameManager.instance.bread && applePiePrice[boughtWorkers] <= GameManager.instance.applePie)
        {
            GameManager.instance.bread -= breadPrice[boughtWorkers];
            GameManager.instance.applePie -= applePiePrice[boughtWorkers];
            boughtWorkers++;
            currentWorkers++;
        }

        if (currentWorkers == 5)
        {
            workerImage.enabled = false;
            priceHolder.SetActive(false);
        }
    }

    public void SpendWorker()
    {
        if (currentWorkers == 0)
        {
            // Cannot spend more workers
            return;
        }
        SetWorkerPrice();
    }

    public IEnumerator DoughAutomationCoroutine()
    {
        GameManager.instance.DoughClick();
        if (cEvents.isWorkerEfficient)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(DoughAutomationCoroutine());
    }

    public IEnumerator WoodAutomationCoroutine()
    {
        GameManager.instance.WoodClick();
        if (cEvents.isWorkerEfficient)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(WoodAutomationCoroutine());
    }

    private IEnumerator BreadAutomationCoroutine()
    {
        cooking.Bread();
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(BreadAutomationCoroutine());
    }

    private IEnumerator ApplePieAutomationCoroutine()
    {
        cooking.ApplePie();
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(ApplePieAutomationCoroutine());
    }


    public void DoughAutomation()
    {
        bool isUnlocked = false;

        if (!isUnlocked && !isDoughBought && currentWorkers >= 1)
        {
            currentWorkers--;
            isUnlocked = true;
            isDoughBought = true;
        }

        if (isUnlocked)
        {
            StartCoroutine(DoughAutomationCoroutine());
        }
    }

    public void WoodAutomation()
    {
        bool isUnlocked = false;

        if (!isUnlocked && !isWoodBought && currentWorkers >= 1)
        {
            currentWorkers--;
            isUnlocked = true;
            isWoodBought = true;
        }

        if (isUnlocked)
        {
            StartCoroutine(WoodAutomationCoroutine());
        }
    }


    public void BreadAutomation()
    {
        bool isUnlocked = false;

        if (!isUnlocked && !isBreadBought && currentWorkers >= 1)
        {
            currentWorkers--;
            isUnlocked = true;
            isBreadBought = true;
        }
        if (isUnlocked)
        {
            StartCoroutine(BreadAutomationCoroutine());
        }

    }

    public void ApplePieAutomation()
    {
        bool isUnlocked = false;

        if (!isUnlocked && !isApplePieBought && currentWorkers >= 1)
        {
            currentWorkers--;
            isUnlocked = true;
            isApplePieBought = true; 
        }
        if (isUnlocked)
        {
            StartCoroutine(ApplePieAutomationCoroutine());
        }
    }
}
