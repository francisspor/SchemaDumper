using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.SqlServer;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;
using FluentMigrator.SchemaDump.SchemaDumpers;

namespace SchemaDumper
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connection = new SqlConnection("data source=172.16.1.186;UID=lssuser;PWD=password1;initial catalog=lss");

            var sb = new StringBuilder();
            var textWriter = new StringWriter(sb);
            var announcer = new TextWriterAnnouncer(textWriter);

            var generator = new SqlServer2008Generator();
            var processor = new SqlServerProcessor(connection, generator, announcer, new ProcessorOptions(), new SqlServerDbFactory());


            var dumper = new SqlServerSchemaDumper(processor, announcer);

            var schema = dumper.ReadDbSchema();
            var schemaWriter = new SchemaWriter();
            schemaWriter.WriteToFile(schema, "C:\\migration.cs");
        }
    }
}
