using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPF_MVVM.Models.Decanat;

namespace WPF_MVVM.Services.Students
{
    internal static class TestData
    {
        public static Group[] Groups { get; } = Enumerable
            .Range(1, 10)
            .Select(i => new Group { Name = $"Group {i}" })
            .ToArray();

        public static Student[] Students { get; } = CreateStudents(Groups);

        private static Student[] CreateStudents(Group[] groups)
        {
            var rnd = new Random();
            var index = 1;
            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    group.Students.Add(new Student
                    {
                        Name = $"Name{index}",
                        Surname = $"Surname{index}",
                        Patronymic = $"Patronymic{index++}",
                        Birthday = DateTime.Now.Subtract(TimeSpan.FromDays(300 * rnd.Next(19,30))),
                        Rating = rnd.NextDouble() * 100
                    });
                }
            }

            return groups.SelectMany(g => g.Students).ToArray();
        }
    }
}
