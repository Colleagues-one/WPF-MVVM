﻿using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.BaseServices;

namespace WPF_MVVM.Services.Students;

internal class GroupsRepository : RepositoryInMemory<Group>
{
    public GroupsRepository() : base(TestData.Groups) { }
    protected override void Update(Group source, Group destination)
    {
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Students = source.Students;
    }

    public Group Get(int id) => GetAll().FirstOrDefault(x => x.Id == id);

    public Group Get(string name) => GetAll().FirstOrDefault(x => x.Name == name);
}
