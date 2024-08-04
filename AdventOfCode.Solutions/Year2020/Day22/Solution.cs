namespace AdventOfCode.Solutions.Year2020.Day22;

class Solution : SolutionBase
{
    Queue<int> firstDeck;
    Queue<int> secondDeck;

    public Solution()
        : base(22, 2020, "")
    {
        string[] paragraphs = Input.SplitByParagraph();
        firstDeck = ParseDeck(paragraphs[0]);
        secondDeck = ParseDeck(paragraphs[1]);
    }

    protected override string SolvePartOne()
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

        return (PlayerScore(firstDeck) + PlayerScore(secondDeck)).ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
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
}
