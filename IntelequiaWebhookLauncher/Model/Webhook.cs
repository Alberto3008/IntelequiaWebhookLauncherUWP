using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IntelequiaWebhookLauncher.Model
{
    public class WebhookContext : DbContext
    {
        public DbSet<Webhook> Webhooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Webhook.db");
        }
    }

    public class Webhook
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Expire { get; set; }

      
    }
}
