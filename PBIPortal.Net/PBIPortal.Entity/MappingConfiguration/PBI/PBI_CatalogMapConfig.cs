using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_CatalogMapConfig : EntityMappingConfiguration<PBI_Catalog>
    {
        public override void Map(EntityTypeBuilder<PBI_Catalog>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

