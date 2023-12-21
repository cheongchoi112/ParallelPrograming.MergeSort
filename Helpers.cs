namespace ParallelPrograming.FinalProject
{
    internal class Helpers
    {
        public static bool AreListsEqual(List<Person> list1, List<Person> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].LastName != list2[i].LastName || list1[i].FirstName != list2[i].FirstName)
                {
                    return false;
                }
            }

            return true;
        }

        public static int ComparePeople(Person a, Person b)
        {
            int lastNameComparison = string.Compare(a.LastName, b.LastName);
            if (lastNameComparison == 0)
            {
                return string.Compare(a.FirstName, b.FirstName);
            }
            return lastNameComparison;
        }
    }
}
