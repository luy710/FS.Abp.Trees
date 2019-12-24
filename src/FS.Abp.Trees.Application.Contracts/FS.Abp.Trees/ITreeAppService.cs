using System;
using System.Threading.Tasks;
using FS.Abp.Trees.Dtos;

namespace FS.Abp.Trees
{
    public interface ITreeAppService<TGetOutputDto, TCreateInput, TUpdateInput, TMoveInput>
        where TGetOutputDto : ITreeDto
        where TCreateInput : ICreateInput
        where TUpdateInput : IUpdateInput
        where TMoveInput : IMoveInput
    {
        Task<TGetOutputDto> CreateAsync(TCreateInput input);
        Task DeleteAsync(Guid id);
        Task<TGetOutputDto> MoveAsync(TMoveInput input);
        Task<TGetOutputDto> UpdateAsync(Guid id, TUpdateInput input);
    }
}