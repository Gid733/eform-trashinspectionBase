/*
The MIT License (MIT)

Copyright (c) 2007 - 2019 microting

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using Microsoft.EntityFrameworkCore;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using Microting.eFormApi.BasePn.Infrastructure.Database.Extensions;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities;

namespace Microting.eFormTrashInspectionBase.Infrastructure.Data
{
    public class TrashInspectionPnDbContext : DbContext, IPluginDbContext
    {
        public TrashInspectionPnDbContext()
        {
        }

        public TrashInspectionPnDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Installation> Installations { get; set; }
        public DbSet<InstallationVersion> InstallationVersions { get; set; }
        public DbSet<InstallationSite> InstallationSites { get; set; }
        public DbSet<InstallationSiteVersion> InstallationSiteVersions { get; set; }
        public DbSet<TrashInspection> TrashInspections { get; set; }
        public DbSet<TrashInspectionVersion> TrashInspectionVersions { get; set; }
        public DbSet<TrashInspectionPnSetting> TrashInspectionPnSettings { get; set; }
        public DbSet<TrashInspectionPnSettingVersion> TrashInspectionPnSettingVersions { get; set; }
        public DbSet<TrashInspectionCase> TrashInspectionCases { get; set; }
        public DbSet<TrashInspectionCaseVersion> TrashInspectionCaseVersions { get; set; }
        public DbSet<Fraction> Fractions { get; set; }
        public DbSet<FractionVersion> FractionVersions { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<SegmentVersion> SegmentVersions { get; set; }

        // add plugin config
        public DbSet<PluginConfigurationValue> PluginConfigurationValues { get; set; }
        public DbSet<PluginConfigurationValueVersion> PluginConfigurationValueVersions { get; set; }

        public virtual Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade ContextDatabase => Database;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddPluginSettingsRules();
        }
    }
}