using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.BaseServices;

namespace WPF_MVVM.Services
{
    internal class StudentsRepository : RepositoryInMemory<Student>
    {
        protected override void Update(Student source, Student destination)
        {
            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.Patronymic = source.Patronymic;
            destination.Birthday = source.Birthday;
            destination.Description = source.Description;
            destination.Rating = source.Rating;
        }
    }

    internal class GroupsRepository : RepositoryInMemory<Group>
    {
        protected override void Update(Group source, Group destination)
        {
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Students = source.Students;
        }
    }
}
