using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace FS.Abp.Trees.MongoDB
{
    public class TreesMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public TreesMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}