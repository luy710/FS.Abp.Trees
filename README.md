# FS.Abp.Trees  
This module is develop by [Abp](https://github.com/abpframework/abp)  
### Documentation  
1.Client Domain module add an Entity and must implement [ITree<TEntity>](https://github.com/yinchang0626/FS.Abp.Trees/blob/master/src/FS.Abp.Trees.Domain/FS.Abp.Trees/ITree.cs)  
```csharp
    public partial class Unit : ITree<Unit>,
            Volo.Abp.Domain.Entities.Auditing.FullAuditedAggregateRoot<Guid>,
            Volo.Abp.MultiTenancy.IMultiTenant
    {
      ...
    }
```
2.Clent EntityFrameworkCoreModule module should config for [ITreeRepository<TEntity>](https://github.com/yinchang0626/FS.Abp.Trees/blob/master/src/FS.Abp.Trees.Domain/FS.Abp.Trees/ITreeRepository.cs) by  
```csharp
    [DependsOn(
        typeof(ClentDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(FS.Abp.Trees.EntityFrameworkCore.TreesEntityFrameworkCoreModule)
    )]
    public class ClentEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ...
            //auto add all ITreeRepository<TEntity> which entity implement `ITree`
            context.Services.AddTreeRepository<ClientDbContext>();
        }
    }
```
3.Client Application.Contracts module add some dots class 
```csharp
public partial class UnitDto :ITreeDto,IEntityDto<Guid> {...}
public partial class UnitWithDetailDto :ITreeDto,IEntityDto<Guid> {...}
public partial class GetListInput :PagedAndSortedResultRequestDto, FS.Abp.Trees.IGetListInput {...}
public partial class CreateInput :FS.Abp.Trees.ICreateInput {...}
public partial class UpdateInput :FS.Abp.Trees.IUpdateInput {...}
public partial class MoveInput :FS.Abp.Trees.IMoveInput {...}
```
4.Client Application module add an ApplicationService
```csharp
    public class EpsyCoreTreeAppService : FS.Abp.Trees.TreeAppService<
        FS.Client.Core.Unit,
        FS.Client.Core.Dtos.UnitWithDetailDto,                                                                                           
        FS.Client.Core.Dtos.UnitDto,
        FS.Client.Core.Dtos.GetListInput,
        FS.Client.Core.Dtos.CreateInput,
        FS.Client.Core.Dtos.UpdateInput,
        FS.Client.Core.Dtos.MoveInput>
    {
    }
```
5.HttpApi.Host module config auto gen ApiController
```csharp
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ClientApplicationModule).Assembly, action => action.RootPath = "Client");
            });
```
6.Get a WebApi that has Crud and Tree(Move) function
