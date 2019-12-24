using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FS.Abp.Trees.EntityFrameworkCore
{
    public class TreesModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public TreesModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}