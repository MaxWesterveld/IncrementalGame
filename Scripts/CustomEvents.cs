using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomEvents : MonoBehaviour
{
    [Header("Worker Efficiency")]
    [HideInInspector] public bool isWorkerEfficient;

    [Header("Increase ClickRewards")]
    [HideInInspector] public bool isIncreased;

    [Header("Reduce Cooldown")]
    [HideInInspector] public bool isRecuded;
    [HideInInspector] public float cooldownReduced;

    [SerializeField] private GameObject eventScreen;
    [SerializeField] private TextMeshProUGUI eventText;

    private void Start()
    {
        StartCoroutine(EventTrigger());
    }

    private void WorkerEfficiency()
    {
        //Randomize worker efficiency
        isWorkerEfficient = true;
        eventText.text = "Worker Efficiency has been doubled!";
    }

    private void IncreaseClickrewards()
    {
        //Incrase the amount you get for clicking
        isIncreased = true;
        eventText.text = "Clickrewards have been doubled!";
    }

    private void ReduceCooldown()
    {
        //Decrease cooldown for cooking
        isRecuded = true;
        cooldownReduced = Random.Range(0.75f, 0.5f);
        eventText.text = $"Cooking cooldowns have been recuced by {cooldownReduced:F2}s";
    }

    private IEnumerator EventTrigger()
    {
        int cooldown = 1;
        for (int i = cooldown; i >= 0; i++)
        {
            yield return new WaitForSeconds(1);
            float random = Random.Range(0, 1000);
            int eventPicker = Random.Range(1, 4);
            if (random >= 999)
            {
                if (eventPicker == 1)
                {
                    WorkerEfficiency();
                }
                else if (eventPicker == 2)
                {
                    IncreaseClickrewards();
                }
                else if (eventPicker == 3)
                {
                    ReduceCooldown();
                }

                eventScreen.SetActive(true);

                StopCoroutine(EventTrigger());
                yield return new WaitForSeconds(30);

                eventScreen.SetActive(false);
            }
        }
        StartCoroutine(EventTrigger());
    }
}

