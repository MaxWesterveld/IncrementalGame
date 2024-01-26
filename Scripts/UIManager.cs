using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject alwaysVisible;

    [SerializeField] private GameObject[] zones;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI doughText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI appleText;
    [SerializeField] private TextMeshProUGUI breadText;
    [SerializeField] private TextMeshProUGUI applePieText;


    private void Update()
    {
        doughText.text = GameManager.instance.dough.ToString();
        woodText.text = GameManager.instance.wood.ToString();
        appleText.text = GameManager.instance.apple.ToString();
        breadText.text = GameManager.instance.bread.ToString();
        applePieText.text = GameManager.instance.applePie.ToString();
    }


    public void ToZone(int index)
    {
        foreach (var zone in zones)
        {
            zone.SetActive(false);
        }
        zones[index].SetActive(true);
    }
}
