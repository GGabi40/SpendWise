// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using SpendWise.Core.Entities;

// namespace SpendWise.Infrastructure.Data.Configurations
// {
//     public class NoteConfiguration : IEntityTypeConfiguration <Note>
//     {
//         public void Configure(EntityTypeBuilder<Note> builder)
//         {
//             builder.ToTable("Notes");

//             builder.HasKey(n => n.iId);

//             builder.Property(n => n.Title)
//                 .IsRequired()
//                 .HasMaxLength(1000);

//             builder.Property(n => n.IsPinned)
//                 .HasDefaultValue(false);

//             builder.Property(n => n.CreatedAt)
//                 .HasDefaultValueSql("CURRENT_TIMESTAMP");

//             builder.HasOne(n => n.User)
//                 .WithMany(n => u.Notes)
//                 .HasForeingKey(n => n.UserId)
//                 .OnDelete(DeleteBehavior.Cascade);
//         }
//     }
// }