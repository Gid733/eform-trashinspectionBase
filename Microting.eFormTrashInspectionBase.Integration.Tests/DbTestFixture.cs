using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microting.eFormTrashInspectionBase.Infrastructure.Data;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Factories;
using NUnit.Framework;

namespace Microting.eFormTrashInspectionBase.Integration.Tests
{
    [TestFixture]
    public abstract class DbTestFixture
    {

        protected TrashInspectionPnDbContext DbContext;
        private string _connectionString;

        private void GetContext(string connectionStr)
        {
            TrashInspectionPnContextFactory contextFactory = new TrashInspectionPnContextFactory();
            DbContext = contextFactory.CreateDbContext(new[] {connectionStr});

            DbContext.Database.Migrate();
            DbContext.Database.EnsureCreated();
        }

        [SetUp]
        public void Setup()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _connectionString = @"data source=(LocalDb)\SharedInstance;Initial catalog=trash-inspection-pn-tests;Integrated Security=true";
            }
            else
            {
                _connectionString = @"Server = localhost; port = 3306; Database = trash-inspection-pn-tests; user = root; password = secretpassword; Convert Zero Datetime = true;";
            }

            GetContext(_connectionString);


            DbContext.Database.SetCommandTimeout(300);

            try
            {
                ClearDb();
            }
            catch
            {
                DbContext.Database.Migrate();
            }
            DoSetup();
        }

        [TearDown]
        public void TearDown()
        {
            ClearDb();

            ClearFile();

            DbContext.Dispose();
        }

        private void ClearDb()
        {
            List<string> modelNames = new List<string>
            {
                "TrashInspectionVersions",
                "TrashInspections",
                "Installations",
                "InstallationVersions",
                "InstallationSites",
                "InstallationSiteVersions",
                "FractionVersions",
                "Fractions",
                "SegmentVersions",
                "Segments",
                "PluginConfigurationValues",
                "PluginConfigurationValueVersions"
            };

            bool firstRunNotDone = true;

            foreach (var modelName in modelNames)
            {
                try
                {
                    if (firstRunNotDone)
                    {
                        DbContext.Database.ExecuteSqlRaw(
                            $"SET FOREIGN_KEY_CHECKS = 0;TRUNCATE `trash-inspection-pn-tests`.`{modelName}`");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Unknown database 'trash-inspection-pn-tests'")
                    {
                        firstRunNotDone = false;
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        private string _path;

        private void ClearFile()
        {
            _path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _path = System.IO.Path.GetDirectoryName(_path)?.Replace(@"file:\", "");

            string picturePath = _path + @"\output\dataFolder\picture\Deleted";

            DirectoryInfo diPic = new DirectoryInfo(picturePath);

            try
            {
                foreach (FileInfo file in diPic.GetFiles())
                {
                    file.Delete();
                }
            }
            catch { }


        }

        protected virtual void DoSetup() { }
    }
}