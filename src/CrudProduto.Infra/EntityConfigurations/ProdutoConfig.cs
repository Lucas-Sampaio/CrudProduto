using CrudProduto.Domain.ProdutoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudProduto.Infra.EntityConfigurations;

internal class ProdutoConfig : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(nameof(Produto));

        builder.HasKey(c => c.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.Nome).IsRequired().HasColumnType($"varchar({Produto.NomeMaximo})");
        builder.Property(x => x.Descricao).HasColumnType($"varchar({Produto.DescricaoMaximo})");
        builder.Property(x => x.Codigo).IsRequired();
        builder.HasIndex(x => x.Codigo).IsUnique();
        builder.Property(x => x.Valor).IsRequired();
        builder.HasOne(x => x.Tag).WithMany(x => x.Produtos).IsRequired();
    }
}