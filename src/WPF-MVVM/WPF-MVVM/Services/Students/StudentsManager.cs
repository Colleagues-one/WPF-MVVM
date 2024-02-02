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

        public void Update(Student student) => _students.Update(student.Id, student);

        public bool Add(Student student, int groupId)
        {
            if(student is null) throw new ArgumentNullException(nameof(student));
            if(groupId <=0) throw new ArgumentOutOfRangeException(nameof(groupId));

            var group = _groups.Get(groupId);
            if (group == null)
            {
                group = new Group() { Id = groupId, Name = $"New Group{groupId}", };
            }
            group.Students.Add(student);
            _students.Add(student);
            return true;
        }
    }
}
