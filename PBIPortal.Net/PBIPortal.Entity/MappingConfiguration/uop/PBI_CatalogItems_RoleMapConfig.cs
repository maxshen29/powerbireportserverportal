using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_CatalogItems_RoleMapConfig : EntityMappingConfiguration<PBI_CatalogItems_Role>
    {
        public override void Map(EntityTypeBuilder<PBI_CatalogItems_Role>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

