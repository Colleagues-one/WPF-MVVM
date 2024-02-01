using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.BaseServices;

namespace WPF_MVVM.Services.Students
{
    internal class StudentsRepository : RepositoryInMemory<Student>
    {
        public StudentsRepository() : base(TestData.Students) { }

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
}
