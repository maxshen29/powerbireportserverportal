using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PBIPortal.Entity.MappingConfiguration
{
    public class Sys_UserMapConfig : EntityMappingConfiguration<Sys_User>
    {
        public override void Map(EntityTypeBuilder<Sys_User>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

