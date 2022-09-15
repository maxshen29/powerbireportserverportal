using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_Group_RightMapConfig : EntityMappingConfiguration<PBI_Group_Right>
    {
        public override void Map(EntityTypeBuilder<PBI_Group_Right>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

