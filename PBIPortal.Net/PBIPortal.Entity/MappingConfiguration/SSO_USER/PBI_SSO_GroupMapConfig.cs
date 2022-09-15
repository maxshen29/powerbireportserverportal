using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_SSO_GroupMapConfig : EntityMappingConfiguration<PBI_SSO_Group>
    {
        public override void Map(EntityTypeBuilder<PBI_SSO_Group>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

