using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    [SerializeField] private TextMeshProUGUI text;
    private int score = 0;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Score: " + score;
    }
    public void UpdateScore()
    {
        this.score += 1;
        text.text = "Score: " + score;
    }
}
