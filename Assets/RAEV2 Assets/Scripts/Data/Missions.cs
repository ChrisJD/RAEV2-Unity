using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;

namespace RAEV2
{
    public class Missions
    {
        public enum MissionSet
        {
            Core,
            TwinShadows,
            ReturnToHoth,
            TheBespinGambit,
            AllyAndVillianPacks
        }

        Dictionary<MissionSet, List<Card>> missions = new Dictionary<MissionSet, List<Card>>();

        public Missions()
        {
            List<Sprite> sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Mission/Core"));
            ParseMissionSpriteArray(MissionSet.Core, sprites);
            sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Mission/Return to Hoth"));
            ParseMissionSpriteArray(MissionSet.ReturnToHoth, sprites);
            sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Mission/The Bespin Gambit"));
            ParseMissionSpriteArray(MissionSet.TheBespinGambit, sprites);
            sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Mission/Twin Shadows"));
            ParseMissionSpriteArray(MissionSet.TwinShadows, sprites);
            sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Mission/AVP"));
            ParseMissionSpriteArray(MissionSet.AllyAndVillianPacks, sprites);

            Debug.Log("Cards in each set:");
            foreach (var missionSet in missions)
            {
                Debug.Log(missionSet.Key.ToString() + ": " + missionSet.Value.Count);
            }
        }

        void ParseMissionSpriteArray(MissionSet set, List<Sprite> missionSprites)
        {
            while (missionSprites.Count > 0)
            {
                Sprite currentSprite = missionSprites[0];
                missionSprites.RemoveAt(0);
                string currentSide;
                string currentSpriteName = SplitSideFromName(currentSprite.name, out currentSide);

                int i = 0;
                for (i = 0; i < missionSprites.Count; i++)
                {
                    string missionSide;
                    string missionSpriteName = SplitSideFromName(missionSprites[i].name, out missionSide);

                    if (missionSpriteName == currentSpriteName && currentSide != missionSide)
                    {
                        break;
                    }
                }

                if (i < missionSprites.Count)
                {
                    AddMission(set, currentSpriteName, currentSide == "Front" ? currentSprite : missionSprites[i], currentSide == "Back" ? missionSprites[i] : currentSprite);
                    missionSprites.RemoveAt(i);
                }
            }
        }

        string SplitSideFromName(string spriteName, out string side)
        {
            int lastSpace = spriteName.LastIndexOf(" ");
            side = spriteName.Substring(lastSpace);
            string missionName = spriteName.Substring(0, lastSpace);

            return missionName;
        }

        void AddMission(MissionSet set, string name, Sprite front, Sprite back)
        {
            Assert.IsNotNull(front);
            Assert.IsNotNull(back);

            List<Card> setCards;
            if (!missions.TryGetValue(set, out setCards))
            {
                setCards = new List<Card>();
                missions.Add(set, setCards);
            }

            setCards.Add(new Card(name, front, back));
        }

        public List<string> GetMissionNames(MissionSet set)
        {
            return missions[set].Select(mission => mission.Name).ToList();
        }

        public List<MissionSet> GetMissionSets()
        {
            return missions.Keys.ToList();
        }

        public List<string> GetMissionSetsString()
        {
            return missions.Keys.Select(set => set.ToString()).ToList();
        }

        public Card GetMissionCard(MissionSet set, string name)
        {
            return missions[set].FirstOrDefault(card => card.Name == name);
        }
    }
}