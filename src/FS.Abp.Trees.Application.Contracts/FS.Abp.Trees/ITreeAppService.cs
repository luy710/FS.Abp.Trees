using System;
using System.Threading.Tasks;
using FS.Abp.Trees.Dtos;
using Volo.Abp.Application.Dtos;

namespace FS.Abp.Trees
{
    public interface ITreeAppService<TGetOutputDto, TGetListOutputDto, TGetListInput, TCreateInput, TUpdateInput, TMoveInput>
        where TGetOutputDto : ITreeDto
        where TGetListOutputDto: ITreeDto
        where TCreateInput : ICreateInput
        where TUpdateInput : IUpdateInput
        where TMoveInput : IMoveInput
    {
        Task<TGetOutputDto> CreateAsync(TCreateInput input);
        Task DeleteAsync(Guid id);
        Task<TGetOutputDto> GetAsync(Guid id);
        Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input);
        Task<TGetOutputDto> UpdateAsync(Guid id, TUpdateInput input);
        Task<TGetOutputDto> MoveAsync(TMoveInput input);
    }
}