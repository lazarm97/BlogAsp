﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(i => i.Path)
                .IsRequired();

            builder.HasIndex(i => i.Path)
                .IsUnique();

            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
