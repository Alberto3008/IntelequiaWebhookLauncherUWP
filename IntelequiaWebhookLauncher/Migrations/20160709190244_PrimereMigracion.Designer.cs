using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IntelequiaWebhookLauncher.Model;

namespace IntelequiaWebhookLauncher.Migrations
{
    [DbContext(typeof(WebhookContext))]
    [Migration("20160709190244_PrimereMigracion")]
    partial class PrimereMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("IntelequiaWebhookLauncher.Model.Webhook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Expire");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Webhooks");
                });
        }
    }
}
