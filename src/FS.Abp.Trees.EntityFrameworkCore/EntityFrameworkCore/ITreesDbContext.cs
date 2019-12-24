﻿using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FS.Abp.Trees.EntityFrameworkCore
{
    [ConnectionStringName(TreesDbProperties.ConnectionStringName)]
    public interface ITreesDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}