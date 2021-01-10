using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day21
{
    internal class Day21
    {
        private readonly HashSet<string> _allergens;
        private readonly HashSet<string> _ingredients;
        private readonly List<(HashSet<string>, HashSet<string>)> _input;
        private readonly Dictionary<string, HashSet<string>> _allergensToIngredients;

        public Day21()
        {
            _allergens = new HashSet<string>();
            _ingredients = new HashSet<string>();
            _input = ReadData();
            _allergensToIngredients = MapAllergenToIngredient();
        }

        private List<(HashSet<string>, HashSet<string>)> ReadData()
        {
            var input = new List<(HashSet<string>, HashSet<string>)>();
            foreach (var line in File.ReadAllLines("Day21.txt"))
            {
                var split = line.Split(" (");
                var ingredients = split[0].Split(' ').ToHashSet();
                var allergens = split[1][9..^1].Split(", ").ToHashSet();
                input.Add((ingredients, allergens));
                _allergens.UnionWith(allergens);
                _ingredients.UnionWith(ingredients);
            }

            return input;
        }

        public long NumberOfAppearancesForIngredientsWithoutAllergens() => _input.SelectMany(x => x.Item1).Count(x => !_allergensToIngredients.Values.Any(y => y.Contains(x)));


        public string GetCanonicalDangerousIngredientsList() => string.Join(",",
            _allergens.OrderBy(x => x).Select(x => _allergensToIngredients[x].Single()));

        private Dictionary<string, HashSet<string>> MapAllergenToIngredient()
        {
            var allergensToIngredients = _allergens.ToDictionary(allergen => allergen,
                allergen => _input.Where(entry => entry.Item2.Contains(allergen))
                    .Aggregate(_ingredients as IEnumerable<string>, (result, entry) => result.Intersect(entry.Item1))
                    .ToHashSet());
            while (allergensToIngredients.Values.Any(ingredients => ingredients.Count > 1))
            {
                foreach (var allergen in _allergens)
                {
                    var candidates = allergensToIngredients[allergen];
                    if (candidates.Count != 1) continue;
                    foreach (var allergenT in _allergens.Where(allergenT => allergen != allergenT))
                        allergensToIngredients[allergenT].Remove(candidates.Single());
                }
            }
            return allergensToIngredients;
        }
    }
}