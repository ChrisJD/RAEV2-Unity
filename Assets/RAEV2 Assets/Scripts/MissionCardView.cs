using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RAEV2
{
    [RequireComponent(typeof(Button))]
    public class MissionCardView : MonoBehaviour
    {
        private Button _flipMissionCardButton;
        private Button FlipMissionCardButton
        {
            get
            {
                if (_flipMissionCardButton == null)
                {
                    _flipMissionCardButton = GetComponent<Button>();
                    _flipMissionCardButton.onClick.AddListener(FlipMissionCard);
                }

                return _flipMissionCardButton;
            }
        }

        private bool missionCardShowingFront = true;
        private Card missionCard = null;
        public Card MissionCard
        {
            set
            {
                missionCard = value;
                if (missionCard != null)
                {
                    Image missionImage = FlipMissionCardButton.GetComponent<Image>();
                    missionImage.sprite = missionCard.CardFront;
                    missionCardShowingFront = true;
                }
            }

            get
            {
                return missionCard;
            }
        }

        private void Awake ()
        {
        }

        private void FlipMissionCard()
        {
            Image missionImage = FlipMissionCardButton.GetComponent<Image>();
            missionCardShowingFront = !missionCardShowingFront;

            if (missionCardShowingFront)
            {
                missionImage.sprite = MissionCard.CardFront;
            }
            else
            {
                missionImage.sprite = MissionCard.CardBack;
            }
        }
    }
}