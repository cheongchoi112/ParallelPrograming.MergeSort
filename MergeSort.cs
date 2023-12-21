using System.Diagnostics;

namespace ParallelPrograming.FinalProject
{
    public static class MergeSort
    {
        public static SortResult SequenceSort()
        {
            FileReader reader = new FileReader();
            List<Person> people = reader.ReadNames(); 

            Stopwatch stopwatch = Stopwatch.StartNew();
            RecursiveMergeSort(people, 0, people.Count - 1);
            stopwatch.Stop();
            Console.WriteLine("Sequence code took {0} milliseconds to execute", stopwatch.ElapsedMilliseconds); // Prints the duration

            return new SortResult() { People = people, ElapsedTime = stopwatch.ElapsedMilliseconds };
        }

        private static void RecursiveMergeSort(List<Person> people, int left, int right)
        {
            if (left < right)
            {
                // Finds the midpoint of the segment
                int middle = left + (right - left) / 2; 

                // Recursively sort the left and right halves
                RecursiveMergeSort(people, left, middle);
                RecursiveMergeSort(people, middle + 1, right);

                // Merge the sorted halves
                Merge(people, left, middle, right);
            }
        }

        public static SortResult ParallelSort(int maxDegreeOfParallelism)
        {
            FileReader reader = new FileReader();
            List<Person> people = reader.ReadNames();

            Stopwatch stopwatch = Stopwatch.StartNew(); 
            ParallelRecursiveMergeSort(people, 0, people.Count - 1, maxDegreeOfParallelism); 
            stopwatch.Stop(); 
            Console.WriteLine("Parallel code with {0} maxDegreeOfParallelism took {1} milliseconds to execute", maxDegreeOfParallelism, stopwatch.ElapsedMilliseconds); // Prints the duration

            return new SortResult() { People = people, ElapsedTime = stopwatch.ElapsedMilliseconds };
        }

        private static void ParallelRecursiveMergeSort(List<Person> people, int left, int right, int maxDegreeOfParallelism)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                if (maxDegreeOfParallelism > 1) // Checks if more parallelism is possible
                {
                    var leftTask = Task.Run(() => ParallelRecursiveMergeSort(people, left, middle, maxDegreeOfParallelism / 2));
                    var rightTask = Task.Run(() => ParallelRecursiveMergeSort(people, middle + 1, right, maxDegreeOfParallelism / 2));

                    Task.WaitAll(leftTask, rightTask);
                }
                else
                {
                    // do a sequential sort when it reaches the max of set maxDegreeOfParallelism
                    ParallelRecursiveMergeSort(people, left, middle, maxDegreeOfParallelism);
                    ParallelRecursiveMergeSort(people, middle + 1, right, maxDegreeOfParallelism);
                }

                Merge(people, left, middle, right); // Merge the sorted halves
            }
        }

        // Merges two sorted segments of the list
        private static void Merge(List<Person> people, int left, int middle, int right)
        {
            // Max indices for left and right lists
            int n1 = middle - left + 1;
            int n2 = right - middle;

            // Temporary lists to hold left and right lists
            var L = new List<Person>();
            var R = new List<Person>();

            // Copy the segments into temporary lists
            for (int i = 0; i < n1; i++)
                L.Add(people[left + i]);
            for (int j = 0; j < n2; j++)
                R.Add(people[middle + 1 + j]);

            int k = left; // k to keep tract of the current index of the main list
            int l = 0, r = 0; // indices for the temporary lists

            // while loop until either list hits the end
            while (l < n1 && r < n2)
            {
                // compare two names and copy the smaller one to main list
                if (Helpers.ComparePeople(L[l], R[r]) <= 0)
                {
                    people[k] = L[l];
                    l++;
                }
                else
                {
                    people[k] = R[r];
                    r++;
                }
                k++;
            }

            // Copy any remaining elements of L and R back into the main list
            while (l < n1)
            {
                people[k] = L[l];
                l++;
                k++;
            }

            while (r < n2)
            {
                people[k] = R[r];
                r++;
                k++;
            }
        }

    }
}
