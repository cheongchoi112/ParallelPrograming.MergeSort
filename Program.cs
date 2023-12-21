namespace ParallelPrograming.FinalProject
{
    // Author: JOE CHOI, SONALI GUPTA
    internal class Program
    {
        static void Main(string[] args)
        {
            long totalExampleSortElapsedTime = 0, totalSequenceSortElapsedTime = 0, total2ParallelSortElapsedTime = 0, total4ParallelSortElapsedTime = 0, total8ParallelSortElapsedTime = 0, total16ParallelSortElapsedTime = 0;
            int numberOfSamples = 5;

            SortResult exampleSortResult = new SortResult();
            SortResult sequenceSortResult = new SortResult();
            SortResult parallelSortResult = new SortResult();
            SortResult reusableSortResult = new SortResult();

            for (int i = 0; i < numberOfSamples; i++)
            {
                exampleSortResult = DotNetSortExample.RunSort();
                totalExampleSortElapsedTime += exampleSortResult.ElapsedTime;
            }

            for (int i = 0; i < numberOfSamples; i++)
            {
                sequenceSortResult = MergeSort.SequenceSort();
                totalSequenceSortElapsedTime += exampleSortResult.ElapsedTime;

            }

            for (int i = 0; i < numberOfSamples; i++)
            {
                parallelSortResult = MergeSort.ParallelSort(2);
                total2ParallelSortElapsedTime += exampleSortResult.ElapsedTime;

            }

            for (int i = 0; i < numberOfSamples; i++)
            {
                reusableSortResult = MergeSort.ParallelSort(4);
                total4ParallelSortElapsedTime += reusableSortResult.ElapsedTime;

            }

            for (int i = 0; i < numberOfSamples; i++)
            {
                reusableSortResult = MergeSort.ParallelSort(8);
                total8ParallelSortElapsedTime += reusableSortResult.ElapsedTime;
            }

            for (int i = 0; i < numberOfSamples; i++)
            {
                reusableSortResult = MergeSort.ParallelSort(16);
                total16ParallelSortElapsedTime += reusableSortResult.ElapsedTime;
            }

            Console.WriteLine("");

            Console.WriteLine("-----------------------------------  Report  -----------------------------------");
            Console.WriteLine("Averge time to run with sample size {0}:", numberOfSamples);
            Console.WriteLine("Example Code: {0} milliseconds", totalExampleSortElapsedTime / numberOfSamples);
            Console.WriteLine("Sequence Code: {0} milliseconds", totalSequenceSortElapsedTime / numberOfSamples);
            Console.WriteLine("Parallel Code (2 threads): {0} milliseconds", total2ParallelSortElapsedTime / numberOfSamples);
            Console.WriteLine("Parallel Code (4 threads): {0} milliseconds", total4ParallelSortElapsedTime / numberOfSamples);
            Console.WriteLine("Parallel Code (6 threads): {0} milliseconds", total8ParallelSortElapsedTime / numberOfSamples);
            Console.WriteLine("Parallel Code (16 threads): {0} milliseconds", total16ParallelSortElapsedTime / numberOfSamples);

            Console.WriteLine("");

            Console.WriteLine("-----------------------  Verifying Merge Sort Algorithm  -----------------------");

            bool isEqual = Helpers.AreListsEqual(sequenceSortResult.People, exampleSortResult.People);
            Console.WriteLine("Result for Sequence code vs Example code are the same? {0}", isEqual);


            isEqual = Helpers.AreListsEqual(parallelSortResult.People, exampleSortResult.People);
            Console.WriteLine("Result Parallel code vs Example code are the same? {0}", isEqual);

            Console.WriteLine("");

            Console.WriteLine("Press Return to exit.");
            Console.ReadLine();
        }
    }
}