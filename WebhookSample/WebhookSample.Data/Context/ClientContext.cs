﻿using Microsoft.EntityFrameworkCore;
using WebhookSample.Domain.Entities;

namespace WebhookSample.Data.Context
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<WebhookEventStatus> WebhookStatus { get; set; }
        public DbSet<ClientHistory> ClientHistories { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
