using WPF_MVVM.Models.Decanat;
using WPF_MVVM.Services.BaseServices;

namespace WPF_MVVM.Services.Students;

internal class GroupsRepository : RepositoryInMemory<Group>
{
    protected override void Update(Group source, Group destination)
    {
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Students = source.Students;
    }
}