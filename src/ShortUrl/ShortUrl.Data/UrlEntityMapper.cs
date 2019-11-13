using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortUrl.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortUrl.Data
{
    public class UrlEntityMapper : ShortUrlEntityTypeConfiguration<UrlEntity>
    {
        #region Methods

        /// <summary>
        /// Configures the Series entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<UrlEntity> builder)
        {
            builder.ToTable(nameof(UrlEntity));
            builder
            .Property(b => b.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
