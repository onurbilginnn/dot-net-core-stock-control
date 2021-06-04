using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StockControl.Models;


namespace StockControl.Context
{
    public partial class BAR_ERPContext : DbContext
    {
        // Unable to generate entity type for table 'dbo.Kategori'. Please see the warning messages.

        public BAR_ERPContext(DbContextOptions<BAR_ERPContext> options)
        : base(options)
        { }

        public virtual DbSet<TKategori> TKategori { get; set; }
        public virtual DbSet<TFaturaGiris> TFaturaGiris { get; set; }
        public virtual DbSet<TSubeBilgileri> TSubeBilgileri { get; set; }
        public virtual DbSet<TUrunListesi> TUrunListesi { get; set; }
        public virtual DbSet<TBOM> TBOM { get; set; }
        public virtual DbSet<TTedarikciBilgileri> TTedarikciBilgileri { get; set; }
        public virtual DbSet<TSatis> TSatis { get; set; }
        public virtual DbSet<TStokDuz> StokDuz { get; set; }
        public virtual DbSet<TYillikSayim> YillikSayim { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TKategori>(entity =>
            {
                entity.HasKey(e => e.KategoriSiraNo);

                entity.ToTable("T_Kategori");

                entity.Property(e => e.KategoriSiraNo).HasColumnName("Sira_No");

                entity.Property(e => e.Kategori)
                   .HasColumnName("Kategori")
                   .HasMaxLength(255);

                entity.Property(e => e.KategoriNo)
                   .HasColumnName("Kategori No")
                   .HasColumnType("int");

                entity.Property(e => e.AltKategori)
                  .HasColumnName("Alt Kategori")
                  .HasMaxLength(255);

                entity.Property(e => e.AltKategoriNo)
                  .HasColumnName("Alt Kategori No")
                  .HasColumnType("int");

            });
            modelBuilder.Entity<TFaturaGiris>(entity =>
            {
                entity.Ignore(e => e.SiraNo);
                entity.HasKey(e => e.SiraNo);
                entity.Property(e => e.SiraNo).HasColumnName("Sira_No");

                entity.ToTable("T_Fatura_Giris");

                entity.Property(e => e.BirimFiyatı)
                    .HasColumnName("Birim_Fiyatı")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FaturaNo)
                    .IsRequired()
                    .HasColumnName("Fatura_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FaturaTarihi)
                    .IsRequired()
                    .HasColumnName("Fatura_Tarihi")
                    .HasMaxLength(20);

                entity.Property(e => e.FiyatKurTipi)
                    .IsRequired()
                    .HasColumnName("Fiyat_Kur_Tipi")
                    .HasMaxLength(10);

                entity.Property(e => e.IrsaliyeNo)
                    .HasColumnName("Irsaliye_No")
                    .HasMaxLength(100);

                entity.Property(e => e.IrsaliyeTarihi)
                    .HasColumnName("Irsaliye_Tarihi")
                    .HasMaxLength(20);

                entity.Property(e => e.KdvTutar)
                    .HasColumnName("KDV_Tutar")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.KdvYuzde)
                    .HasColumnName("KDV_Yuzde")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.KdvliToplamTutar)
                    .HasColumnName("KDVli_Toplam_Tutar")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.KdvsizToplam)
                    .HasColumnName("KDVsiz_Toplam")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MiktarBirimi)
                    .IsRequired()
                    .HasColumnName("Miktar_Birimi")
                    .HasMaxLength(50);

                entity.Property(e => e.SubeNo)
                    .IsRequired()
                    .HasColumnName("Sube_No")
                    .HasMaxLength(10);

                entity.Property(e => e.TedarikciAdi)
                    .IsRequired()
                    .HasColumnName("Tedarikci_Adi")
                    .HasMaxLength(500);

                entity.Property(e => e.UrunAdi)
                    .IsRequired()
                    .HasColumnName("Urun_Adi")
                    .HasMaxLength(500);

                entity.Property(e => e.UrunAltKategori)
                    .IsRequired()
                    .HasColumnName("Urun_Alt_Kategori")
                    .HasMaxLength(255);

                entity.Property(e => e.UrunKategori)
                    .IsRequired()
                    .HasColumnName("Urun_Kategori")
                    .HasMaxLength(255);

                entity.Property(e => e.UrunMiktari)
                    .HasColumnName("Urun_Miktari")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UrunNo)
                    .IsRequired()
                    .HasColumnName("Urun_No")
                    .HasMaxLength(50);

                entity.Property(e => e.KayitTarihi)
    .IsRequired()
    .HasColumnName("Kayit_Tarihi")
    .HasMaxLength(20);

                entity.Property(e => e.DegTarihi)
               .IsRequired()
               .HasColumnName("Degis_Tarihi")
               .HasMaxLength(20);

                entity.Property(e => e.KayitKadi)
              .HasColumnName("Kayit_Kadi")
              .HasMaxLength(100);

                entity.Property(e => e.DegKadi)
             .HasColumnName("Deg_Kadi")
             .HasMaxLength(100);
            });
            modelBuilder.Entity<TSubeBilgileri>(entity =>
            {
                entity.HasKey(e => e.SubeNo);

                entity.ToTable("T_SubeBilgileri");

                entity.Property(e => e.SubeNo)
                    .HasColumnName("Sube_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasColumnName("Adres")
                    .HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(50);

                entity.Property(e => e.PostaKodu)
                    .HasColumnName("Posta Kodu")
                    .HasMaxLength(20);

                entity.Property(e => e.Sehir)
                    .IsRequired()
                    .HasColumnName("Sehir")
                    .HasMaxLength(30);

                entity.Property(e => e.SubeAdi)
                    .IsRequired()
                    .HasColumnName("Sube_Adi")
                    .HasMaxLength(100);

                entity.Property(e => e.TelNo)
                    .IsRequired()
                    .HasColumnName("Tel No")
                    .HasMaxLength(30);

                entity.Property(e => e.Ulke)
                    .IsRequired()
                    .HasColumnName("Ulke")
                    .HasMaxLength(30);

                entity.Property(e => e.KayitTarihi)
                .IsRequired()
                .HasColumnName("Kayit_Tarihi")
                .HasMaxLength(20);

                entity.Property(e => e.DegTarihi)
               .IsRequired()
               .HasColumnName("Degis_Tarihi")
               .HasMaxLength(20);

                entity.Property(e => e.KayitKadi)
             .HasColumnName("Kayit_Kadi")
             .HasMaxLength(100);

                entity.Property(e => e.DegKadi)
             .HasColumnName("Deg_Kadi")
             .HasMaxLength(100);
            });
            modelBuilder.Entity<TUrunListesi>(entity =>
            {
                entity.HasKey(e => e.UrunNo);

                entity.ToTable("T_Urun_Listesi");

                entity.Property(e => e.UrunNo)
                    .HasColumnName("Urun_No")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.KdvOrani)
                    .HasColumnName("KDV_Orani")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MiktarBirimi)
                    .IsRequired()
                    .HasColumnName("Miktar_Birimi")
                    .HasMaxLength(50);

                entity.Property(e => e.UrunAdi)
                    .IsRequired()
                    .HasColumnName("Urun_Adi")
                    .HasMaxLength(500);

                entity.Property(e => e.UrunAltKategori)
                    .IsRequired()
                    .HasColumnName("Urun_Alt_Kategori")
                    .HasMaxLength(255);

                entity.Property(e => e.UrunKategori)
                    .IsRequired()
                    .HasColumnName("Urun_Kategori")
                    .HasMaxLength(255);

                entity.Property(e => e.SubeNo)
                   .IsRequired()
                   .HasColumnName("Sube_No")
                   .HasColumnType("int");

                entity.Property(e => e.UrunSeviyesi)
                    .IsRequired()
                    .HasColumnName("Urun_Seviyesi")
                    .HasMaxLength(50);

                entity.Property(e => e.KayitTarihi)
          .IsRequired()
          .HasColumnName("Kayit_Tarihi")
          .HasMaxLength(20);

                entity.Property(e => e.DegTarihi)
               .IsRequired()
               .HasColumnName("Degis_Tarihi")
               .HasMaxLength(20);

                entity.Property(e => e.KayitKadi)
             .HasColumnName("Kayit_Kadi")
             .HasMaxLength(100);

                entity.Property(e => e.DegKadi)
             .HasColumnName("Deg_Kadi")
             .HasMaxLength(100);
            });
            modelBuilder.Entity<TBOM>(entity =>
            {
                entity.Ignore(e => e.SiraNo);
                entity.HasKey(e => e.SiraNo);
                entity.Property(e => e.SiraNo).HasColumnName("Sira_No");

                entity.ToTable("T_BOM");

                entity.Property(e => e.SubeNo)
                   .IsRequired()
                   .HasColumnName("Sube_No")
                   .HasMaxLength(10);

                entity.Property(e => e.UrunSeviyesi)
                  .IsRequired()
                  .HasColumnName("Urun_Seviye")
                  .HasMaxLength(75);

                entity.Property(e => e.UrunNo)
              .IsRequired()
              .HasColumnName("Urun_No")
              .HasMaxLength(50);

                entity.Property(e => e.UrunAdi)
                .IsRequired()
                .HasColumnName("Urun_Adi")
                .HasMaxLength(500);

                entity.Property(e => e.BaglRefMik)
               .IsRequired()
               .HasColumnName("Bagl_Ref_Mik");


                entity.Property(e => e.UrunMiktari)
                 .IsRequired()
                    .HasColumnName("Urun_Miktari")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FireOrani)
                .IsRequired()
                   .HasColumnName("Fire_Orani")
                   .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FireliMiktar)
                .IsRequired()
                   .HasColumnName("Fireli_Miktar")
                   .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MiktarBirimi)
               .IsRequired()
               .HasColumnName("Miktar_Birimi")
               .HasMaxLength(50);



                entity.Property(e => e.FiyatKurTipi)
                    .IsRequired()
                    .HasColumnName("Fiyat_Kur_Tipi")
                    .HasMaxLength(10);




                entity.Property(e => e.BaglAraUrunNo)
                .HasColumnName("Bagl_Ara_Urun_No")
                   .HasMaxLength(50);

                entity.Property(e => e.BaglAraUrunAdi)
              .HasColumnName("Bagl_Ara_Urun_Adi")
                 .HasMaxLength(500);

                entity.Property(e => e.BaglBitUrunNo)
              .HasColumnName("Bagl_Bit_Urun_No")
                .HasMaxLength(50);

                entity.Property(e => e.BaglBitUrunAdi)
              .HasColumnName("Bagl_Bit_Urun_Adi")
                 .HasMaxLength(500);

                entity.Property(e => e.KayitTarihi)
         .IsRequired()
         .HasColumnName("Kayit_Tarihi")
         .HasMaxLength(20);

                entity.Property(e => e.DegTarihi)
               .HasColumnName("Degis_Tarihi")
               .HasMaxLength(20);

                entity.Property(e => e.KayitKadi)
             .HasColumnName("Kayit_Kadi")
             .HasMaxLength(100);

                entity.Property(e => e.DegKadi)
             .HasColumnName("Deg_Kadi")
             .HasMaxLength(100);

            });
            modelBuilder.Entity<TTedarikciBilgileri>(entity =>
            {
                entity.HasKey(e => e.TedarikciNo);

                entity.ToTable("T_TedarikciBilgileri");

                entity.Property(e => e.SubeNo)
                 .IsRequired()
                 .HasColumnName("Sube_No")
                 .HasColumnType("int");

                entity.Property(e => e.TedarikciNo)
                    .HasColumnName("Tedarikci_No")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.TedarikciAdi)
                   .HasColumnName("Tedarikci_Adi")
                   .IsRequired()
                   .HasMaxLength(500);

                entity.Property(e => e.VTCKN)
                  .HasColumnName("Vergi_TC_Kimlik_No")
                  .IsRequired()
                  .HasMaxLength(20);

                entity.Property(e => e.BankaAdi)
                 .HasColumnName("BankaAdi")
                 .IsRequired()
                 .HasMaxLength(100);

                entity.Property(e => e.IBAN)
               .HasColumnName("IBAN_No")
               .IsRequired()
               .HasMaxLength(50);

                entity.Property(e => e.Vade)
             .HasColumnName("Odeme_Vadesi")
             .HasMaxLength(30);

                entity.Property(e => e.OdemeSekli)
                .HasColumnName("Odeme_Sekli")
                .HasMaxLength(30);

                entity.Property(e => e.Ulke)
                .IsRequired()
                .HasColumnName("Ulke")
                .HasMaxLength(30);

                entity.Property(e => e.Sehir)
               .IsRequired()
               .HasColumnName("Sehir")
               .HasMaxLength(30);

                entity.Property(e => e.Adres)
                   .IsRequired()
                   .HasColumnName("Adres")
                   .HasMaxLength(500);

                entity.Property(e => e.PostaKodu)
              .HasColumnName("Posta_Kodu")
              .HasMaxLength(20);

                entity.Property(e => e.TelNo)
             .IsRequired()
             .HasColumnName("Tel_No")
             .HasMaxLength(30);


                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(50);

                entity.Property(e => e.KayitTarihi)
          .IsRequired()
          .HasColumnName("Kayit_Tarihi")
          .HasMaxLength(20);

                entity.Property(e => e.DegTarihi)
                .HasColumnName("Degis_Tarihi")
               .HasMaxLength(20);

                entity.Property(e => e.KayitKadi)
             .HasColumnName("Kayit_Kadi")
             .HasMaxLength(100);

                entity.Property(e => e.DegKadi)
             .HasColumnName("Deg_Kadi")
             .HasMaxLength(100);
            });
            modelBuilder.Entity<TSatis>(entity =>
            {
                entity.Ignore(e => e.SiraNo);
                entity.HasKey(e => e.SiraNo);
                entity.Property(e => e.SiraNo).HasColumnName("Sira_No");
                entity.ToTable("T_Satis");

                entity.Property(e => e.SatisNo)
                .IsRequired()
               .HasColumnName("Satis_No")
               .HasMaxLength(50);

                entity.Property(e => e.SatisTarihi)
               .IsRequired()
              .HasColumnName("Satis_Tarih")
              .HasMaxLength(20);

                entity.Property(e => e.UrunNo)
              .IsRequired()
             .HasColumnName("Sat_Urun_No")
             .HasMaxLength(50);

                entity.Property(e => e.UrunAdi)
             .IsRequired()
            .HasColumnName("Sat_Urun_Adi")
            .HasMaxLength(500);

                entity.Property(e => e.UrunSeviyesi)
             .IsRequired()
            .HasColumnName("Sat_Urun_Seviyesi")
            .HasMaxLength(75);

                entity.Property(e => e.BagUrunSeviyesi)
          .HasColumnName("Bag_Urun_Seviyesi")
          .HasMaxLength(75);

                entity.Property(e => e.SatisAdet)
             .IsRequired()
            .HasColumnName("Sat_Adet")
            .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.KayitTarihi)
        .IsRequired()
        .HasColumnName("Kayit_Tarihi")
        .HasMaxLength(20);

                entity.Property(e => e.DegTarihi)
                .HasColumnName("Degis_Tarihi")
               .HasMaxLength(20);

                entity.Property(e => e.SubeNo)
             .HasColumnName("Sube_No")
             .HasColumnType("int");

                entity.Property(e => e.KayitKadi)
             .HasColumnName("Kayit_Kadi")
             .HasMaxLength(100);

                entity.Property(e => e.DegKadi)
             .HasColumnName("Deg_Kadi")
             .HasMaxLength(100);


            });
            modelBuilder.Entity<TStokDuz>(entity =>
            {
                entity.Ignore(e => e.SiraNo);
                entity.HasKey(e => e.SiraNo);
                entity.Property(e => e.SiraNo).HasColumnName("Sira_No");
                entity.ToTable("T_StokDuz");

                entity.Property(e => e.UrunNo)
               .HasColumnName("Urun_No")
               .HasMaxLength(50);

                entity.Property(e => e.UrunAdi)
               .HasColumnName("Urun_Adi")
               .HasMaxLength(500);

                entity.Property(e => e.DuzKadi)
                .HasColumnName("Duz_Kadi")
                .HasMaxLength(100);


                entity.Property(e => e.DuzAdedi)
            .HasColumnName("Duz_Adedi")
            .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DuzTarihi)
        .HasColumnName("Duz_Tarihi")
        .HasMaxLength(100);

                entity.Property(e => e.SubeNo)
    .HasColumnName("Sube_No")
    .HasMaxLength(10);

                entity.Property(e => e.KayitTarihi)
.HasColumnName("Kayit_Tarihi")
.HasMaxLength(50);

            });
            modelBuilder.Entity<TYillikSayim>(entity =>
            {
                entity.Ignore(e => e.SiraNo);
                entity.HasKey(e => e.SiraNo);
                entity.Property(e => e.SiraNo).HasColumnName("Sira_No");

                entity.ToTable("T_YillikSayim");

                entity.Property(e => e.UrunNo)
               .HasColumnName("Urun_No")
               .HasMaxLength(50);

                entity.Property(e => e.UrunAdi)
               .HasColumnName("Urun_Adi")
               .HasMaxLength(500);

                entity.Property(e => e.Acilis)
          .HasColumnName("Acilis")
          .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Giris)
          .HasColumnName("Giris")
          .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Cikis)
     .HasColumnName("Cikis")
     .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Duzeltme)
     .HasColumnName("Duzeltme")
     .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Bakiye)
     .HasColumnName("Bakiye")
     .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FiiliSayim)
     .HasColumnName("Fiili_Sayim")
     .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Fark)
     .HasColumnName("Fark")
     .HasColumnType("decimal(18, 4)");


                entity.Property(e => e.StokDonemiYil)
             .HasColumnName("StokDonemiYil")
             .HasMaxLength(50);

                entity.Property(e => e.KayitKadi)
      .HasColumnName("KayitKadi")
       .HasMaxLength(50);

                entity.Property(e => e.SubeNo)
          .HasColumnName("SubeNo")
          .HasMaxLength(50);

                entity.Property(e => e.KayitTarihi)
        .HasColumnName("KayitTarihi")
        .HasMaxLength(50);


            });


    }
    }
}
