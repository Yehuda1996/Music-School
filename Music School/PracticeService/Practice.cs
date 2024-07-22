using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_School.PracticeService
{
    static class Practice
    {
        public static Func<List<string>, bool> IsStartsWithA = (list) =>
        list.Any(x => x.StartsWith("a"));

        public static Func<List<string>, bool> IsStringEmpty = (list) => 
        list.Any(string.IsNullOrEmpty);

        public static Func<List<string>, bool> AllContainsA = list =>
        list.All(x => x.Contains("a"));

        public static Func<List<string>, List<string>> ToUpperCase = list => 
        list.Select(x => x.ToUpper()).ToList();

        public static Func<List<string>, List<string>> ToUpperCaseQuery = list =>
        (from str in list select str.ToUpper()).ToList();

        public static Func<List<string>, List<string>> LenghtGreaterThan3 = list =>
        list.Where(x => x.Length > 3).ToList();

        public static Func<List<string>, List<string>> LengthGreaterThan3Query = list =>
        (from str in list
         where str.Length > 3 select str).ToList();

        public static Func<List<string>, string> StringifyList = (list) =>
        list.Aggregate(string.Empty, (acc, n) => $"{acc} {n}");

        public static Func<List<string>, int> SumLengths = list =>
        list.Aggregate(0, (acc, n) => acc + n.Length);

        public static Func<List<string>, List<string>> WhereAbove3 = (list) =>
        list.Aggregate(new List<string>(), (acc, n) => n.Length > 3 ?[..acc, n] : acc);

        public static Func<List<string>, List<int>> SelectLenghts = (list) =>
        list.Aggregate(new List<int>(), (acc, n) => [.. acc, n.Length]);
    }
}
