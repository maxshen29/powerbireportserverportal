using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class SellOrderListMapConfig : EntityMappingConfiguration<SellOrderList>
    {
        public override void Map(EntityTypeBuilder<SellOrderList>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

