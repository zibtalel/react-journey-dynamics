namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	[Migration(20211201120500)]
	public class UpdatePotentialPriorityLookup : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[LU].[PotentialPriority]"))
			{
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '20% (Persönlicher Kontakt)' WHERE Value = 'prio1' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '20% (Personal contact)' WHERE Value = 'prio1' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '20% (Contacto personal)' WHERE Value = 'prio1' AND Language = 'es'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '20% (Contact personnel)' WHERE Value = 'prio1' AND Language = 'fr'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '20% (Személyes kapcsolattartó)' WHERE Value = 'prio1' AND Language = 'hu'");
				
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '40% (Unterlagen versandt)' WHERE Value = 'prio2' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '40% (Information dispatched)' WHERE Value = 'prio2' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '40% (Informacion enviada)' WHERE Value = 'prio2' AND Language = 'es'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '40% (Envoi informations)' WHERE Value = 'prio2' AND Language = 'fr'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '40% (Információ elküldve)' WHERE Value = 'prio2' AND Language = 'hu'");
				
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '60% (Interesse vorhanden)' WHERE Value = 'prio3' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '60% (Substantial interest)' WHERE Value = 'prio3' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '60% (Interés sustancial)' WHERE Value = 'prio3' AND Language = 'es'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '60% (Intérêt substantiel)' WHERE Value = 'prio3' AND Language = 'fr'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '60% (Valódi érdeklődés)' WHERE Value = 'prio3' AND Language = 'hu'");	
				
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '80% (Konkretes Projekt)' WHERE Value = 'prio4' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '80% (Project budgeted)' WHERE Value = 'prio4' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '80% (Proyecto presupuestado)' WHERE Value = 'prio4' AND Language = 'es'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '80% (Projet budgétisé)' WHERE Value = 'prio4' AND Language = 'fr'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '80% (Projektfinanszírozás megbecsülve)' WHERE Value = 'prio4' AND Language = 'hu'");	
				
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '100% (Bereit zur übergabe)' WHERE Value = 'prio5' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '100% (Ready for handover)' WHERE Value = 'prio5' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '100% (Listo para el traspaso)' WHERE Value = 'prio5' AND Language = 'es'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '100% (Prêt à être remis)' WHERE Value = 'prio5' AND Language = 'fr'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[PotentialPriority] SET Name = '100% (Átvételre kész)' WHERE Value = 'prio5' AND Language = 'hu'");

			}
		}
	}
}