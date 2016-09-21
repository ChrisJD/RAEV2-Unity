using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Extensions;

namespace RAEV2
{
    public class AeDeck : MonoBehaviour
    {
        public static int MAX_BLITZ_CARDS = 10;
        private static int BASE_BLITZ_CARDS = 2;
        
        public int NumberOfBLitzCards { get; private set; }

        [SerializeField]
        private Button shuffleButton;
        [SerializeField]
        private Button drawAeButton;
        [SerializeField]
        private Button drawAeDiscardButton;
        //[SerializeField]
        //private Button shuffleAeButton;
        [SerializeField]
        private Image currentAeCardImage;
        private Color aePlaceHolderColor;
        [SerializeField]
        private Sprite aeBlitzCard;
        [SerializeField]
        private Sprite aeCardBack;
        [SerializeField]
        private Sprite aeDiscardPile;
        [SerializeField]
        private ShuffleDialog shuffleDialog;

        private List<Card> aeDeck = new List<Card>();
        private int currentAeCardIndex = -1;
        private Card currentAeCard = null;
        private Card blitzCard = null;
        
        private void Awake()
        {
            NumberOfBLitzCards = BASE_BLITZ_CARDS;
            aePlaceHolderColor = currentAeCardImage.color;

            List<Sprite> sprites = new List<Sprite>(Resources.LoadAll<Sprite>("AE"));
            foreach (Sprite s in sprites)
            {
                aeDeck.Add(new Card(s.name, s, aeCardBack));
            }

            blitzCard = new Card(aeBlitzCard.name, aeBlitzCard, aeCardBack);
            for (int i = 0; i < NumberOfBLitzCards; i++)
            {
                aeDeck.Add(blitzCard);
            }

            drawAeButton.onClick.AddListener(NextAeCard);
            drawAeDiscardButton.onClick.AddListener(PreviousAeCard);
            shuffleButton.onClick.AddListener(shuffleDialog.ShowDialog);

            shuffleDialog.ShowDialog();
        }

        private void PreviousAeCard()
        {
            ChangeAeCard(currentAeCardIndex - 1);
        }

        private void NextAeCard()
        {
            ChangeAeCard(currentAeCardIndex + 1);
        }

        private void ChangeAeCard(int newCardIndex)
        {
            // Index of current card (the largest AE Deck card image in the UI)
            // -1 if no cards have been drawn yet
            // aeDeck.Count when last card in deck is current.
            currentAeCardIndex = Mathf.Clamp(newCardIndex, -1, aeDeck.Count);
            // index of card on top of discard pile
            int previousAeCardIndex = currentAeCardIndex - 1;
            // index of card on top of draw pile
            int nextAeCardIndex = currentAeCardIndex + 1;

            // Update state of current card area
            if (IsValidCardIndex(currentAeCardIndex))
            {
                currentAeCard = aeDeck[currentAeCardIndex];
                currentAeCardImage.sprite = currentAeCard.CardFront;
                currentAeCardImage.color = Color.white;
            }
            else
            {
                currentAeCard = null;
                currentAeCardImage.sprite = null;
                currentAeCardImage.color = aePlaceHolderColor;
            }

            // Update state of discard pile
            if (IsValidCardIndex(previousAeCardIndex))
            {
                drawAeDiscardButton.GetComponent<Image>().sprite = aeDeck[previousAeCardIndex].CardFront;
            }
            else
            {
                drawAeDiscardButton.GetComponent<Image>().sprite = aeDiscardPile;
            }

            // Update state of draw pile
            drawAeButton.GetComponent<Image>().enabled = IsValidCardIndex(nextAeCardIndex); // < aeDeck.Count)
        }

        private bool IsValidCardIndex(int index)
        {
            return (index >= 0) && (index < aeDeck.Count);
        }

        public void Shuffle(int newBlitzCardCount)
        {            
            ApplyBlitzChange(newBlitzCardCount);
            aeDeck.Shuffle();
            ChangeAeCard(-1);
        }

        private void ApplyBlitzChange(int newNumberOfBLitzCards)
        {
            int blitzCardCountChange = newNumberOfBLitzCards - NumberOfBLitzCards;
            if (blitzCardCountChange != 0)
            {
                while (blitzCardCountChange < 0)
                {
                    int toRemove = aeDeck.IndexOf(blitzCard);
                    aeDeck.RemoveAt(toRemove);
                    blitzCardCountChange++;
                }

                while (blitzCardCountChange > 0)
                {
                    aeDeck.Add(blitzCard);
                    blitzCardCountChange--;
                }
            }

            NumberOfBLitzCards = aeDeck.FindAll(x => x == blitzCard).Count;
            Debug.Log("Blitz cards = " + NumberOfBLitzCards);
        }
    }
}