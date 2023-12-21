using System.Diagnostics;

namespace ParallelPrograming.FinalProject
{
    public class DotNetSortExample
    {
        public static SortResult RunSort()
        {
            FileReader reader = new FileReader();
            List<Person> people = reader.ReadNames();

            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Person> sortedNames = people.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
            stopwatch.Stop();

            Console.WriteLine("Example code took {0} milliseconds to execute", stopwatch.ElapsedMilliseconds);

            return new SortResult() { People = sortedNames , ElapsedTime = stopwatch.ElapsedMilliseconds };
        }
    }
}
