namespace Crm.Project.Model.Relationships
{
	using Crm.Library.BaseModel;
	using Crm.Model;
	using Crm.Project.Model.Lookups;

	public class PotentialContactRelationship : LookupRelationship<Potential, Contact, PotentialContactRelationshipType>
	{
		public virtual Person ChildPerson { get; set; } //use this only for odata api as it can throw a ObjectNotFoundException when accessing
		public virtual Company ChildCompany { get; set; } //use this only for odata api as it can throw a ObjectNotFoundException when accessing
	}
}