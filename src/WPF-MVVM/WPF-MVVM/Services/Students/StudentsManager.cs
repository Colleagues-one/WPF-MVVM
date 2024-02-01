using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models.Decanat;

namespace WPF_MVVM.Services.Students
{
    internal class StudentsManager
    {
        private readonly StudentsRepository _students;
        private readonly GroupsRepository _groups;

        public IEnumerable<Student> Students => _students.GetAll();
        public IEnumerable<Group> Groups => _groups.GetAll();

        public StudentsManager(StudentsRepository students, GroupsRepository groups)
        {
            _students = students;
            _groups = groups;
        }
    }
}
