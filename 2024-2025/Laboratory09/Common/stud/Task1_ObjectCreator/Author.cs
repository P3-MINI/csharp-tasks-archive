namespace Task1_ObjectCreator
{
    public class Author
    {
        public Author(string name, string surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        public int Age {get; set;}

        // Part 4
        // public DateTime BirthDate { get; set; }

        public override string? ToString()
        {
            return $"Author {Name} {Surname} (Age {Age})";
        }
    }
}
