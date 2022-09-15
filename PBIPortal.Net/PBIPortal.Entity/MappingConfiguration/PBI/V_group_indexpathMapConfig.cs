using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class V_group_indexpathMapConfig : EntityMappingConfiguration<V_group_indexpath>
    {
        public override void Map(EntityTypeBuilder<V_group_indexpath>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

