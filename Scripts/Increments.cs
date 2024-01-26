using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Increments : MonoBehaviour
{
    private WorkerManager worker;
    private Cooking cooking;
    private SQLite sqlite;

    [SerializeField] private TextMeshProUGUI unlockText;
    [SerializeField] private TextMeshProUGUI incrementPointText;
    [SerializeField] private TextMeshProUGUI currentPointsText;

    [Header("Cost texts")]
    [SerializeField] private TextMeshProUGUI clickRewardCost;
    [SerializeField] private TextMeshProUGUI reduceCooldownCost;
    [SerializeField] private TextMeshProUGUI workerEffCost;

    [SerializeField] private GameObject incrementScreen;


    private int incrementPoints;
    [SerializeField] private int currentPoints;

    public int clickrewardLevel = 0;
    public int reducecooldownLevel = 0;
    public int workerefficiencyLevel = 0;

    public float gatherEfficiency;

    [SerializeField] private int[] costs;

    private void Start()
    {
        worker = FindObjectOfType<WorkerManager>();
        cooking = FindObjectOfType<Cooking>();
        sqlite = FindObjectOfType<SQLite>();

        for (int i = 0; i < costs.Length; i++)
        {
            costs[i] = (int)Mathf.Pow(2, i);
            Debug.Log(costs[clickrewardLevel]);
        }
    }


    private void Update()
    {
        currentPointsText.text = $"Current points: {currentPoints}";
        incrementPoints = (int)(GameManager.instance.bread * 0.01f + GameManager.instance.applePie * 0.02f);

        incrementPointText.text = "Increment now and recieve " + incrementPoints.ToString() + " points";

        clickRewardCost.text = $"Cost: {costs[clickrewardLevel]}";
        reduceCooldownCost.text = $"Cost: {costs[reducecooldownLevel]}";
        workerEffCost.text = $"Cost: {costs[workerefficiencyLevel]}";

        if (worker.boughtWorkers > 4)
        {
            unlockText.enabled = false;
            incrementScreen.SetActive(true);
        }

        if (clickrewardLevel > 9)
        {
            //Disable button
        }
    }

    public void Increment()
    {
        //Increment and reset progress in trade for increment points
        currentPoints += incrementPoints;
        sqlite.ResetStats();
    }

    public void ClickReward()
    {
        //Calculate the updated cost
        int cost = costs[clickrewardLevel];

        //Check if the player has enough points to purchase the upgrade
        if (currentPoints >= cost)
        {
            //Increase rewards for clicking by 2x
            GameManager.instance.clickReward *= 2;

            // ubtract the cost from the current points and increase the upgrade level
            currentPoints -= cost;
            clickrewardLevel++;
        }
    }


    public void ReduceCooldown()
    {
        //Calculate the updated cost
        int cost = costs[reducecooldownLevel];

        //Check if the player has enough points to purchase the upgrade
        if (currentPoints >= cost)
        {
            //Reduce cooldowns by 2% each time
            cooking.breadCooldown *= 0.98f;
            cooking.applePieCooldown *= 0.98f;

            //Subtract the cost from the current points and increase the upgrade level
            currentPoints -= cost;
            reducecooldownLevel++;

            //Update the cost text
        }
    }

    public void WorkerEfficiency()
    {
        //Calcuylate the updated cost
        int cost = costs[workerefficiencyLevel];

        //Check if the player has enough points to purchase the upgrade
        if (currentPoints >= cost)
        {
            //Make the workers more efficient my gathering 1.2x per level
            gatherEfficiency *= 1.2f;

            currentPoints -= cost;
            workerefficiencyLevel++;
        }
    }
}