using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class PBI_SSO_USERMapConfig : EntityMappingConfiguration<PBI_SSO_USER>
    {
        public override void Map(EntityTypeBuilder<PBI_SSO_USER>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

