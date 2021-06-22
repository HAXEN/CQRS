using CQRS.Commanding;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Features.TestEntity
{
    public class CreateTestEntityCommand : ICommand
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    [Route("TestEntity/{id}/Create")]
    public class CreateTestEntityHandler : PersistedCommandHandler<CreateTestEntityCommand>
    {
        public override async Task Handle(CreateTestEntityCommand command, CancellationToken token = default)
        {
            
        }

        public override string StreamId(CreateTestEntityCommand command) => $"TestEntity/{command.Id}";
    }
}
