using UnityEngine;
using UnityEngine.UI;

namespace RAEV2
{
    public class MissionSelector : MonoBehaviour
    {
        [SerializeField]
        private Dropdown missionsSetSelector;
        [SerializeField]
        private Dropdown missionsSelector;
        [SerializeField]
        private MissionCardView missionCardView;

        private Missions missions;
        private Missions.MissionSet currentSelectedSet = Missions.MissionSet.Core;

        private void Awake()
        {
            missions = new Missions();

            missionsSelector.onValueChanged.AddListener(MissionChanged);
            missionsSetSelector.onValueChanged.AddListener(MissionSetChanged);
            missionsSetSelector.AddOptions(missions.GetMissionSetsString());
            MissionSetChanged(0);
        }

        private void MissionSetChanged(int optionSelected)
        {
            string missionSet = missionsSetSelector.options[optionSelected].text;
            Missions.MissionSet set = (Missions.MissionSet)System.Enum.Parse(typeof(Missions.MissionSet), missionSet);
            currentSelectedSet = set;
            missionsSelector.ClearOptions();
            missionsSelector.AddOptions(missions.GetMissionNames(set));
            MissionChanged(0);
        }

        private void MissionChanged(int optionSelected)
        {
            string mission = missionsSelector.options[optionSelected].text;
            missionCardView.MissionCard = missions.GetMissionCard(currentSelectedSet, mission);
        }
    }
}