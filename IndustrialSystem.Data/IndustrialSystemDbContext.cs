using Microsoft.EntityFrameworkCore;
using IndustrialSystem.Data.Entities;

namespace IndustrialSystem.Data;

public class IndustrialSystemDbContext : DbContext
{
    public IndustrialSystemDbContext(DbContextOptions<IndustrialSystemDbContext> options)
        : base(options)
    {
    }

    public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
    public DbSet<TypePoste> TypesPostes { get; set; } = null!;
    public DbSet<Poste> Postes { get; set; } = null!;
    public DbSet<Application> Applications { get; set; } = null!;
    public DbSet<TypeProduit> TypesProduits { get; set; } = null!;
    public DbSet<FamilleProduit> FamillesProduits { get; set; } = null!;
    public DbSet<Produit> Produits { get; set; } = null!;
    public DbSet<Operation> Operations { get; set; } = null!;
    public DbSet<Nomenclature> Nomenclatures { get; set; } = null!;
    public DbSet<Parametre> Parametres { get; set; } = null!;
    public DbSet<HistoriqueAction> HistoriqueActions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration des relations et contraintes

        // Utilisateur
        modelBuilder.Entity<Utilisateur>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Utilisateur>()
            .HasIndex(u => u.Matricule)
            .IsUnique();

        // Poste
        modelBuilder.Entity<Poste>()
            .HasOne(p => p.TypePoste)
            .WithMany(tp => tp.Postes)
            .HasForeignKey(p => p.TypePosteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Application
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Poste)
            .WithMany(p => p.Applications)
            .HasForeignKey(a => a.PosteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Produit
        modelBuilder.Entity<Produit>()
            .HasOne(p => p.TypeProduit)
            .WithMany(tp => tp.Produits)
            .HasForeignKey(p => p.TypeProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Produit>()
            .HasOne(p => p.FamilleProduit)
            .WithMany(fp => fp.Produits)
            .HasForeignKey(p => p.FamilleProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        // Operation
        modelBuilder.Entity<Operation>()
            .HasOne(o => o.Produit)
            .WithMany(p => p.Operations)
            .HasForeignKey(o => o.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        // Nomenclature
        modelBuilder.Entity<Nomenclature>()
            .HasOne(n => n.ProduitFini)
            .WithMany(p => p.Nomenclatures)
            .HasForeignKey(n => n.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        // Parametre
        modelBuilder.Entity<Parametre>()
            .HasOne(p => p.Poste)
            .WithMany(p => p.Parametres)
            .HasForeignKey(p => p.PosteId)
            .OnDelete(DeleteBehavior.Restrict);

        // HistoriqueAction
        modelBuilder.Entity<HistoriqueAction>()
            .HasOne(h => h.Utilisateur)
            .WithMany(u => u.HistoriqueActions)
            .HasForeignKey(h => h.UtilisateurId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index composites et autres configurations
        modelBuilder.Entity<Parametre>()
            .HasIndex(p => new { p.Nom, p.PosteId })
            .IsUnique();

        modelBuilder.Entity<Operation>()
            .HasIndex(o => new { o.ProduitId, o.OrdreExecution });
    }
}