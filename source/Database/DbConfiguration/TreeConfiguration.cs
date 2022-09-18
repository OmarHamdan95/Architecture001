 using Architecture.Domain;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;

 namespace Architecture.Database;

 public class TreeConfiguration: IEntityTypeConfiguration<Tree>
 {
     public void Configure(EntityTypeBuilder<Tree> entityConfigration)
     {
         entityConfigration.HasMany(t => t.FlateParent).WithOne(t => t.Tree);
     }
 }
