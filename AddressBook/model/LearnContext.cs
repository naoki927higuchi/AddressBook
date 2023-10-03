using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.model;

public partial class LearnContext : DbContext
{
    public LearnContext()
    {
    }

    public LearnContext(DbContextOptions<LearnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstAddress> MstAddresses { get; set; }

    public virtual DbSet<MstAreaCode> MstAreaCodes { get; set; }

    public virtual DbSet<MstCity> MstCities { get; set; }

    public virtual DbSet<MstPrefecture> MstPrefectures { get; set; }

    public virtual DbSet<MstZip> MstZips { get; set; }

    public virtual DbSet<TrnAddress> TrnAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["AddressBookDatabase"].ConnectionString);
        //optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_address_PKC");

            entity.ToTable("mst_address", tb => tb.HasComment("住所マスタ"));

            entity.Property(e => e.Id)
                .HasComment("ＩＤ")
                .HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasComment("市区町村名")
                .HasColumnName("city");
            entity.Property(e => e.CityCana)
                .HasMaxLength(40)
                .HasComment("市区町村名（カナ）")
                .HasColumnName("city_cana");
            entity.Property(e => e.Code1)
                .HasComment("コード１")
                .HasColumnName("code1");
            entity.Property(e => e.Code2)
                .HasComment("コード２")
                .HasColumnName("code2");
            entity.Property(e => e.Code3)
                .HasComment("コード３")
                .HasColumnName("code3");
            entity.Property(e => e.Code4)
                .HasComment("コード４")
                .HasColumnName("code4");
            entity.Property(e => e.Code5)
                .HasComment("コード５")
                .HasColumnName("code5");
            entity.Property(e => e.Code6)
                .HasComment("コード６")
                .HasColumnName("code6");
            entity.Property(e => e.GovCode)
                .HasMaxLength(5)
                .HasComment("全国地方公共団体コード")
                .HasColumnName("gov_code");
            entity.Property(e => e.Pref)
                .HasMaxLength(10)
                .HasComment("都道府県名")
                .HasColumnName("pref");
            entity.Property(e => e.PrefKana)
                .HasMaxLength(10)
                .HasComment("都道府県名（カナ）")
                .HasColumnName("pref_kana");
            entity.Property(e => e.Town)
                .HasMaxLength(400)
                .HasComment("町域名")
                .HasColumnName("town");
            entity.Property(e => e.TownKana)
                .HasMaxLength(400)
                .HasComment("町域名（カナ）")
                .HasColumnName("town_kana");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(7)
                .HasComment("郵便番号")
                .HasColumnName("zip_code");
            entity.Property(e => e.ZipCodeOld)
                .HasMaxLength(5)
                .HasComment("郵便番号（旧）")
                .HasColumnName("zip_code_old");
        });

        modelBuilder.Entity<MstAreaCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_area_code_PKC");

            entity.ToTable("mst_area_code", tb => tb.HasComment("市外局番"));

            entity.Property(e => e.Id)
                .HasComment("ＩＤ")
                .HasColumnName("id");
            entity.Property(e => e.AreaCode)
                .HasMaxLength(4)
                .HasComment("市外局番")
                .HasColumnName("area_code");
            entity.Property(e => e.CityCode)
                .HasMaxLength(4)
                .HasComment("市内局番")
                .HasColumnName("city_code");
            entity.Property(e => e.Section)
                .HasMaxLength(800)
                .HasComment("番号区画")
                .HasColumnName("section");
            entity.Property(e => e.SectionCode)
                .HasMaxLength(8)
                .HasComment("番号区画コード")
                .HasColumnName("section_code");
        });

        modelBuilder.Entity<MstCity>(entity =>
        {
            entity.HasKey(e => e.GovCode).HasName("mst_city_PKC");

            entity.ToTable("mst_city", tb => tb.HasComment("市区町村名マスタ"));

            entity.Property(e => e.GovCode)
                .HasMaxLength(5)
                .HasComment("行政区域コード")
                .HasColumnName("gov_code");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasComment("市区町村名")
                .HasColumnName("city");
            entity.Property(e => e.Pref)
                .HasMaxLength(10)
                .HasComment("都道府県名")
                .HasColumnName("pref");
        });

        modelBuilder.Entity<MstPrefecture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_prefecture_PKC");

            entity.ToTable("mst_prefecture", tb => tb.HasComment("都道府県マスタ"));

            entity.Property(e => e.Id)
                .HasComment("ＩＤ")
                .HasColumnName("id");
            entity.Property(e => e.Pref)
                .HasMaxLength(10)
                .HasComment("都道府県名")
                .HasColumnName("pref");
            entity.Property(e => e.PrefKana)
                .HasMaxLength(10)
                .HasComment("都道府県名（カナ）")
                .HasColumnName("pref_kana");
        });

        modelBuilder.Entity<MstZip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_zip_PKC");

            entity.ToTable("mst_zip", tb => tb.HasComment("郵便番号マスタ"));

            entity.Property(e => e.Id)
                .HasComment("ＩＤ")
                .HasColumnName("id");
            entity.Property(e => e.GovCode)
                .HasMaxLength(5)
                .HasComment("行政区域コード")
                .HasColumnName("gov_code");
            entity.Property(e => e.Town)
                .HasMaxLength(40)
                .HasComment("町域名")
                .HasColumnName("town");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(7)
                .HasComment("郵便番号")
                .HasColumnName("zip_code");

            entity.HasOne(d => d.GovCodeNavigation).WithMany(p => p.MstZips)
                .HasForeignKey(d => d.GovCode)
                .HasConstraintName("mst_zip_FK1");
        });

        modelBuilder.Entity<TrnAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trn_address_PKC");

            entity.ToTable("trn_address", tb => tb.HasComment("住所録トラン"));

            entity.Property(e => e.Id)
                .HasComment("ＩＤ")
                .HasColumnName("id");
            entity.Property(e => e.Apart)
                .HasMaxLength(40)
                .HasComment("アパート等")
                .HasColumnName("apart");
            entity.Property(e => e.Block)
                .HasMaxLength(40)
                .HasComment("番地等")
                .HasColumnName("block");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasComment("市区町村名")
                .HasColumnName("city");
            entity.Property(e => e.Mail)
                .HasMaxLength(80)
                .HasComment("メール")
                .HasColumnName("mail");
            entity.Property(e => e.Memo)
                .HasMaxLength(256)
                .HasComment("メモ")
                .HasColumnName("memo");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasComment("氏名")
                .HasColumnName("name");
            entity.Property(e => e.Pref)
                .HasMaxLength(10)
                .HasComment("都道府県名")
                .HasColumnName("pref");
            entity.Property(e => e.Tel)
                .HasMaxLength(13)
                .HasComment("電話番号")
                .HasColumnName("tel");
            entity.Property(e => e.Town)
                .HasMaxLength(40)
                .HasComment("町域名")
                .HasColumnName("town");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(7)
                .HasComment("郵便番号")
                .HasColumnName("zip_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
