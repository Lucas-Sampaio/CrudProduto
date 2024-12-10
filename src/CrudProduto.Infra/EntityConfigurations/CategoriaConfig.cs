using CrudProduto.Domain.ProdutoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudProduto.Infra.EntityConfigurations;

internal class CategoriaConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable(nameof(Tag));

        builder.HasKey(c => c.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.Descricao).HasColumnType($"varchar({Tag.DescricaoMaximo})").IsRequired();
        builder.HasIndex(x => x.Descricao);
    }
}