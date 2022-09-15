using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class V_GetALLUserRightbyRptidMapConfig : EntityMappingConfiguration<V_GetALLUserRightbyRptid>
    {
        public override void Map(EntityTypeBuilder<V_GetALLUserRightbyRptid>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

