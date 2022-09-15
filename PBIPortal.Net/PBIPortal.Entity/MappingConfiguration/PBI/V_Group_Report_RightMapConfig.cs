using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class V_Group_Report_RightMapConfig : EntityMappingConfiguration<V_Group_Report_Right>
    {
        public override void Map(EntityTypeBuilder<V_Group_Report_Right>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

