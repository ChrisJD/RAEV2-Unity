using UnityEngine;
using System.Collections;

namespace RAEV2
{
    public class Card
    {
        public Sprite CardFront { private set; get; }
        public Sprite CardBack { private set; get; }
        public string Name { private set; get; }

        public Card(string name, Sprite front, Sprite back)
        {
            Name = name;
            CardFront = front;
            CardBack = back;
        }
    }
}