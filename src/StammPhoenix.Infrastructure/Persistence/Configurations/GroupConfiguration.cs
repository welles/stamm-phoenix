using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Infrastructure.Persistence.Configurations;

public sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder
            .HasMany<Leader>(x => x.Members)
            .WithMany(x => x.Groups)
            .UsingEntity(
                "group_members",
                b =>
                {
                    b.Property("GroupsId").HasColumnName("group_id");
                    b.Property("MembersId").HasColumnName("leader_id");
                }
            );
    }
}
