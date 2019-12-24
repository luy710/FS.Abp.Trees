using System;
using System.ComponentModel.DataAnnotations;

namespace FS.Abp.Trees.Dtos
{
    public interface IMoveInput
    {
        Guid Id { get; set; }

        Guid? NewParentId { get; set; }
    }
}
