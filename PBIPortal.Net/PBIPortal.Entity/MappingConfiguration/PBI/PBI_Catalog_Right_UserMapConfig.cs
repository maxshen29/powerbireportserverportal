using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_Catalog_Right_UserMapConfig : EntityMappingConfiguration<PBI_Catalog_Right_User>
    {
        public override void Map(EntityTypeBuilder<PBI_Catalog_Right_User>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

