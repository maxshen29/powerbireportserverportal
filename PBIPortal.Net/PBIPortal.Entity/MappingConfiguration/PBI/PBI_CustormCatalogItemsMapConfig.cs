using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_CustormCatalogItemsMapConfig : EntityMappingConfiguration<PBI_CustormCatalogItems>
    {
        public override void Map(EntityTypeBuilder<PBI_CustormCatalogItems>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

