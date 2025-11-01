using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.Serializable]
    public class PlayerStats
    {
        public string playerName;
        public float survivalTime;
        public float bestTime;
    }

    public List<PlayerStats> players = new List<PlayerStats>();

    public TextMeshProUGUI scoreboardText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // Update survival times for non-taggers
        foreach (var player in players)
        {
            if (!IsTagger(player.playerName))
            {
                player.survivalTime += Time.deltaTime;
                if (player.survivalTime > player.bestTime)
                    player.bestTime = player.survivalTime;
            }
        }

        UpdateScoreboard();
    }

    public bool IsTagger(string playerName)
    {
        // Find the current tagger in scene
        var taggers = FindObjectsOfType<PlayerBase>(true);
        foreach (var t in taggers)
        {
            if (t.isTagger && t.playerName == playerName)
                return true;
        }
        return false;
    }

    public void PlayerTagged(string taggerName, string runnerName)
    {
        // Reset the runner’s timer
        var runner = players.FirstOrDefault(p => p.playerName == runnerName);
        if (runner != null)
        {
            runner.survivalTime = 0;
        }
    }

    private void UpdateScoreboard()
    {
        var sorted = players.OrderByDescending(p => p.bestTime).ToList();

        string display = "<b>🏆 SURVIVAL LEADERBOARD 🕒</b>\n";
        for (int i = 0; i < sorted.Count; i++)
        {
            display += $"{i + 1}. {sorted[i].playerName} - {sorted[i].bestTime:F2} sec\n";
        }

        scoreboardText.text = display;
    }
}
