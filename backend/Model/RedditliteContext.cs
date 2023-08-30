using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Model;

public partial class RedditliteContext : DbContext
{
    public RedditliteContext()
    {
    }

    public RedditliteContext(DbContextOptions<RedditliteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DataUser> DataUsers { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<ForumXuser> ForumXusers { get; set; }

    public virtual DbSet<ForumXuserRole> ForumXuserRoles { get; set; }

    public virtual DbSet<ImageDatum> ImageData { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleXpermission> RoleXpermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-001YU\\SQLEXPRESS;Initial Catalog=REDDITLITE;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC270C6FEFA0");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.FkPost).HasColumnName("FK_Post");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FkPost)
                .HasConstraintName("FK__Comment__FK_Post__4E88ABD4");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Comment__FK_User__4F7CD00D");
        });

        modelBuilder.Entity<DataUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataUser__3214EC2731D4AAB2");

            entity.ToTable("DataUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Born).HasColumnType("date");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Salt).IsUnicode(false);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.DataUsers)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__DataUser__Photo__3C69FB99");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC2705B377BC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkForumNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FkForum)
                .HasConstraintName("FK__Favorites__FK_Fo__534D60F1");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Favorites__FK_Us__52593CB8");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forum__3214EC27F31B7C97");

            entity.ToTable("Forum");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Owner)
                .HasConstraintName("FK__Forum__Owner__403A8C7D");

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__Forum__Photo__3F466844");
        });

        modelBuilder.Entity<ForumXuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumXUs__3214EC27B30E9E81");

            entity.ToTable("ForumXUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkForumNavigation).WithMany(p => p.ForumXusers)
                .HasForeignKey(d => d.FkForum)
                .HasConstraintName("FK__ForumXUse__FK_Fo__440B1D61");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.ForumXusers)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__ForumXUse__FK_Us__4316F928");
        });

        modelBuilder.Entity<ForumXuserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumXUs__3214EC2761116FC6");

            entity.ToTable("ForumXUserRole");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");
            entity.Property(e => e.FkRole).HasColumnName("FK_Role");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkForumNavigation).WithMany(p => p.ForumXuserRoles)
                .HasForeignKey(d => d.FkForum)
                .HasConstraintName("FK__ForumXUse__FK_Fo__5FB337D6");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.ForumXuserRoles)
                .HasForeignKey(d => d.FkRole)
                .HasConstraintName("FK__ForumXUse__FK_Ro__5EBF139D");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.ForumXuserRoles)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__ForumXUse__FK_Us__5DCAEF64");
        });

        modelBuilder.Entity<ImageDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageDat__3214EC27D00A4CA3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Photo).IsRequired();
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Likes__3214EC273C714542");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkPost).HasColumnName("Fk_Post");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkPostNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.FkPost)
                .HasConstraintName("FK__Likes__Fk_Post__4BAC3F29");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Likes__FK_User__4AB81AF0");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC27662C57F0");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__Location__Photo__398D8EEE");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC27186DA4BA");

            entity.ToTable("Permission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC27F0EFB781");

            entity.ToTable("Post");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.FkForum)
                .HasConstraintName("FK__Post__FK_Forum__47DBAE45");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Post__FK_User__46E78A0C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC27A486B8D4");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleXpermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleXPer__3214EC2728F31559");

            entity.ToTable("RoleXPermission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkPermission).HasColumnName("FK_Permission");
            entity.Property(e => e.FkRole).HasColumnName("FK_Role");

            entity.HasOne(d => d.FkPermissionNavigation).WithMany(p => p.RoleXpermissions)
                .HasForeignKey(d => d.FkPermission)
                .HasConstraintName("FK__RoleXPerm__FK_Pe__5AEE82B9");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.RoleXpermissions)
                .HasForeignKey(d => d.FkRole)
                .HasConstraintName("FK__RoleXPerm__FK_Ro__59FA5E80");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
