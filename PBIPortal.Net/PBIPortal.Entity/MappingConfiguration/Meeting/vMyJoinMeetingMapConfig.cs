using PBIPortal.Entity.MappingConfiguration;
using PBIPortal.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DairyStar.Framework.Entity.MappingConfiguration
{
    public class vMyJoinMeetingMapConfig : EntityMappingConfiguration<vMyJoinMeeting>
    {
        public override void Map(EntityTypeBuilder<vMyJoinMeeting> builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
    }
}


