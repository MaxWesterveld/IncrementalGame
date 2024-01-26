using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private CustomEvents cEvents;

    [SerializeField] private GameObject quitScreen;

    [Header("Ingredients")]
    public int dough;
    public int wood;
    public int apple;

    [Header("Recipies")]
    public int bread;
    public int applePie;

    [Header("ClickRewards")]
    public int clickReward = 1;

    [Header("RecepieCounter")]
    [SerializeField] private TextMeshProUGUI breadCount;
    [SerializeField] private TextMeshProUGUI applePieCount;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this); 
        }
        else
        {
            instance = this;
        }

        cEvents = FindObjectOfType<CustomEvents>();
    }


    private void Update()
    {
        breadCount.text = bread.ToString();
        applePieCount.text = applePie.ToString();
    }

    public void DoughClick()
    {
        _ = cEvents.isIncreased ? dough += (clickReward * 2) : dough += clickReward;
    }

    public void WoodClick()
    {
        int random = Random.Range(0, 100);
        if (cEvents.isIncreased)
        {
            _ = random >= 90 ? apple += (clickReward * 2) : wood += (clickReward * 2);
        }
        else
        {
            _ = random >= 90 ? apple += clickReward : wood += clickReward;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
