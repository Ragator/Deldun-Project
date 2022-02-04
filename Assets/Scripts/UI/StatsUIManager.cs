using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUIManager : MonoBehaviour
{
    private PlayerStats myPlayerStats;

    [SerializeField] private TextMeshProUGUI invBaseStatsText;
    [SerializeField] private TextMeshProUGUI invLevelStatsText;

    private void Start()
    {
        myPlayerStats = GameObject.FindWithTag(DeldunProject.Tags.player).GetComponent<PlayerStats>();

        myPlayerStats.onStatModifiedCallback += UpdateStatsText;

        UpdateStatsText();
    }

    private void UpdateStatsText()
    {
        invBaseStatsText.text = (
            "Maximum Health : " + myPlayerStats.maxHealth.Value
            + "\n"
            + "Maximum Sanity : " + myPlayerStats.maxSanity.Value
            + "\n"
            + "Maximum Stamina : " + myPlayerStats.maxStamina.Value

            + "\n"
            + "\n"

            + "Physical Defense :" + myPlayerStats.physicalResistance.Value
            + "\n"
            + "Blood Defense : " + myPlayerStats.bloodResistance.Value
            + "\n"
            + "Arcane Defense : " + myPlayerStats.arcaneResistance.Value
            );

        invLevelStatsText.text = (
            "Level " + myPlayerStats.playerLevel

            + "\n"
            + "\n"

            + "Longevity " + myPlayerStats.longevity.Value
            + "\n"
            + "Fitness " + myPlayerStats.fitness.Value
            + "\n"
            + "Willpower " + myPlayerStats.willpower.Value
            + "\n"
            + "Brawn " + myPlayerStats.brawn.Value
            + "\n"
            + "Skill " + myPlayerStats.skill.Value
            + "\n"
            + "Vision " + myPlayerStats.vision.Value);
    }
}
