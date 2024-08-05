namespace AdventOfCode.Solutions.Year2020.Day22;

class Solution : SolutionBase
{
    string[] stringDecks;

    public Solution()
        : base(22, 2020, "")
    {
        stringDecks = Input.SplitByParagraph();
    }

    protected override string SolvePartOne()
    {
        return WinningPlayerScore(PlayCombat).ToString();
    }

    protected override string SolvePartTwo()
    {
        return WinningPlayerScore(PlayRecursiveCombat).ToString();
    }

    int WinningPlayerScore(Func<Queue<int>, Queue<int>, int> playGame)
    {
        Queue<int> firstDeck = ParseDeck(stringDecks[0]);
        Queue<int> secondDeck = ParseDeck(stringDecks[1]);

        int winner = playGame(firstDeck, secondDeck);

        int score;
        if (winner == 1)
        {
            score = PlayerScore(firstDeck);
        }
        else
        {
            score = PlayerScore(secondDeck);
        }

        return score;
    }

    Queue<int> ParseDeck(string stringRepresentation)
    {
        Queue<int> deck = new Queue<int>();

        foreach (string line in stringRepresentation.SplitByNewline().Skip(1))
        {
            deck.Enqueue(int.Parse(line));
        }

        return deck;
    }

    int PlayerScore(Queue<int> deck)
    {
        int score = 0;

        int multiplier = deck.Count();
        foreach (int card in deck)
        {
            score += card * multiplier;
            multiplier--;
        }

        return score;
    }

    int PlayCombat(Queue<int> firstDeck, Queue<int> secondDeck)
    {
        while (firstDeck.Count != 0 && secondDeck.Count() != 0)
        {
            int firstCard = firstDeck.Dequeue();
            int secondCard = secondDeck.Dequeue();

            if (firstCard > secondCard)
            {
                firstDeck.Enqueue(firstCard);
                firstDeck.Enqueue(secondCard);
            }
            else
            {
                secondDeck.Enqueue(secondCard);
                secondDeck.Enqueue(firstCard);
            }
        }

        if (firstDeck.Count() != 0)
        {
            return 1;
        }

        return 2;
    }

    int PlayRecursiveCombat(Queue<int> firstDeck, Queue<int> secondDeck)
    {
        HashSet<string> snapshots = new HashSet<string>();

        while (firstDeck.Count != 0 && secondDeck.Count() != 0)
        {
            if (RoundRepeats(snapshots, firstDeck, secondDeck))
            {
                return 1;
            }

            PlayRecursiveCombatRound(firstDeck, secondDeck);
        }

        if (firstDeck.Count() != 0)
        {
            return 1;
        }

        return 2;
    }

    void PlayRecursiveCombatRound(Queue<int> firstDeck, Queue<int> secondDeck)
    {
        int firstCard = firstDeck.Dequeue();
        int secondCard = secondDeck.Dequeue();

        int roundWinner;
        if (firstDeck.Count() >= firstCard && secondDeck.Count() >= secondCard)
        {
            roundWinner = PlayRecursiveCombat(
                new Queue<int>(firstDeck.Take(firstCard)),
                new Queue<int>(secondDeck.Take(secondCard))
            );
        }
        else if (firstCard > secondCard)
        {
            roundWinner = 1;
        }
        else
        {
            roundWinner = 2;
        }

        if (roundWinner == 1)
        {
            firstDeck.Enqueue(firstCard);
            firstDeck.Enqueue(secondCard);
        }
        else
        {
            secondDeck.Enqueue(secondCard);
            secondDeck.Enqueue(firstCard);
        }
    }

    string RoundSnapshot(Queue<int> firstDeck, Queue<int> secondDeck)
    {
        StringBuilder snapshot = new StringBuilder();

        foreach (int card in firstDeck)
        {
            snapshot.Append(card.ToString());
            snapshot.Append('|');
        }

        foreach (int card in secondDeck)
        {
            snapshot.Append('|');
            snapshot.Append(card.ToString());
        }

        return snapshot.ToString();
    }

    bool RoundRepeats(HashSet<string> snapshots, Queue<int> firstDeck, Queue<int> secondDeck)
    {
        string snapshot = RoundSnapshot(firstDeck, secondDeck);
        if (snapshots.Contains(snapshot))
        {
            return true;
        }

        snapshots.Add(snapshot);
        return false;
    }
}
