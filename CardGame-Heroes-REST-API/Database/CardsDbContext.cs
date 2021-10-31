using CardGame_Heroes_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CardGame_Heroes_REST_API.Database
{
    public partial class CardsDbContext : DbContext
    {
        public CardsDbContext()
        {
        }

        public CardsDbContext(DbContextOptions<CardsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CardsClass> CardsClasses { get; set; } = null!;
        public virtual DbSet<CardsElement> CardsElements { get; set; } = null!;
        public virtual DbSet<CardsImage> CardsImages { get; set; } = null!;
        public virtual DbSet<CardsRareness> CardsRarenesses { get; set; } = null!;
        public virtual DbSet<CardsSet> CardsSets { get; set; } = null!;
        public virtual DbSet<CardsType> CardsTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("cards");

                entity.HasIndex(e => e.Class, "class");

                entity.HasIndex(e => e.Element, "element");

                entity.HasIndex(e => e.Name, "name");

                entity.HasIndex(e => e.Rareness, "rareness");

                entity.HasIndex(e => e.SetName, "set_name");

                entity.HasIndex(e => e.Type, "type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Инкрементное значения карты в базе");

                entity.Property(e => e.Attack)
                    .HasColumnName("attack")
                    .HasComment("Показатель атаки карты (-1 если Х)");

                entity.Property(e => e.Class)
                    .HasMaxLength(40)
                    .HasColumnName("class")
                    .HasComment("Название класса")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasComment("Стоимость карты в монетах\r\n(-1 если Х)");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description")
                    .HasComment("Функциональное описание карты")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Element)
                    .HasMaxLength(60)
                    .HasColumnName("element")
                    .HasComment("Название стихии")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.FlavorText)
                    .HasMaxLength(200)
                    .HasColumnName("flavor_text")
                    .HasComment("Сюжетное описание карты, не влияющиее на игровой процесс")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Health)
                    .HasColumnName("health")
                    .HasComment("Показатель здоровья карты (-1 если Х)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasComment("Название карты")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.NumberInSet)
                    .HasColumnName("number_in_set")
                    .HasComment("Номер карты в своём выпуске");

                entity.Property(e => e.Rareness)
                    .HasMaxLength(20)
                    .HasColumnName("rareness")
                    .HasComment("Называние редкости")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.SetName)
                    .HasMaxLength(40)
                    .HasColumnName("set_name")
                    .HasComment("Название выпуска")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Type)
                    .HasMaxLength(40)
                    .HasColumnName("type")
                    .HasComment("Название типа карты")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.ClassNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Class)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("cards_ibfk_5");

                entity.HasOne(d => d.ElementNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Element)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("cards_ibfk_3");

                entity.HasOne(d => d.RarenessNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Rareness)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("cards_ibfk_4");

                entity.HasOne(d => d.SetNameNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.SetName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("cards_ibfk_2");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("cards_ibfk_1");
            });

            modelBuilder.Entity<CardsClass>(entity =>
            {
                entity.HasKey(e => e.ClassName)
                    .HasName("PRIMARY");

                entity.ToTable("cards_classes");

                entity.HasComment("Таблица классов карт")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(40)
                    .HasColumnName("class_name")
                    .HasComment("Название класса");
            });

            modelBuilder.Entity<CardsElement>(entity =>
            {
                entity.HasKey(e => e.ElementName)
                    .HasName("PRIMARY");

                entity.ToTable("cards_elements");

                entity.HasComment("Таблица стихий карт")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ElementName)
                    .HasMaxLength(40)
                    .HasColumnName("element_name")
                    .HasComment("Название стихии");
            });

            modelBuilder.Entity<CardsImage>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("PRIMARY");

                entity.ToTable("cards_images");

                entity.HasComment("Таблица внешних ссылок на изображения");

                entity.HasIndex(e => e.CardName, "card_name");

                entity.Property(e => e.CardId)
                    .ValueGeneratedNever()
                    .HasColumnName("card_id");

                entity.Property(e => e.ArtworkUrl)
                    .HasMaxLength(500)
                    .HasColumnName("artwork_url")
                    .HasComment("Внешняя ссылка на изображение персонажа внутри карты");

                entity.Property(e => e.CardImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("card_image_url")
                    .HasComment("Внешняя ссылка на целое изображение карты");

                entity.Property(e => e.CardName)
                    .HasMaxLength(50)
                    .HasColumnName("card_name")
                    .HasComment("Название карты")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.Card)
                    .WithOne(p => p.CardsImage)
                    .HasForeignKey<CardsImage>(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cards_images_ibfk_1");
            });

            modelBuilder.Entity<CardsRareness>(entity =>
            {
                entity.HasKey(e => e.RarenessName)
                    .HasName("PRIMARY");

                entity.ToTable("cards_rarenesses");

                entity.HasComment("Таблица редкости карт")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.RarenessName)
                    .HasMaxLength(20)
                    .HasColumnName("rareness_name")
                    .HasComment("Назавание редкости карты");
            });

            modelBuilder.Entity<CardsSet>(entity =>
            {
                entity.HasKey(e => e.SetName)
                    .HasName("PRIMARY");

                entity.ToTable("cards_sets");

                entity.HasComment("Таблица выпусков (игровых наборов) карт")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.SetName)
                    .HasMaxLength(40)
                    .HasColumnName("set_name")
                    .HasComment("Название выпуска");

                entity.Property(e => e.CardsAmount)
                    .HasColumnName("cards_amount")
                    .HasComment("Колличество карт в выпуске");
            });

            modelBuilder.Entity<CardsType>(entity =>
            {
                entity.HasKey(e => e.TypeName)
                    .HasName("PRIMARY");

                entity.ToTable("cards_types");

                entity.HasComment("Таблица типов карт")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(40)
                    .HasColumnName("type_name")
                    .HasComment("Название типа карты");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
