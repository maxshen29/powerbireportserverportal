using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_CatalogItems_Role_GroupMapConfig : EntityMappingConfiguration<PBI_CatalogItems_Role_Group>
    {
        public override void Map(EntityTypeBuilder<PBI_CatalogItems_Role_Group>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

