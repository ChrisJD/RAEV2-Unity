using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

namespace RAEV2
{
    public class ShuffleDialog : MonoBehaviour
    {
        // UI Elements
        [SerializeField]
        private Button dialogYes;
        [SerializeField]
        private Button dialogNo;
        [SerializeField]
        private Button moreBlitz;
        [SerializeField]
        private Button lessBlitz;
        [SerializeField]
        private Text   blitzCount;

        [SerializeField]
        private AeDeck aeCards;
        
        private int newBlitzCardCount;
        
        private void Awake()
        {
            dialogYes.onClick.AddListener(ConfirmShuffle);
            dialogNo.onClick.AddListener(CancelShuffle);
            moreBlitz.onClick.AddListener(BlitzUp);
            lessBlitz.onClick.AddListener(BlitzDown);

            // On the first shuffle the deck must be setup/shuffled
            dialogNo.interactable = false;
        }

        private void OnEnable()
        {
            newBlitzCardCount = aeCards.NumberOfBLitzCards;
            blitzCount.text = "" + newBlitzCardCount;
        }
        
        public void ShowDialog()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }

        private void ConfirmShuffle()
        {
            aeCards.Shuffle(newBlitzCardCount);

            // Re-enable the cancel button for the dialog if it is disabled (only true on shuffles after the first)
            dialogNo.interactable = true;

            gameObject.SetActive(false);
        }

        private void CancelShuffle()
        {
            gameObject.SetActive(false);
        }

        private void BlitzUp()
        {
            UpdateBlitzCount(newBlitzCardCount + 1);
        }

        private void BlitzDown()
        {
            UpdateBlitzCount(newBlitzCardCount - 1);
        }

        private void UpdateBlitzCount(int newCount)
        {
            newBlitzCardCount = Mathf.Clamp(newCount, 0, AeDeck.MAX_BLITZ_CARDS);
            blitzCount.text = "" + newBlitzCardCount;
        }
    }
}