namespace AdventOfCode.Solutions.Utils;

public static class CollectionUtils
{
    public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> input) =>
        input.Aggregate(input.First(), (intersector, next) => intersector.Intersect(next));

    public static string JoinAsStrings<T>(this IEnumerable<T> items, string delimiter = "") =>
        string.Join(delimiter, items);

    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values) =>
        values.Count() == 1
            ? new[] { values }
            : values.SelectMany(
                v => Permutations(values.Where(x => x?.Equals(v) == false)),
                (v, p) => p.Prepend(v)
            );

    public static List<long> CumulativeSums(this List<long> oriignalNumbers)
    {
        List<long> cumulativeSums = new List<long>();

        long sum = 0;
        foreach (long number in oriignalNumbers)
        {
            sum += number;
            cumulativeSums.Add(sum);
        }

        return cumulativeSums;
    }
}
