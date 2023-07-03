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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-0013D\\SQLEXPRESS;Initial Catalog=REDDITLITE;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC27DC7C5BF1");

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
                .HasConstraintName("FK__Comment__FK_Post__49C3F6B7");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Comment__FK_User__4AB81AF0");
        });

        modelBuilder.Entity<DataUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataUser__3214EC2735919154");

            entity.ToTable("DataUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.DataUsers)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__DataUser__Photo__3B75D760");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC27195046DE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkForumNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FkForum)
                .HasConstraintName("FK__Favorites__FK_Fo__70DDC3D8");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Favorites__FK_Us__6FE99F9F");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forum__3214EC2759DE4420");

            entity.ToTable("Forum");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Owner)
                .HasConstraintName("FK__Forum__Owner__3F466844");

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__Forum__Photo__3E52440B");
        });

        modelBuilder.Entity<ForumXuser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ForumXUser");

            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkForumNavigation).WithMany()
                .HasForeignKey(d => d.FkForum)
                .HasConstraintName("FK__ForumXUse__FK_Fo__5DCAEF64");

            entity.HasOne(d => d.FkUserNavigation).WithMany()
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__ForumXUse__FK_Us__5CD6CB2B");
        });

        modelBuilder.Entity<ForumXuserRole>(entity =>
        {
            entity.HasKey(e => new { e.FkUser, e.FkRole, e.FkForum }).HasName("PK__ForumXUs__91E03C271FDBEBB0");

            entity.ToTable("ForumXUserRole");

            entity.Property(e => e.FkUser).HasColumnName("FK_User");
            entity.Property(e => e.FkRole).HasColumnName("FK_Role");
            entity.Property(e => e.FkForum).HasColumnName("FK_Forum");

            entity.HasOne(d => d.FkForumNavigation).WithMany(p => p.ForumXuserRoles)
                .HasForeignKey(d => d.FkForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumXUse__FK_Fo__5AEE82B9");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.ForumXuserRoles)
                .HasForeignKey(d => d.FkRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumXUse__FK_Ro__59FA5E80");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.ForumXuserRoles)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumXUse__FK_Us__59063A47");
        });

        modelBuilder.Entity<ImageDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageDat__3214EC27622628B9");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Likes__3214EC27442E5656");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkPost).HasColumnName("Fk_Post");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkPostNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.FkPost)
                .HasConstraintName("FK__Likes__Fk_Post__46E78A0C");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Likes__FK_User__45F365D3");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC270829D170");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__Location__Photo__38996AB5");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC27D51BC579");

            entity.ToTable("Permission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC27E0D2BB6B");

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
                .HasConstraintName("FK__Post__FK_Forum__4316F928");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Post__FK_User__4222D4EF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC27B6B13C74");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.FkPermissions).WithMany(p => p.FkRoles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleXpermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("FkPermission")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleXPerm__FK_Pe__5629CD9C"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("FkRole")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleXPerm__FK_Ro__5535A963"),
                    j =>
                    {
                        j.HasKey("FkRole", "FkPermission").HasName("PK__RoleXPer__6BEDD7FA4547DFDB");
                        j.ToTable("RoleXPermission");
                        j.IndexerProperty<int>("FkRole").HasColumnName("FK_Role");
                        j.IndexerProperty<int>("FkPermission").HasColumnName("FK_Permission");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
