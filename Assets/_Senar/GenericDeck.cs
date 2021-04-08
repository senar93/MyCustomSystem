using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class GenericDeck<T>
{
    [ReadOnly, SerializeField]
    protected List<T> cards = new List<T>();

    [SerializeField]
    protected bool _canBeRefill = false;
    [ShowIf("_canBeRefill"), SerializeField]
    protected DeckRefillOptions<T> refillSource;

    public bool IsEmpty { get => (cards != null && cards.Count > 0); }
    public bool CanBeRefill { get => (_canBeRefill && refillSource != null && refillSource.IsRefillOptionsValid); }



    /*public virtual void Shuffle()
    {
        
    }*/

    /*public virtual T Draw(bool remove = true)
    {

	}*/

    /*public virtual List<T> Draw(int i, bool failIfCannotBeDrawnAllRequiredCards = false, bool remove = true)
    {
        List<T> tmpCards = new List<T>();

        return tmpCards;
	}*/

    //public virtual void SetToRefillable
    public virtual List<T> GetCopyOfListOfAllCard()
    {
        return new List<T>(cards);
	}

    public void Refill()
    {
        if(CanBeRefill) 
        {
            cards.AddRange(refillSource.GetCards());
		}
	}

}



public abstract class DeckRefillOptions<T> 
{
    public abstract List<T> GetCards();
    public abstract bool IsRefillOptionsValid { get; }
    public abstract bool IsRefillOptionsEmpty { get; }
}



public class List_DeckRefillOptions<T> : DeckRefillOptions<T>
{
    public bool shuffleAfterRefill = true;
    public List<T> cards = new List<T>();

	public override bool IsRefillOptionsValid { get => (cards != null); }
    [ShowIf("IsRefillOptionsValid")]
    public override bool IsRefillOptionsEmpty { get => (IsRefillOptionsValid && cards.Count > 0); }

    public override List<T> GetCards()
	{
		if(IsRefillOptionsValid)
        {
            if (shuffleAfterRefill)
            {
                //WIP shuffle cards
            }
            return new List<T>(cards);
		}

        return null;
	}
}



public class GenericDeck_DeckRefillOptions<T> : DeckRefillOptions<T>
{
    public bool removeAfterRefill = true;
    public bool shuffleAfterRefill = true;
    public GenericDeck<T> deck = new GenericDeck<T>();

    public override bool IsRefillOptionsValid { get => (deck != null); }
    [ShowIf("IsRefillOptionsValid")]
    public override bool IsRefillOptionsEmpty { get => (IsRefillOptionsValid && deck.IsEmpty); }

    public override List<T> GetCards()
    {
        if (IsRefillOptionsValid)
        {
            if (shuffleAfterRefill)
            {
                //WIP shuffle tmp
            }
            if (removeAfterRefill)
            {
                //WIP clear deck
            }
            return deck.GetCopyOfListOfAllCard();
        }

        return null;
    }
}
