namespace ParallelPrograming.FinalProject
{
    public class FileReader
    {
        public List<Person> ReadNames()
        {
            List<Person> people = new List<Person>();

            // populate the list of names from a file
            using (StreamReader sr = new StreamReader("random names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string[] s = sr.ReadLine().Split(' ');
                    people.Add(new Person(s[0], s[1]));
                }
            }

            return people;

        }
    }
}
