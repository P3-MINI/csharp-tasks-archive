using System.Numerics;

namespace EN_Lab09
{
    internal struct Grade<T> where T : INumber<T>
    {
        public T Value { get; set; }

        public Grade(T value) { this.Value = value; }

        public static implicit operator T(Grade<T> grade) => grade.Value;
    }

    internal struct Presence
    {
        public Grade<int> Grade { get; set; }
    }

    internal struct Quiz
    {
        public Grade<float> Grade { get; set; }
    }

    internal struct Task
    {
        public Grade<float> Grade { get; set; }
    }

    internal struct Laboratory
    {
        public Presence Presence { get; init; }
        public Task Task { get; init; }
        public Quiz? Quiz { get; init; }

        public Laboratory(Presence presence, Task task, Quiz? quiz = null)
        {
            this.Presence = presence;
            this.Task = task;
            this.Quiz = quiz;
        }
    }

    internal class SubjectInfo
    {
        public SortedDictionary<DateTime, Laboratory> Info { get; } = new SortedDictionary<DateTime, Laboratory>();

        public List<T> GetPresences<T>() where T : INumber<T>
        {
            return this.Info.Values.Select(x => T.CreateChecked(x.Presence.Grade.Value)).ToList();
        }

        public List<T> GetQuizzes<T>() where T : INumber<T>
        {
            return this.Info.Values.Select(x => x.Quiz.HasValue ? T.CreateChecked(x.Quiz.Value.Grade.Value) : T.Zero).ToList();
        }

        public List<T> GetTasks<T>() where T : INumber<T>
        {
            return this.Info.Values.Select(x => T.CreateChecked(x.Task.Grade.Value)).ToList();
        }
    } 
}
