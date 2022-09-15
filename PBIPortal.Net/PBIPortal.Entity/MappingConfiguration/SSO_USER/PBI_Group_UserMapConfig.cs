using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_Group_UserMapConfig : EntityMappingConfiguration<PBI_Group_User>
    {
        public override void Map(EntityTypeBuilder<PBI_Group_User>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

