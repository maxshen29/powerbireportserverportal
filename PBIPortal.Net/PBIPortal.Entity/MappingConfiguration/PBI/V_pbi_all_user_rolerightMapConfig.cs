using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class V_pbi_all_user_rolerightMapConfig : EntityMappingConfiguration<V_pbi_all_user_roleright>
    {
        public override void Map(EntityTypeBuilder<V_pbi_all_user_roleright>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

