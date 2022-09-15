using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_CatalogItemsMapConfig : EntityMappingConfiguration<PBI_CatalogItems>
    {
        public override void Map(EntityTypeBuilder<PBI_CatalogItems>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

